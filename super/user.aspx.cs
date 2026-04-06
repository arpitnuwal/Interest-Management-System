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

public partial class user : System.Web.UI.Page
{
    ClsConnection Cnn = new ClsConnection();
    public enum MessageType { Success, Error, Info, Warning };
    string filename, filename1, filename2, filename3, filename4;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            txtloandate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
           
        }

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

            }
        }
        else
        {

          //  LblErr.Text = "Select customerlist photo"; return;
        
        }
        if (aadharphoto.HasFile) 
        {
            filename1 = ID + ".jpg";
            if (aadharphoto.HasFile)
            {
                VaryQualityLevel(aadharphoto.PostedFile.InputStream, filename1, "~/img/user/aadhar/");

            }
        }

        Cnn.ExecuteNonQuery("INSERT INTO [customerlist] (id,name,mobile,loandate,amount,address,interestrate,kistamount,kistnumber,img1,img2,descr,rts,status,name2,mobile2,name3,mobile3,name4,mobile4,name5,mobile5) values ('" + ID + "','" + txtname.Text.Trim().Replace("'", "''").ToUpper() + "','" + txtmobile.Text.Trim().Replace("'", "''") + "','" + txtloandate.Text.Trim().Replace("'", "''") + "','" + txtloanamount.Text.Trim().Replace("'", "''") + "',N'" + txtaddress.Text.Trim().Replace("'", "''").ToUpper() + "','" + txtinterestrate.Text + "','" + txtkistamount.Text + "','" + txtKistNumber.Text + "','" + filename + "','" + filename1 + "',N'" + txtdescription.Text.Trim().Replace("'", "''") + "',getdate(),1,'" + txtname2.Text + "','" + txtmobile2.Text + "','" + txtname3.Text + "','" + txtmobile3.Text + "','" + txtname4.Text + "','" + txtmobile4.Text + "','" + txtname5.Text + "','" + txtmobile5.Text + "')");
       
            LblErr.Text = "Data Submitted!!";
           
        Cnn.Close();
        clear();

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


  
}