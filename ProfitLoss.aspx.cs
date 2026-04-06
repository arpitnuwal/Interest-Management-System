using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class ProfitLoss : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  LoadParty();
        }
    }

    

   

  public void LoadLedger(string partyId)
    {
        con.Open();

        // ✅ FIX: YearEnd entries remove
        SqlCommand cmd = new SqlCommand(@"
        SELECT TransactionFromDate, Type, Amount, InterestAmount
        FROM Transactions
        WHERE 1=1  "+partyId+"  and IsYearEnd = 0         ORDER BY TransactionFromDate, ID", con);

       

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        con.Close();

        lblPartyName.Text = txtname.Text;
        ledgerBody.Controls.Clear();

        decimal totalDebit = 0, totalCredit = 0;
        decimal totalDebitInterest = 0, totalCreditInterest = 0;

        foreach (DataRow dr in dt.Rows)
        {
            HtmlTableRow row = new HtmlTableRow();

            HtmlTableCell dcDate = new HtmlTableCell();
            HtmlTableCell dcAmount = new HtmlTableCell();
            HtmlTableCell dcInterest = new HtmlTableCell();

            HtmlTableCell ccDate = new HtmlTableCell();
            HtmlTableCell ccAmount = new HtmlTableCell();
            HtmlTableCell ccInterest = new HtmlTableCell();

            int type = Convert.ToInt32(dr["Type"]);
            decimal amount = Convert.ToDecimal(dr["Amount"]);
            decimal interest = Convert.ToDecimal(dr["InterestAmount"]);
            DateTime date = Convert.ToDateTime(dr["TransactionFromDate"]);

            if (type == 1) // Debit
            {
                dcDate.InnerText = date.ToString("dd-MMM-yyyy");
                dcAmount.InnerText = "₹ " + amount.ToString("0.00");
                dcInterest.InnerText = "₹ " + interest.ToString("0.00");

                ccDate.InnerText = "-";
                ccAmount.InnerText = "-";
                ccInterest.InnerText = "-";

                totalDebit += amount;
                totalDebitInterest += interest;
            }
            else // Credit
            {
                ccDate.InnerText = date.ToString("dd-MMM-yyyy");
                ccAmount.InnerText = "₹ " + amount.ToString("0.00");
                ccInterest.InnerText = "₹ " + interest.ToString("0.00");

                dcDate.InnerText = "-";
                dcAmount.InnerText = "-";
                dcInterest.InnerText = "-";

                totalCredit += amount;
                totalCreditInterest += interest;
            }

            row.Cells.Add(dcDate);
            row.Cells.Add(dcAmount);
            row.Cells.Add(dcInterest);
            row.Cells.Add(ccDate);
            row.Cells.Add(ccAmount);
            row.Cells.Add(ccInterest);

            ledgerBody.Controls.Add(row);
        }

        // ✅ Totals
        lblTotalDebit.Text = "₹ " + totalDebit.ToString("0.00");
        lblTotalDebitInterest.Text = "₹ " + totalDebitInterest.ToString("0.00");

        lblTotalCredit.Text = "₹ " + totalCredit.ToString("0.00");
        lblTotalCreditInterest.Text = "₹ " + totalCreditInterest.ToString("0.00");

        // ✅ Profit / Loss
        decimal profitLoss = (totalCredit + totalCreditInterest) - (totalDebit + totalDebitInterest);
        lblProfitLoss.Text = "₹ " + profitLoss.ToString("0.00");

        // ✅ Closing Balance
        decimal closingBalance = (totalDebit + totalDebitInterest) - (totalCredit + totalCreditInterest);
        lblClosingBalance.Text = "₹ " + closingBalance.ToString("0.00");

        pnlLedger.Visible = true;
    }

    // Close Account
    protected void btnCloseAccount_Click(object sender, EventArgs e)
    {


        int partyId = Convert.ToInt32(lblpid.Text);

        decimal closingBalance = decimal.Parse(lblClosingBalance.Text.Replace("₹", "").Trim());

        if (closingBalance == 0)
        {
            lblCloseMsg.Text = "✅ Account already settled!";
            return;
        }

        con.Open();

        SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Transactions
        (PersonID, Type, Amount, InterestAmount, TransactionFromDate, IsYearEnd)
        VALUES (@p, @t, @a, 0, @d, 0)", con);

        cmd.Parameters.AddWithValue("@p", partyId);
        cmd.Parameters.AddWithValue("@t", closingBalance > 0 ? 2 : 1);
        cmd.Parameters.AddWithValue("@a", Math.Abs(closingBalance));
        cmd.Parameters.AddWithValue("@d", DateTime.Now);

        cmd.ExecuteNonQuery();
        con.Close();

        lblCloseMsg.Text = "✅ Account closed with balance ₹ " + closingBalance.ToString("0.00");

        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
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
}