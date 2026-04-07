using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Globalization;

public partial class ProfitLoss : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  LoadParty();
            if (string.IsNullOrEmpty(Request.QueryString["key"]))
            {
                // key missing ya empty dono case
            }
            else
            {
                // key available
            }
        }
    }

    

   

  public void LoadLedger(string partyId)
    {
        con.Open();


      






        // ✅ FIX: YearEnd entries remove
        SqlCommand cmd = new SqlCommand(@"
        SELECT TransactionFromDate, Type, Amount, InterestAmount,PersonID,per,id,Closedate
        FROM Transactions
        WHERE 1=1  " + partyId+"    ORDER BY TransactionFromDate, ID", con);

       

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);




        lblrate.Text = dt.Rows[0]["per"].ToString();
        lblpid.Text = dt.Rows[0]["PersonID"].ToString();
        lbltid.Text = dt.Rows[0]["id"].ToString();

      //
        SqlCommand totaldebittotalamount = new SqlCommand(@"select ISNULL(SUM(ABS(Amount)), 0)  from Transactions where PersonID=" + lblpid.Text + " and mode ='Debit' and  IsYearEnd=0", con);
        lblTotalDebit.Text = totaldebittotalamount.ExecuteScalar().ToString();
      //
        SqlCommand totaldebittotalInterestAmountamount = new SqlCommand(@"select   ISNULL(SUM(ABS(InterestAmount)), 0)  from Transactions where PersonID=" + lblpid.Text + " and mode ='Debit' and  IsYearEnd=0", con);
        lblTotalDebitInterest.Text = totaldebittotalInterestAmountamount.ExecuteScalar().ToString();
      //

        SqlCommand totalcreditotalamount = new SqlCommand(@"select  ISNULL(SUM(ABS(Amount)), 0) from Transactions where PersonID=" + lblpid.Text + " and mode ='Credit' and  IsYearEnd=0", con);
        lblTotalCredit.Text = totalcreditotalamount.ExecuteScalar().ToString();

      //

        SqlCommand totalcrediInteresttotalamount = new SqlCommand(@"select  ISNULL(SUM(ABS(InterestAmount)), 0) from Transactions where PersonID=" + lblpid.Text + " and mode ='Credit' and  IsYearEnd=0", con);
        lblTotalCreditInterest.Text = totalcrediInteresttotalamount.ExecuteScalar().ToString();


        decimal profitLoss = (Convert.ToDecimal(lblTotalCredit.Text) + Convert.ToDecimal(lblTotalCreditInterest.Text)) - (Convert.ToDecimal(lblTotalDebit.Text) + Convert.ToDecimal(lblTotalDebitInterest.Text));
        lblProfitLoss.Text = "₹ " + profitLoss.ToString("0.00");

        string query = @"SELECT TOP 1 CONVERT(VARCHAR(10), CloseDate, 103) FROM Transactions WHERE PersonID =" + lblpid.Text + "  ORDER BY id DESC";
       SqlCommand closedatecmd = new SqlCommand(query,con);
       string closedate = closedatecmd.ExecuteScalar().ToString();

        lblClosingBalance.Text = "₹ " + profitLoss.ToString("0.00");
        lbllastbalance.Text =profitLoss.ToString("0.00");

        lblslosingdate.Text = "&nbsp;&nbsp;&nbsp;Closing Date :  " + closedate.ToString();

        con.Close();

        lblPartyName.Text = txtname.Text;
        ledgerBody.Controls.Clear();

        decimal totalDebit = 0, totalCredit = 0;
        decimal totalDebitInterest = 0, totalCreditInterest = 0;

        foreach (DataRow dr in dt.Rows)
        {
            HtmlTableRow row = new HtmlTableRow();


            HtmlTableCell dcAmount = new HtmlTableCell(); 
            HtmlTableCell dcDate = new HtmlTableCell();
            HtmlTableCell dcInterest = new HtmlTableCell();

         
            HtmlTableCell ccAmount = new HtmlTableCell();
            HtmlTableCell ccDate = new HtmlTableCell();
            HtmlTableCell ccInterest = new HtmlTableCell();

            int type = Convert.ToInt32(dr["Type"]);
            decimal amount = Convert.ToDecimal(dr["Amount"]);
            decimal interest = Convert.ToDecimal(dr["InterestAmount"]);
            DateTime date = Convert.ToDateTime(dr["TransactionFromDate"]);
            DateTime cdate = Convert.ToDateTime(dr["Closedate"]);
            if (type == 1) // Debit
            {
                dcDate.InnerText = date.ToString("dd-MMM-yyyy") + " TO " + cdate.ToString("dd-MMM-yyyy") + " ";
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
                ccDate.InnerText = date.ToString("dd-MMM-yyyy") + "  TO  " + cdate.ToString("dd-MMM-yyyy") + " ";
                ccAmount.InnerText = "₹ " + amount.ToString("0.00");
                ccInterest.InnerText = "₹ " + interest.ToString("0.00");

                dcDate.InnerText = "-";
                dcAmount.InnerText = "-";
                dcInterest.InnerText = "-";

                totalCredit += amount;
                totalCreditInterest += interest;
            }

            dcAmount.Style["font-weight"] = "bold";
            dcDate.Style["font-weight"] = "bold";
            dcInterest.Style["font-weight"] = "bold";

            row.Cells.Add(dcAmount);
            row.Cells.Add(dcDate);
            row.Cells.Add(dcInterest);
          
            row.Cells.Add(ccAmount);
            row.Cells.Add(ccDate);
            row.Cells.Add(ccInterest);

            ledgerBody.Controls.Add(row);
        }

        // ✅ Totals
       

        // ✅ Profit / Loss
       
        // ✅ Closing Balance
        
      
        pnlLedger.Visible = true;
    }

    // Close Account
    protected void btnCloseAccount_Click(object sender, EventArgs e)
    {
        lblCloseMsg.Text = "";

        int partyId = Convert.ToInt32(lblpid.Text);

        decimal closingBalance = Math.Abs(
            decimal.Parse(lblClosingBalance.Text.Replace("₹", "").Trim())
        );

        if (closingBalance == 0)
        {
            lblCloseMsg.Text = "✅ Account already settled!";
            return;
        }
        decimal amount = Convert.ToDecimal(lbllastbalance.Text);
        decimal monthlyRate = Convert.ToDecimal(lblrate.Text);
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
            lblCloseMsg.Text = "Please enter valid date in dd-MM-yyyy format";
            return;
        }

        if (toDate < fromDate)
        {
            lblCloseMsg.Text = "To date must be greater than from date";
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

        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            cnn.Open();

            SqlCommand cmdupdate = new SqlCommand(@"update Transactions set IsYearEnd=1  where PersonID=" + partyId + "", cnn);

            cmdupdate.ExecuteNonQuery();

            SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Transactions
        (PersonID, Type, Amount, InterestAmount, TransactionFromDate, IsYearEnd,mode,per,Closedate)
        VALUES (" + partyId + ", 1,'" + amount + "'," + interest + " ,'" + fromDate + "' ,  0,'Debit','" + lblrate.Text + "','" + toDate + "')", cnn);


            cmd.ExecuteNonQuery();


         
            cnn.Close();
          
        }
        Response.Redirect("ProfitLoss.aspx");
      
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