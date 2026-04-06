using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    ClsConnection Cnn = new ClsConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Txtusername.Text == "")
        {
            LblErr.Text = "Please Enter User Name";
            return;
        }

        if (Txtpassword.Text == "")
        {
            LblErr.Text = "Please Enter Password";
            return;
        }
        DataSet ds = new DataSet();
        Cnn.Open();
        Cnn.FillDataSet(ds, "select * from AdminLogin  where UserName = '" + Txtusername.Text.Replace("'", "''") + "'COLLATE SQL_Latin1_General_CP1_CS_AS and Password = '" + Txtpassword.Text.Replace("'", "''") + "'", "Login");
        Cnn.Close();
        if (ds.Tables[0].Rows.Count == 0)
        {
            LblErr.Text = "Please Enter Correct Username & Password";
        }
        else
        {
            LblErr.Text = "";

            Session["Admin_id"] = ds.Tables[0].Rows[0]["id"].ToString();
          //   monthlyrecordnew();
          //  tenureapply();
            Response.Redirect("user.aspx");

        }
    }


    public void monthlyrecordnew()
    {
        var startDate = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        string[] dtname = startDate.Split('/');
        string sname = "";
        if (!string.IsNullOrEmpty(startDate))
        {
            sname = dtname[0];
        }

        if (sname == "01")
        {
            Cnn.Open();

            string count = Cnn.ExecuteScalar("select count(*) from monthlyduerecord where datein='" + startDate + "'").ToString();

            if (count == "0")
            {
                DataTable dtable = Cnn.FillTable("select *  from   customer Where 1=1 and accountclose=0   Order by id asc", "Order");
                if (dtable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        string check = Cnn.ExecuteScalar("select count(*)  from customerallentry  where cid=" + dtable.Rows[i]["id"] + " and method='Cr' and JoiningDate BETWEEN Convert(Datetime, '" + DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy") + "' ,104) AND Convert(Datetime, '" + DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy") + "' ,104)+1").ToString();

                            if (check == "0")
                            {                              
                               
                                string penaltyamount = Cnn.ExecuteScalar("select penaltycharge FROM customer WHERE id=" + dtable.Rows[i]["id"] + "").ToString();
                                string pamount = Cnn.ExecuteScalar("select monthlyamount FROM customer WHERE id=" + dtable.Rows[i]["id"] + "").ToString();

                                //
                                int id = Convert.ToInt32(Cnn.ExecuteScalar("select isnull(max(id),0) + 1 from customerallentry"));
                                Cnn.ExecuteNonQuery("insert into [customerallentry] (id,cid,Client,Amount,Date,JoiningDate,Active,Type,rts,method,penalty) values ('" + id + "','" + dtable.Rows[i]["id"] + "','" + dtable.Rows[i]["name"] + "','" + penaltyamount + "','" + DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/") + "',getdate(),1,'Due Amount Add in Next premium',getdate(),'Dr',1)");

                              
                            

                              


                               


                                Cnn.ExecuteNonQuery("insert into tenurerecordinterstedbyuser (userid,amount,rts) values ('" + dtable.Rows[i]["id"] + "','" + pamount + "',getdate())");
                            }                        
                       
                    }
                }

                int monthid = Convert.ToInt32(Cnn.ExecuteScalar("Select  IsNull(Max(id)+1,1) From [monthlyduerecord]"));
                Cnn.ExecuteNonQuery("insert into monthlyduerecord (id,datein,rts) values ('" + monthid + "','" + startDate + "',getdate())");
            }

            Cnn.Close();
        }

    }

    public void monthlyrecord()
    {
        var startDate = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        string[] dtname = startDate.Split('/');
        string sname = "";
        if (!string.IsNullOrEmpty(startDate))
        {
            sname = dtname[0];
        }

        if (sname == "01")
        {
            Cnn.Open();

            string count = Cnn.ExecuteScalar("select count(*) from monthlyduerecord where datein='" + startDate + "'").ToString();

            if (count == "0")
            {
                DataTable dtable = Cnn.FillTable("select *  from   customer Where 1=1 and accountclose=0   Order by id asc", "Order");
                if (dtable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        string thispidcount = Cnn.ExecuteScalar("select count(*) from  customerpremiumlist  where MONTH(premiumdate)='" + dtname[1].Remove(0, 1) + "' and userid=" + dtable.Rows[i]["id"] + " and status=0  and  depositamount=0").ToString();
                        if (thispidcount != "0")
                        {
                            string thispid = Cnn.ExecuteScalar("select Id from  customerpremiumlist  where MONTH(premiumdate)='" + dtname[1].Remove(0, 1) + "' and userid=" + dtable.Rows[i]["id"] + " and status=0 order by id asc").ToString();
                            string check = Cnn.ExecuteScalar("select  count(*) from  customerpremiumlist where   MONTH(premiumdate)!='" + dtname[1].Remove(0, 1) + "' and MONTH(premiumdate)!='" + dtname[1].Remove(0, 1) + "'    and status=0 and depositamount=0 and userid=" + dtable.Rows[i]["id"] + " and id<" + thispid + "   ").ToString();

                            if (check != "0")
                            {
                                string thismonthid = Cnn.ExecuteScalar("select  top 1 *   from  customerpremiumlist where   MONTH(premiumdate)!='" + dtname[1].Remove(0, 1) + "' and MONTH(premiumdate)!='" + dtname[1].Remove(0, 1) + "'    and status=0 and depositamount=0 and userid=" + dtable.Rows[i]["id"] + " and id<" + thispid + "  order by id desc").ToString();



                                string firmid = Cnn.ExecuteScalar("select firmid FROM customer WHERE id=" + dtable.Rows[i]["id"] + "").ToString();
                                string fileid = Cnn.ExecuteScalar("select filenumber FROM customer WHERE id=" + dtable.Rows[i]["id"] + "").ToString();
                                string penaltyamount = Cnn.ExecuteScalar("select penaltycharge FROM customer WHERE id=" + dtable.Rows[i]["id"] + "").ToString();
                                string pamount = Cnn.ExecuteScalar("select monthlyamount FROM customer WHERE id=" + dtable.Rows[i]["id"] + "").ToString();


                                int id = Convert.ToInt32(Cnn.ExecuteScalar("select isnull(max(id),0) + 1 from customerallentry"));

                                Cnn.ExecuteNonQuery("insert into [customerallentry] (id,cid,Client,Amount,Date,JoiningDate,Active,Type,rts,method,penalty) values ('" + id + "','" + dtable.Rows[i]["id"] + "','" + dtable.Rows[i]["name"] + "','" + penaltyamount + "','" + DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/") + "',getdate(),1,'Due Amount Add in Next premium',getdate(),'Dr',1)");

                                //
                                string nextid = Cnn.ExecuteScalar("select TOP 1 ID FROM customerpremiumlist WHERE userid=" + dtable.Rows[i]["id"] + " AND STATUS=0  and  duecondtion !=1  and id !=" + thismonthid + " ORDER BY ID ASC ").ToString();

                                Cnn.ExecuteNonQuery("Update customerpremiumlist set duecondtion=1 where id='" + thismonthid + "'");


                                string totalkist = Cnn.ExecuteScalar("select count(*) from customerpremiumlist where userid=" + dtable.Rows[i]["id"] + " and id=" + thismonthid + "").ToString();
                                string date = Cnn.ExecuteScalar("select convert(varchar, premiumdate, 103) from customerpremiumlist where userid=" + dtable.Rows[i]["id"] + "  order by id desc").ToString();
                             
                                string mdate = "SELECT DATEADD(month, 1, convert(datetime,'" + date + "',103)) AS DateAdd";

                               
                                Cnn.ExecuteNonQuery("insert into tenurerecordinterstedbyuser (userid,amount,rts) values ('" + dtable.Rows[i]["id"] + "','" + pamount + "',getdate())");
                            }
                        }

                        //lstmiscamtamount = lstmiscamtamount + Convert.ToInt32(DTH4.Rows[i]["Amount"]);
                     
                    }
                }

                int monthid = Convert.ToInt32(Cnn.ExecuteScalar("Select  IsNull(Max(id)+1,1) From [monthlyduerecord]"));
                Cnn.ExecuteNonQuery("insert into monthlyduerecord (id,datein,rts) values ('" + monthid + "','" + startDate + "',getdate())");
            }

            Cnn.Close();
        }

    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }





    public void tenureapply()
    {
        var startDate = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        string[] dtname = startDate.Split('/');
        string sname = "";
        if (!string.IsNullOrEmpty(startDate))
        {
            sname = dtname[0];
        }

        if (sname == "30")
        {
            Cnn.Open();
            string count = Cnn.ExecuteScalar("select count(*) from tenurerecord where datein='" + startDate + "'").ToString();

            if (count == "0")
            {



                DataTable dtable = Cnn.FillTable("select * from customer where accountclose=0   order by id asc", "Order");
                if (dtable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        string lastkistcheck = Cnn.ExecuteScalar("SELECT count(*) FROM customerpremiumlist  where userid=" + dtable.Rows[i]["id"] + " and status=0 ").ToString();

                        if (lastkistcheck != "0")
                        {

                            string lastkist = Cnn.ExecuteScalar("SELECT top 1 premiumdate FROM customerpremiumlist  where userid=" + dtable.Rows[i]["id"] + " and status=0 order by id desc").ToString();

                            string pamount = Cnn.ExecuteScalar("SELECT isnull(sum(pendingamount),'0') as pendingamount   FROM customerpremiumlist  where  '" + lastkist + "'< getdate() and userid=" + dtable.Rows[i]["id"] + " and status=0").ToString();

                            if (pamount != "0")
                            {
                                int idd = Convert.ToInt32(Cnn.ExecuteScalar("select isnull(max(id),0) + 1 from customerallentry"));
                                int thisamount = (Convert.ToInt32(pamount) * Convert.ToInt32(dtable.Rows[0]["interestrate"]) / 100);
                                int allamouunt = thisamount + Convert.ToInt32(pamount);


                               // Cnn.ExecuteNonQuery("update customerpremiumlist set status=1 where userid=" + dtable.Rows[i]["id"] + "");

                              //  Cnn.ExecuteNonQuery("insert into [customerallentry] (id,cid,Client,Amount,Date,JoiningDate,Active,Type,rts,method,penalty,premiumid) values ('" + idd + "','" + dtable.Rows[i]["id"] + "','" + dtable.Rows[0]["name"] + "','" + allamouunt + "','" + DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/") + "',convert(datetime,getdate(),103),1,'interest add with old amount',getdate(),'Dr',1,0)");

                                //
                                string uid = dtable.Rows[i]["id"].ToString();

                                int cplID = Convert.ToInt32(Cnn.ExecuteScalar("Select  IsNull(Max(id)+1,1) From [customerpremiumlist]"));
                                string lastdate = Cnn.ExecuteScalar("select top 1 convert(varchar, premiumdate, 103) as monthdate from customerpremiumlist where userid='" + dtable.Rows[i]["id"] + "' order by id desc").ToString();


                                string date = "convert(datetime,'" + lastdate + "',103)";
                                string mdate = "SELECT DATEADD(month,1," + date + ") AS DateAdd";





                              //  Cnn.ExecuteNonQuery("insert into customerpremiumlist(id,userid,premiumdate,amount,status,rts,duecondtion,depositamount,pendingamount,oldpremiumid) values('" + cplID + "'," + dtable.Rows[i]["id"] + ",(" + mdate + "),'" + allamouunt + "',0,getdate(),0,0,'" + allamouunt + "','0')");



                             //   Cnn.ExecuteNonQuery("insert into tenurerecordbyuser (userid,amount,rts) values ('" + dtable.Rows[i]["id"] + "','" + allamouunt + "',getdate())");

                            }
                        }
                    }
                }

             //   int monthid = Convert.ToInt32(Cnn.ExecuteScalar("Select  IsNull(Max(id)+1,1) From [tenurerecord]"));
              //  Cnn.ExecuteNonQuery("insert into tenurerecord (id,datein,rts) values ('" + monthid + "','" + startDate + "',getdate())");
            }



            Cnn.Close();



            //



        }

    }
}