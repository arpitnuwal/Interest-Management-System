using System;
using System.Data.SqlClient;
using System.Configuration;

public partial class Dashboard : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    void LoadData()
    {
        con.Open();

        // 1 = Debit, 2 = Credit
        lblDebit.Text = "₹ " + GetValue("SELECT ISNULL(SUM(Amount),0) FROM Transactions WHERE Type=1");
        lblCredit.Text = "₹ " + GetValue("SELECT ISNULL(SUM(Amount),0) FROM Transactions WHERE Type=2");

        // Interest column fix
        lblInterest.Text = "₹ " + GetValue("SELECT ISNULL(SUM(InterestAmount),0) FROM Transactions");

        con.Close();
    }

    double GetValue(string query)
    {
        SqlCommand cmd = new SqlCommand(query, con);
        return Convert.ToDouble(cmd.ExecuteScalar());
    }
}