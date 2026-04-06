using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class detailshow : System.Web.UI.Page
{
    ClsConnection Cnn = new ClsConnection();
    public enum MessageType { Success, Error, Info, Warning };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Admin_id"] == null)
        {
            Response.Redirect("login.aspx");
        }
       

      
        if (Session["msgs"] != null)
        {
            ShowMessage(Session["msgs"].ToString(), MessageType.Success);
        }
       
    }

  
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }


    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        if (txtname.Text.Trim() != "" && txtname.Text.Trim() != null)
        {
            Cnn.Open();
            string check = Cnn.ExecuteScalar("select count(*) from customerlist where  name='" + txtname.Text.Trim() + "'").ToString();

            if (check == "1")
            {
                string id = Cnn.ExecuteScalar("select id from customerlist where  name='" + txtname.Text.Trim() + "'").ToString();

                Response.Redirect("userdata.aspx?id=" + id + "");
            }
            Cnn.Close();
        }
    }
}