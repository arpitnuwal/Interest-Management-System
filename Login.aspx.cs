using System;
using System.Data.SqlClient;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

    protected void btnLogin_Click(object sender, EventArgs e)
    {
      
            con.Open();

            string query = "SELECT COUNT(*) FROM Admin WHERE Username=@u AND Password=@p";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@u", txtUser.Text);
            cmd.Parameters.AddWithValue("@p", txtPass.Text);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count == 1)
            {
                Session["admin"] = txtUser.Text;
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                lblMsg.Text = "❌ Invalid Username or Password";
            }

            con.Close();
        
    }
}