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

public partial class customerlist : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {

         
            LoadData();
            Session["msgs"] = null;
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }

    protected void LstOrder_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Cnn.Open();


        if (e.CommandName == "deactive")
        {
            Label LblOrderId = (Label)e.Item.FindControl("lblid");

            Cnn.ExecuteNonQuery("update [newsdetails] set  active=0 where newsid=" + LblOrderId.Text + "");



        }
        if (e.CommandName == "active")
        {
            Label LblOrderId = (Label)e.Item.FindControl("lblid");

            Cnn.ExecuteNonQuery("update [newsdetails] set active=1 where newsid=" + LblOrderId.Text + "");



        }

        if (e.CommandName == "delete")
        {
            Label LblOrderId = (Label)e.Item.FindControl("lblid");

            Cnn.ExecuteNonQuery("delete from customerlist where id=" + LblOrderId.Text + "");
          



          //  Cnn.ExecuteNonQuery("update [newsdetails] set active=1 where newsid=" + LblOrderId.Text + "");



        }


      
        if (e.CommandName == "Delete")
        {
            Label LblOrderId = (Label)e.Item.FindControl("lblid");
            Label lblimage = (Label)e.Item.FindControl("lblimage");
            if (lblimage.Text.Length > 0)
            {
                File.Delete(Server.MapPath("~/img/news/" + lblimage.Text + ""));

            }
            // Cnn.ExecuteNonQuery("update tablecounter set Count = Count + 1 where tablename='newsdetails'");
            Cnn.ExecuteNonQuery("delete from [customerlist] where newsid='" + LblOrderId.Text + "'");
            Cnn.ExecuteNonQuery("delete from [customerlist] where blogid='" + LblOrderId.Text + "'");
            ShowMessage("Record Delete successfully", MessageType.Success);
            LoadData();


        }




        LoadData();
        Cnn.Close();
    }

 

    private void LoadData()
    {
        string Cond_ = "", mobile = "",name="",firm="";

       
        if (txtmobile.Text != "")
        {
            mobile = "AND mobile like '%" + txtmobile.Text.Trim() + "%'";
        }
        if (txtname.Text != "")
        {
            mobile = "AND Name like '%" + txtname.Text.Trim() + "%'";
        }
       
        Cnn.Open();
        DataTable dtable = Cnn.FillTable("select *  from   customerlist Where 1=1 " + Cond_ + " " + mobile + " " + name + " " + firm + " Order by id desc", "Order");
        LstOrder.DataSource = dtable;
        LstOrder.DataBind();

        //pnlOrderInfo.Visible = false;
        Cnn.Close();

    }

    protected void LstOrder_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        LoadData();
    }

    protected void LstOrder_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }

    protected string setclass(bool active)
    {
        string classToApply = string.Empty;
        if (active == true)
        {
            classToApply = "normal";
        }
        else if (active == false)
        {
            classToApply = "red";
        }

        return classToApply;

    }

    protected void Imgbtn_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void LstCategory_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {

    }
    protected void LstCategory_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

    }
    protected void Imgbtnstore_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void LstuserDetail_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

    }

    protected void BtnClose_Click(object sender, EventArgs e)
    {
        //DivProducts.Visible = true;

    }
    protected void LstOrder_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {

    }
}