using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class AddTransaction : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
        }
    }

   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {

                lblMsg.Text = "Enter name";
                return;
            
            }

            int partyId = Convert.ToInt32(lblpid.Text);
            int type = Convert.ToInt32(ddlType.SelectedValue);
            decimal amount = Convert.ToDecimal(txtAmount.Text);
            decimal monthlyRate = Convert.ToDecimal(txtInterest.Text);
            DateTime fromDate, toDate;

            bool isFromValid = DateTime.TryParseExact(
                txtDate.Text.Trim(),
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fromDate);

            bool isToValid = DateTime.TryParseExact(
                txtCloseDate.Text.Trim(),
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out toDate);

            if (!isFromValid || !isToValid)
            {
                lblMsg.Text = "Please enter valid date in dd-MM-yyyy format";
                return;
            }

            if (toDate < fromDate)
            {
                lblMsg.Text = "To date must be greater than from date";
                return;
            }

            // total months
            int months = ((toDate.Year - fromDate.Year) * 12)
                       + (toDate.Month - fromDate.Month);

            // month date add करके difference निकालो
            DateTime tempDate = fromDate.AddMonths(months);

            // अगर date आगे निकल गई तो 1 month कम
            if (tempDate > toDate)
            {
                months--;
                tempDate = fromDate.AddMonths(months);
            }

            // remaining days
            int days = (toDate - tempDate).Days;

            con.Open();

            decimal interest = 0;



            int daysRemainingInMonth = days;
            decimal currentMonthFraction = daysRemainingInMonth / 30m;

            int fullMonths = months;

            decimal totalMonths = fullMonths + currentMonthFraction;

            interest = Math.Round(
                amount * monthlyRate * totalMonths / 100,
                2
            );

            string mode = (type == 1) ? "Debit" : "Credit";

            string query = @"INSERT INTO Transactions
        (PersonID, Type, Amount, InterestAmount,
         TransactionFromDate, IsYearEnd, mode,per,Closedate)
        VALUES (@p,@t,@a,@i,@d,@y,@m,'" + txtInterest.Text + "','" + toDate + "')";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@p", partyId);
            cmd.Parameters.AddWithValue("@t", type);
            cmd.Parameters.AddWithValue("@a", amount);
            cmd.Parameters.AddWithValue("@i", interest);
            cmd.Parameters.AddWithValue("@d", fromDate);
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
  
    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        string query = @"
    SELECT TOP 1 
        p.PartyID,
        t.Per,
        t.CloseDate
    FROM Party p
    LEFT JOIN Transactions t ON p.PartyID = t.PersonID
    WHERE p.Name = @Name
    ORDER BY t.ID DESC";

        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@Name", txtname.Text);

            if (con.State != ConnectionState.Open)
                con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    // PartyID textbox
                    lblpid.Text = dr["PartyID"].ToString();

                    // Per textbox
                    txtInterest.Text = dr["Per"].ToString();

                    // Close Date textbox
                    if (dr["CloseDate"] != DBNull.Value)
                    {
                        txtCloseDate.Text = Convert.ToDateTime(dr["CloseDate"])
                            .ToString("dd-MM-yyyy");

                        lblclosedateitle.Text = "Last Transaction Close Date";
                        lblclosedateitle.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        txtCloseDate.Text = "";
                        lblclosedateitle.Text = "Transaction Close Date";
                        lblclosedateitle.ForeColor = System.Drawing.Color.Black;
                    }

                    lblclosedateitle.Visible = true;
                    txtCloseDate.Visible = true;
                }
                else
                {
                    lblMsg.Text = "Party not found";
                  
                    txtCloseDate.Text = "";
                }
            }

            con.Close();
        }
    }
}