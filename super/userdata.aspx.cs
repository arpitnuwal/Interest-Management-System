using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userdata : System.Web.UI.Page
{
    ClsConnection Cnn = new ClsConnection();
    public enum MessageType { Success, Error, Info, Warning };
    string filename, filename1, filename2, filename3, filename4;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            list();
        //    txtloandate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
          
        }

    }

    public void list()
    {

        DataTable dtable = Cnn.FillTable("select *  from   customerlist Where 1=1  and id=" + Request.QueryString["id"] + "", "Order");
        txtaddress.Text = dtable.Rows[0]["address"].ToString();
        txtdescription.Text = dtable.Rows[0]["descr"].ToString();
        txtinterestrate.Text = dtable.Rows[0]["interestrate"].ToString();
        txtKistNumber.Text = dtable.Rows[0]["kistnumber"].ToString();
        txtloanamount.Text = dtable.Rows[0]["amount"].ToString();
        txtloandate.Text = dtable.Rows[0]["loandate"].ToString();
        txtmobile.Text = dtable.Rows[0]["mobile"].ToString();
        txtname.Text = dtable.Rows[0]["name"].ToString();
        txtkistamount.Text = dtable.Rows[0]["kistamount"].ToString();

        Image1.ImageUrl = "~/img/user/photo/" + dtable.Rows[0]["img1"].ToString();
        Image2.ImageUrl = "~/img/user/aadhar/" + dtable.Rows[0]["img2"].ToString();


        txtname2.Text = dtable.Rows[0]["name2"].ToString();
        txtmobile2.Text = dtable.Rows[0]["mobile2"].ToString();

        txtname3.Text = dtable.Rows[0]["name3"].ToString();
        txtmobile3.Text = dtable.Rows[0]["mobile3"].ToString();

        txtname4.Text = dtable.Rows[0]["name4"].ToString();
        txtmobile4.Text = dtable.Rows[0]["mobile4"].ToString();


        txtname5.Text = dtable.Rows[0]["name5"].ToString();
        txtmobile5.Text = dtable.Rows[0]["mobile5"].ToString();
    }




    protected void Button1_Click(object sender, EventArgs e)
    {
        string nid = "";
        Cnn.Open();

        string count = Cnn.ExecuteScalar("select count(*)  from   customerlist  where id!=" + Request.QueryString["id"] + " and id>" + Request.QueryString["id"] + " and LEFT(name, 1) = '" + Request.QueryString["word"] + "'").ToString();
        if (count != "0")
        {
            nid = Cnn.ExecuteScalar("select id  from   customerlist  where id!=" + Request.QueryString["id"] + "  and id>" + Request.QueryString["id"] + "   and LEFT(name, 1) = '" + Request.QueryString["word"] + "'  order by id asc").ToString();
            Response.Redirect("userdata.aspx?id=" + nid + "&&word="+Request.QueryString["word"]+""); Cnn.Close();
        }
        else
        {
            lblmsg.Text = "No Data Found";
        
        }
      
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void back_Click(object sender, EventArgs e)
    {
        string nid = "";
        Cnn.Open();

        string count = Cnn.ExecuteScalar("select count(*)  from   customerlist  where id!=" + Request.QueryString["id"] + " and id<" + Request.QueryString["id"] + " and LEFT(name, 1) = '" + Request.QueryString["word"] + "'").ToString();
        if (count != "0")
        {
            nid = Cnn.ExecuteScalar("select id  from   customerlist  where id!=" + Request.QueryString["id"] + "  and id<" + Request.QueryString["id"] + "   and LEFT(name, 1) = '" + Request.QueryString["word"] + "'  order by id desc").ToString();
            Response.Redirect("userdata.aspx?id=" + nid + "&&word=" + Request.QueryString["word"] + ""); Cnn.Close();
        }
        else
        {
            lblmsg.Text = "No Data Found";

        }
      
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        Cnn.Open();
        Cnn.ExecuteNonQuery("update customerlist set name='" + txtname.Text + "',address=N'" + txtaddress.Text + "',descr=N'" + txtdescription.Text + "' where id=" + Request.QueryString["id"] + "");
        Cnn.Close();
    }
}