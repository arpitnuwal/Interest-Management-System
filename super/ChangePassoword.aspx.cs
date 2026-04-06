using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePassoword : System.Web.UI.Page
{
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
            list();
        }
    }

    public void list()
    {
        Cnn.Open();
        Txtold.Text = Cnn.ExecuteScalar("select Password from [adminLogin] where id=" + Session["Admin_id"] + "").ToString();
        Cnn.Close();
    
    }
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
      
                Cnn.Open();
                Cnn.ExecuteNonQuery("update [AdminLogin] set Password='" + Txtnews.Text.Replace("'", "''") + "'  where id=" + Session["Admin_id"] + "");
                Cnn.Close();
            
            clear();
            list();
            ShowMessage("Password Change successfully", MessageType.Success);
         
    }

    public void clear()
    {
        Txtnews.Text = "";
       
    }
  
}