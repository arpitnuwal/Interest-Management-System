using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class newimport : System.Web.UI.Page
{
    private string connString = System.Configuration.ConfigurationManager.ConnectionStrings["cfmsecurities"].ConnectionString;

    ClsConnection Cnn = new ClsConnection();
    public enum MessageType { Success, Error, Info, Warning };
    string filename;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Admin_id"] == null)
        {
            Response.Redirect("login.aspx");
        }
       
    }


    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string insertQuery = txtInsertQuery.Text.Trim();

        if (string.IsNullOrEmpty(insertQuery))
        {
            return;
        }

        string conStr = ConfigurationManager.ConnectionStrings["cfmsecurities"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conStr))
        {

            using (SqlCommand cmd1 = new SqlCommand("DELETE FROM customerlist", con))
            {
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }

            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        lblmsg.Text = "Success"; txtInsertQuery.Text = "";
    }






  

   
}