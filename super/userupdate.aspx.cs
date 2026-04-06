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

public partial class userupdate : System.Web.UI.Page
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
        //////////insert





        Cnn.Open();
        int ID = Convert.ToInt32(Cnn.ExecuteScalar("Select  IsNull(Max(id)+1,1) From [customerlist]"));

        if (customerphoto.HasFile)
        {
            filename = ID + ".jpg";
            if (customerphoto.HasFile)
            {
                VaryQualityLevel(customerphoto.PostedFile.InputStream, filename, "~/img/user/photo/");
                Cnn.ExecuteNonQuery("update customerlist set img1='" + filename + "' where id=" + Request.QueryString["id"] + "");

            }
        }
       
        if (aadharphoto.HasFile)
        {
            filename1 = ID + ".jpg";
            if (aadharphoto.HasFile)
            {
                VaryQualityLevel(aadharphoto.PostedFile.InputStream, filename1, "~/img/user/aadhar/");
                Cnn.ExecuteNonQuery("update customerlist set img2='" + filename + "' where id=" + Request.QueryString["id"] + "");

            }
        }
        Cnn.ExecuteNonQuery("update customerlist set name='" + txtname.Text + "',mobile='" + txtmobile.Text + "',loandate='" + txtloandate.Text + "',amount='" + txtloanamount.Text + "',address=N'" + txtaddress.Text + "',interestrate='" + txtinterestrate.Text + "',kistamount='" + txtkistamount.Text + "',kistnumber='" + txtKistNumber.Text + "',descr=N'" + txtdescription.Text + "',name2='" + txtname2.Text + "',mobile2='" + txtmobile2.Text + "',name3='" + txtname3.Text + "',mobile3='" + txtmobile3.Text + "',name4='" + txtname4.Text + "',mobile4='" + txtmobile4.Text + "',name5='" + txtname5.Text + "',mobile5='" + txtmobile5.Text + "' where id=" + Request.QueryString["id"] + "");
        

        LblErr.Text = "Data Update!!";

        Cnn.Close();
        clear();
        list();
        ShowMessage("Record submitted successfully", MessageType.Success);
    }

    private void VaryQualityLevel(Stream stream, string fname, string addresimg)
    {



        //  size 
        System.Drawing.Image photo = new Bitmap(stream);
        //Bitmap bmp1 = new Bitmap(photo, 119, 83);

        Bitmap bmp1 = new Bitmap(photo, 400, 400);
        // without size
        //  Bitmap bmp1 = new Bitmap(stream);
        ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
        EncoderParameters myEncoderParameters = new EncoderParameters(1);
        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 40L);
        myEncoderParameters.Param[0] = myEncoderParameter;
        bmp1.Save(Server.MapPath(addresimg + fname), jgpEncoder, myEncoderParameters);
        bmp1.Dispose();

    }
    private ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }
        return null;

    }


    public void clear()
    {
        txtname.Text = "";

        txtloanamount.Text = "";
        txtinterestrate.Text = "";

        txtmobile.Text = "";
        txtaddress.Text = "";
        txtaddress.Text = "";
        txtdescription.Text = "";
        txtinterestrate.Text = "";
        txtkistamount.Text = "";

        txtKistNumber.Text = "";

        txtloanamount.Text = "";
        txtloandate.Text = "";
        txtmobile.Text = "";
        txtname.Text = "";




    }
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }



    protected void Button2_Click(object sender, EventArgs e)
    {
        Cnn.Open();
       
        Cnn.ExecuteNonQuery("delete from customerlist where id=" + Request.QueryString["id"] + "");


      

        Cnn.Close();

        Response.Redirect("Modification.aspx");
    }
}