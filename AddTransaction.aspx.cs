using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddTransaction : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadParty();
        }
    }

    void LoadParty()
    {
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT PartyID, Name FROM Party", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlParty.DataSource = dt;
        ddlParty.DataTextField = "Name";
        ddlParty.DataValueField = "PartyID";
        ddlParty.DataBind();
        ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Party --", "0"));
        con.Close();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlParty.SelectedValue == "0")
            {
                lblMsg.Text = "❌ Please select a party!";
                return;
            }

            int partyId = Convert.ToInt32(ddlParty.SelectedValue);
            int type = Convert.ToInt32(ddlType.SelectedValue);
            decimal amount = Convert.ToDecimal(txtAmount.Text);
            decimal monthlyRate = Convert.ToDecimal(txtInterest.Text);
            DateTime transactionDate = Convert.ToDateTime(txtDate.Text);

            con.Open();

            decimal interest = 0;

            DateTime endDate = new DateTime(transactionDate.Year, 12, 31);

            int daysRemainingInMonth = 30 - transactionDate.Day + 1;
            decimal currentMonthFraction = daysRemainingInMonth / 30m;

            int fullMonths = 12 - transactionDate.Month;

            decimal totalMonths = fullMonths + currentMonthFraction;

            interest = Math.Round(
                amount * monthlyRate * totalMonths / 100,
                2
            );

            string mode = (type == 1) ? "Debit" : "Credit";

            string query = @"INSERT INTO Transactions
        (PersonID, Type, Amount, InterestAmount,
         TransactionFromDate, IsYearEnd, mode,per)
        VALUES (@p,@t,@a,@i,@d,@y,@m,'"+txtInterest.Text+"')";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@p", partyId);
            cmd.Parameters.AddWithValue("@t", type);
            cmd.Parameters.AddWithValue("@a", amount);
            cmd.Parameters.AddWithValue("@i", interest);
            cmd.Parameters.AddWithValue("@d", transactionDate);
            cmd.Parameters.AddWithValue("@y", 0);
            cmd.Parameters.AddWithValue("@m", mode);

            cmd.ExecuteNonQuery();

            decimal closingBalance = GetClosingBalance(partyId);

            lblMsg.Text = "✅ Saved! Interest = ₹ " +
                          interest.ToString("0.00") +
                          " | Closing Balance: ₹ " +
                          closingBalance.ToString("0.00");

            txtAmount.Text = "";
          // txtInterest.Text = "";
            txtDate.Text = "";

            con.Close();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "❌ Error: " + ex.Message;
        }
    }
    decimal GetClosingBalance(int partyId)
    {
        decimal balance = 0;

        // ✅ YearEnd entries ignore (future safe)
        string query = "SELECT Type, Amount, InterestAmount FROM Transactions WHERE PersonID=@p AND IsYearEnd=0 ORDER BY TransactionFromDate, ID";

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@p", partyId);

        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            int type = Convert.ToInt32(dr["Type"]);
            decimal amt = Convert.ToDecimal(dr["Amount"]);
            decimal intr = Convert.ToDecimal(dr["InterestAmount"]);

            if (type == 1)
                balance += amt + intr;
            else
                balance -= amt;
        }
        dr.Close();

        return balance;
    }
}