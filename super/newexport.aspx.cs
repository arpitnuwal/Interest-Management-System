using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class newexport : System.Web.UI.Page
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
        if (!IsPostBack)
            {
                LoadInsertScript();
            }
    }


    private void LoadInsertScript()
        {
            string conStr = ConfigurationManager.ConnectionStrings["cfmsecurities"].ConnectionString;

            string sql = @"
SELECT
'INSERT [dbo].[customerlist]
([id],[name],[mobile],[loandate],[amount],[address],[interestrate],
 [kistamount],[kistnumber],[img1],[img2],[descr],[rts],[status],
 [name2],[mobile2],[name3],[mobile3],[name4],[mobile4],[name5],[mobile5])
VALUES (' +
CAST(id AS varchar) + ', ' +
'N''' + ISNULL(REPLACE(name,'''',''''''), '') + ''', ' +
'N''' + ISNULL(REPLACE(mobile,'''',''''''), '') + ''', ' +
CASE 
    WHEN loandate IS NULL THEN 'NULL'
    ELSE 'N''' + CONVERT(varchar, loandate, 103) + ''''
END + ', ' +
'N''' + ISNULL(amount,'') + ''', ' +
'N''' + ISNULL(REPLACE(address,'''',''''''), '') + ''', ' +
'N''' + ISNULL(interestrate,'') + ''', ' +
'N''' + ISNULL(kistamount,'') + ''', ' +
'N''' + ISNULL(kistnumber,'') + ''', ' +
'N''' + ISNULL(img1,'') + ''', ' +
'N''' + ISNULL(img2,'') + ''', ' +
'N''' + ISNULL(REPLACE(descr,'''',''''''), '') + ''', ' +
'N''' + CONVERT(varchar, rts, 100) + ''', ' +
CAST(status AS varchar) + ', ' +
'N''' + ISNULL(name2,'') + ''', ' +
'N''' + ISNULL(mobile2,'') + ''', ' +
'N''' + ISNULL(name3,'') + ''', ' +
'N''' + ISNULL(mobile3,'') + ''', ' +
'N''' + ISNULL(name4,'') + ''', ' +
'N''' + ISNULL(mobile4,'') + ''', ' +
'N''' + ISNULL(name5,'') + ''', ' +
'N''' + ISNULL(mobile5,'') + ''' );'
AS InsertQuery
FROM customerlist;";

            StringBuilder sb = new StringBuilder();

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    sb.AppendLine(dr["InsertQuery"].ToString());
                }
            }

            txtInsertQuery.Text = sb.ToString();
        }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string content = txtInsertQuery.Text;

        Response.Clear();
        Response.ContentType = "text/plain";
        Response.AddHeader(
            "Content-Disposition",
            "attachment; filename=customerlist_insert.txt"
        );

        Response.Write(content);
        Response.End();
    }
}







  

   
