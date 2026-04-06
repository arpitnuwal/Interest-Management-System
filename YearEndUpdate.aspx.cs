using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

public partial class YearEndUpdate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadClientData();
            if (DateTime.Now.Month == 12 && DateTime.Now.Day == 31)
            {
                RunYearEndUpdate();
            }

            btnYearEnd.Visible = false;
            SqlConnection con = new SqlConnection(
       ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            if (DateTime.Now.Day == 31 && DateTime.Now.Month == 12)
            {
                con.Open();


                SqlCommand checkcmd = new SqlCommand("SELECT COUNT(*) FROM yearentry WHERE DAY(GETDATE()) = 31 AND MONTH(GETDATE()) = 12;", con);
                string countcheck = checkcmd.ExecuteScalar().ToString();

                con.Close();
                

                if (countcheck == "0") { btnYearEnd.Visible = true; }

            }
        }
    }

    private void LoadClientData()
    {
        string query = @"
        SELECT 
            p.PartyID,
            p.Name AS ClientName,

            ISNULL(SUM(CASE WHEN t.Type = 2 THEN t.Amount ELSE 0 END), 0) AS TotalCR,
            ISNULL(SUM(CASE WHEN t.Type = 1 THEN t.Amount ELSE 0 END), 0) AS TotalDR,

            ISNULL(SUM(CASE WHEN t.Type = 1 THEN t.InterestAmount ELSE 0 END), 0) AS DRInterest,
            ISNULL(SUM(CASE WHEN t.Type = 2 THEN t.InterestAmount ELSE 0 END), 0) AS CRInterest,

            (
                (
                    ISNULL(SUM(CASE WHEN t.Type = 1 THEN t.Amount ELSE 0 END), 0)
                    -
                    ISNULL(SUM(CASE WHEN t.Type = 2 THEN t.Amount ELSE 0 END), 0)
                )
                +
                (
                    ISNULL(SUM(CASE WHEN t.Type = 1 THEN t.InterestAmount ELSE 0 END), 0)
                    -
                    ISNULL(SUM(CASE WHEN t.Type = 2 THEN t.InterestAmount ELSE 0 END), 0)
                )
            ) AS FinalTotal

        FROM Party p
        LEFT JOIN Transactions t 
            ON p.PartyID = t.PersonID
        GROUP BY 
            p.PartyID, p.Name
        ORDER BY 
            p.Name";

        SqlDataAdapter da = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvClients.DataSource = dt;
        gvClients.DataBind();
    }

    protected void btnRunYearEnd_Click(object sender, EventArgs e)
    {
        RunYearEndUpdate();
    }

    void RunYearEndUpdate()
    {
        try
        {
            List<int> partyIds = new List<int>();

            con.Open(); // ✅ OPEN ONLY ONCE

            // 1️⃣ Get all parties
            using (SqlCommand cmd = new SqlCommand("SELECT PartyID FROM Party", con))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    partyIds.Add(Convert.ToInt32(dr["PartyID"]));
                }
                dr.Close();
            }

            // 🔥 ✅ FIX: DATA SE YEAR NIKALO (NOT DateTime.Now)
            int lastYear = 0;

            using (SqlCommand cmdYear = new SqlCommand(
                "SELECT MAX(YEAR(TransactionFromDate)) FROM Transactions WHERE IsYearEnd=0", con))
            {
                object result = cmdYear.ExecuteScalar();
                if (result != DBNull.Value)
                    lastYear = Convert.ToInt32(result);
            }

            // 📅 Correct next year opening
            DateTime nextYearDate = new DateTime(lastYear + 1, 1, 1);

            // ❗ Duplicate check (correct year)
            using (SqlCommand checkCmd = new SqlCommand(
                "SELECT COUNT(*) FROM Transactions WHERE IsYearEnd=1 AND YEAR(TransactionFromDate)=@year",
                con))
            {
                checkCmd.Parameters.AddWithValue("@year", lastYear + 1);

                int alreadyDone = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (alreadyDone > 0)
                {
                    lblMsg.Text = "❌ Year-End already done!";
                    con.Close();
                    return;
                }
            }

            // 2️⃣ Loop all parties
            foreach (int partyId in partyIds)
            {
                decimal closingBalance = GetClosingBalance(partyId);

                if (closingBalance == 0)
                    continue;

                using (SqlCommand cmdInsert = new SqlCommand(@"
                INSERT INTO Transactions
                (PersonID, Type, Amount, InterestAmount, TransactionFromDate, IsYearEnd)
                VALUES (@p,@t,@a,@i,@d,@y)", con))
                {
                    cmdInsert.Parameters.AddWithValue("@p", partyId);

                    if (closingBalance > 0)
                    {
                        cmdInsert.Parameters.AddWithValue("@t", 1); // Debit
                        cmdInsert.Parameters.AddWithValue("@a", closingBalance);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@t", 2); // Credit
                        cmdInsert.Parameters.AddWithValue("@a", Math.Abs(closingBalance));
                    }

                    cmdInsert.Parameters.AddWithValue("@i", 0);
                    cmdInsert.Parameters.AddWithValue("@d", nextYearDate);
                    cmdInsert.Parameters.AddWithValue("@y", 1);

                    cmdInsert.ExecuteNonQuery();
                }
            }

            con.Close();

            lblMsg.Text = "✅ Year-End Done! Opening created for " + nextYearDate.Year;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "❌ Error: " + ex.Message;
        }
    }

    // ✅ FINAL CORRECT BALANCE FUNCTION
    decimal GetClosingBalance(int partyId)
    {
        decimal balance = 0;

        string query = @"SELECT Type, Amount, InterestAmount 
                         FROM Transactions 
                         WHERE PersonID=@p AND IsYearEnd=0";

        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@p", partyId);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int type = Convert.ToInt32(dr["Type"]);
                decimal amt = Convert.ToDecimal(dr["Amount"]);
                decimal intr = Convert.ToDecimal(dr["InterestAmount"]);

                if (type == 1)
                    balance += (amt + intr);
                else
                    balance -= amt;
            }

            dr.Close();
        }

        return balance;
    }
    protected void btnYearEnd_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["con"].ConnectionString);

    con.Open();
    SqlTransaction trans = con.BeginTransaction();

   
        string selectQuery = @"
        SELECT 
            p.PartyID,
            (
                (
                    ISNULL(SUM(CASE WHEN t.Type = 1 THEN t.Amount ELSE 0 END), 0)
                    -
                    ISNULL(SUM(CASE WHEN t.Type = 2 THEN t.Amount ELSE 0 END), 0)
                )
                +
                (
                    ISNULL(SUM(CASE WHEN t.Type = 1 THEN t.InterestAmount ELSE 0 END), 0)
                    -
                    ISNULL(SUM(CASE WHEN t.Type = 2 THEN t.InterestAmount ELSE 0 END), 0)
                )
            ) AS FinalTotal
        FROM Party p
        LEFT JOIN Transactions t 
            ON p.PartyID = t.PersonID
        WHERE ISNULL(t.IsYearEnd,0)=0
        GROUP BY p.PartyID";

        SqlCommand cmd = new SqlCommand(selectQuery, con, trans);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        DateTime nextYearDate = new DateTime(DateTime.Now.Year + 1, 1, 1);

        foreach (DataRow row in dt.Rows)
        {
            int partyId = Convert.ToInt32(row["PartyID"]);
            decimal finalTotal = Convert.ToDecimal(row["FinalTotal"]);

            decimal interest = finalTotal * 0.02m * 12;
            decimal newAmount = finalTotal + interest;


            string updateQuery = @"
        UPDATE Transactions
        SET IsYearEnd = 1
        WHERE ISNULL(IsYearEnd,0)=0   and PersonID=" + partyId + "";

            SqlCommand updateCmd = new SqlCommand(updateQuery, con, trans);
            updateCmd.ExecuteNonQuery();




            string insertQuery = @"
            INSERT INTO Transactions
            (PersonID, Type, Amount, InterestAmount, TransactionFromDate, IsYearEnd, mode)
            VALUES
            (@PersonID, 1, @Amount, @InterestAmount, @Date, 0, 'Debit')";

            SqlCommand insertCmd = new SqlCommand(insertQuery, con, trans);
            insertCmd.Parameters.AddWithValue("@PersonID", partyId);
            insertCmd.Parameters.AddWithValue("@Amount", finalTotal);
            insertCmd.Parameters.AddWithValue("@InterestAmount", interest);
            insertCmd.Parameters.AddWithValue("@Date", nextYearDate);

            insertCmd.ExecuteNonQuery();
        }

        SqlCommand insertyear = new SqlCommand("insert into yearentry (rts) values (getdate())", con, trans);
        insertyear.ExecuteNonQuery();
        trans.Commit();

   


        con.Close();

        Response.Write("<script>alert('Year End Successfully Done');</script>");
    
    
}
    
}