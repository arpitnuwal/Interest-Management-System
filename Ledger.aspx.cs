using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System;
using System.Collections;
using System.Web;
public partial class Ledger : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }



    public void LoadLedger(string name)
    {
        DataTable dt = new DataTable();

        try
        {
            con.Open();

            string query = @"
            SELECT 
                ID,
                PersonID,
                TransactionFromDate,
                CASE 
                    WHEN Type = 1 THEN 'Debit' 
                    ELSE 'Credit' 
                END AS TypeName,
                Amount,
                InterestAmount
            FROM Transactions
            WHERE 1=1  and IsYearEnd=0  " + name + "       ORDER BY TransactionFromDate, ID";

            SqlCommand cmd = new SqlCommand(query, con);
           
          

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                gvLedger.DataSource = null;
                gvLedger.DataBind();
                lblClosingBalance.Text = "Closing Balance: ₹ 0.00";
                lblpid.Text = "";
                return;
            }

            lblpid.Text = dt.Rows[0]["PersonID"].ToString();

            decimal balance = 0;
            dt.Columns.Add("RunningBalance", typeof(decimal));

            foreach (DataRow row in dt.Rows)
            {
                string type = row["TypeName"].ToString();
                decimal amount = Convert.ToDecimal(row["Amount"]);
                decimal interest = Convert.ToDecimal(row["InterestAmount"]);

                if (type == "Debit")
                {
                    balance += amount + interest;
                }
                else
                {
                    balance -= amount + interest;
                }

                row["RunningBalance"] = balance;
            }

            gvLedger.DataSource = dt;
            gvLedger.DataBind();

            lblClosingBalance.Text = "Closing Balance: ₹ " + balance.ToString("0.00");
        }
        catch (Exception ex)
        {
            lblClosingBalance.Text = "Error: " + ex.Message;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    decimal GetRemainingBalance(int partyId)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand(@"
            SELECT SUM(CASE WHEN Type=1 THEN Amount + InterestAmount ELSE -Amount END) 
            FROM Transactions WHERE PersonID=@p", con);
        cmd.Parameters.AddWithValue("@p", partyId);

        object result = cmd.ExecuteScalar();
        con.Close();

        if (result == DBNull.Value)
            return 0;

        return Convert.ToDecimal(result);
    }

   
    // Edit / Update
   

   
    // Deposit Closing Balance
    protected void btnDepositBalance_Click(object sender, EventArgs e)
    {


        int partyId = Convert.ToInt32(lblpid.Text);
        decimal balance = GetRemainingBalance(partyId);

        if (balance == 0)
        {
            lblMsg.Text = "✅ No balance to deposit!";
            return;
        }

        // Insert as Credit (Type = 2)
        con.Open();
        SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Transactions (PersonID, Type, Amount, InterestAmount, TransactionFromDate, IsYearEnd)
            VALUES (@p, 2, @a, 0, @d, 0)", con);
        cmd.Parameters.AddWithValue("@p", partyId);
        cmd.Parameters.AddWithValue("@a", balance);
        cmd.Parameters.AddWithValue("@d", DateTime.Now);
        cmd.ExecuteNonQuery();
        con.Close();

      //  LoadLedger(partyId);
        lblMsg.Text = "✅ ₹ " + balance.ToString("0.00") + " deposited successfully!";

        string sql = "";

        if (txtname.Text != "")
        {
            sql = "and  PersonID = (SELECT PartyID  FROM Party WHERE Name ='" + txtname.Text + "' )";
        }
        if (txtmobile.Text != "")
        {
            sql = sql + " and PersonID = (SELECT PartyID  FROM Party WHERE mobile ='" + txtmobile.Text + "' )";
        }


        LoadLedger(sql);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sql = "";

        if (txtname.Text != "")
        {
            sql = "and  PersonID = (SELECT PartyID  FROM Party WHERE Name ='"+txtname.Text+"' )";
        }
        if (txtmobile.Text != "")
        {
            sql = sql + " and PersonID = (SELECT PartyID  FROM Party WHERE mobile ='" + txtmobile.Text + "' )";
        }


        LoadLedger(sql);
    }
}