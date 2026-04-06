using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for sellproduct
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
public class sellproduct : System.Web.Services.WebService {

    public sellproduct()
    {        
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //--------------------ProductSearch-----------------------------------
   

    [WebMethod]
    public string[] Searchbyname(string prefixText, int count)
    {
        //if (Session["paid"] == "and Paid='Debit'")
        //{
        ClsConnection Cnn = new ClsConnection();
        count = 10;
        string[] str;
        string SearchWord = "";
        char[] C = { ' ' };
        str = prefixText.Split(C, StringSplitOptions.RemoveEmptyEntries);
        int i;
        for (i = 0; i < str.Length; i++)
        {
            SearchWord = SearchWord + str[i];
            if (i == str.Length - 1) break;
            SearchWord = SearchWord + " near ";
        }
        SearchWord = SearchWord.Replace(".", " ");
        SearchWord = SearchWord.Replace(",", " ");
        SearchWord = SearchWord.Replace(")", " ");
        SearchWord = SearchWord.Replace("(", " ");
        string sql = "Select top 50 'keywords'= name from Party where  name like '%" + SearchWord + "%'   order by keywords";
        DataTable dt = Cnn.FillTable(sql, "dt");
        string[] items = new string[dt.Rows.Count];
        i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["keywords"].ToString().ToUpper() + "", i);
            i++;
        }
        return items;
        //}

    }

  
   }