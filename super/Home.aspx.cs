using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class worker_Default : System.Web.UI.Page
{
    public static string currentdate = "";
    ClsConnection Cnn = new ClsConnection();
    public static string TotalCustomer = "0", TodayPremium = "0", TodayCollection = "0", TodayOutstanding = "0", name = "0", TotalInvestorCr = "0", TotalInvestorDr = "0", TotalDueInstallment = "0", TotalForClose = "0", TotalCollection = "0", TotalLoanamount="0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Admin_id"] == null)
        {
            Response.Redirect("login.aspx");
        }
       
      
    }


  
   
}