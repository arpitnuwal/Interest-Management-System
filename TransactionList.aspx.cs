using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class TransactionList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        
          
    }

    void LoadData(string name)
    {
        con.Open();

        string query = @"
            SELECT t.ID, p.Name,
                   CASE WHEN t.Type=1 THEN 'Debit' ELSE 'Credit' END AS TypeText,
                   t.Type, t.Amount, t.InterestAmount, t.TransactionFromDate, t.IsYearEnd
            FROM Transactions t  INNER JOIN Party p ON t.PersonID = p.PartyID   WHERE 1=1  " + name + "    ORDER BY t.TransactionFromDate, t.ID";

        SqlDataAdapter da = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvTransactions.DataSource = dt;
        gvTransactions.DataBind();

        decimal runningBalance = 0, totalDebit = 0, totalCredit = 0, totalInterest = 0;

        foreach (DataRow row in dt.Rows)
        {
            int type = Convert.ToInt32(row["Type"]);
            decimal amt = Convert.ToDecimal(row["Amount"]);
            decimal intr = Convert.ToDecimal(row["InterestAmount"]);

            if (type == 1) { runningBalance += amt + intr; totalDebit += amt + intr; }
            else { runningBalance -= amt; totalCredit += amt; }

            totalInterest += intr;
        }

        lblDebit.Text = totalDebit.ToString("0.00");
        lblCredit.Text = totalCredit.ToString("0.00");
        lblInterest.Text = totalInterest.ToString("0.00");
        lblProfit.Text = runningBalance.ToString("0.00");

        con.Close();
    }

    protected void gvTransactions_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvTransactions.PageIndex = e.NewPageIndex;

        string sql = "";

        if (txtname.Text != "")
        {
            sql = "and  p.Name ='" + txtname.Text + "' ";
        }
        if (txtmobile.Text != "")
        {
            sql = sql + " and P.mobile ='" + txtmobile.Text + "' ";
        }


        LoadData(sql);
    }

    protected void gvTransactions_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvTransactions.DataKeys[e.RowIndex].Value);
        con.Open();
        SqlCommand cmd = new SqlCommand("DELETE FROM Transactions WHERE ID=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
        con.Close();

        string sql = "";

        if (txtname.Text != "")
        {
            sql = "and  p.Name ='" + txtname.Text + "' ";
        }
        if (txtmobile.Text != "")
        {
            sql = sql + " and P.mobile ='" + txtmobile.Text + "' ";
        }


        LoadData(sql);
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string sql = "";

        if (txtname.Text != "")
        {
            sql = "and  p.Name ='" + txtname.Text + "' ";
        }
        if (txtmobile.Text != "")
        {
            sql = sql + " and P.mobile ='" + txtmobile.Text + "' ";
        }


        LoadData(sql);
    }
}