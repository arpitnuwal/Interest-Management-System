using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class worker_Admin : System.Web.UI.MasterPage
{
    private string connString = System.Configuration.ConfigurationManager.ConnectionStrings["cfmsecurities"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }


    protected string SetCssClass(string page)
    {
        return Request.Url.AbsolutePath.ToLower().EndsWith(page.ToLower()) ? "color:white;background-color:green" : "";
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["Admin_id"] = null;

        Response.Redirect("login.aspx");

    }





    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["cfmsecurities"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connString))
        using (SqlCommand cmd = new SqlCommand(query, con))
        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        {
            try
            {
                con.Open();
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                // Log error (consider using logging framework)
                throw new Exception("Database query error: " + ex.Message);
            }
        }

        return dt;
    }
    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        string query = "SELECT * from customerlist";

        using (DataTable dt = GetData(query))
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment; filename=Export.csv");

                StringBuilder sb = new StringBuilder();
                sb.Append("\uFEFF"); // Add BOM to ensure UTF-8 encoding is recognized correctly

                // Add column names
                sb.AppendLine(string.Join(",", dt.Columns.Cast<DataColumn>().Select(col => col.ColumnName)));

                // Add row data
                foreach (DataRow row in dt.Rows)
                {
                    List<string> fields = new List<string>();
                    foreach (object field in row.ItemArray)
                    {
                        string value = field.ToString().Replace("\"", "\"\""); // Escape double quotes
                        if (value.Contains(",") || value.Contains("\n") || value.Contains("\r"))
                        {
                            value = "\"" + value + "\""; // Enclose in quotes if necessary
                        }
                        fields.Add(value);
                    }
                    sb.AppendLine(string.Join(",", fields));
                }

                Response.Write(sb.ToString());
                Response.End();
            }
        }

    }

    //private DataTable GetData(string query)
    //{
    //    DataTable dt = new DataTable();
    //    using (SqlConnection con = new SqlConnection(connString))
    //    using (SqlCommand cmd = new SqlCommand(query, con))
    //    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
    //    {
    //        try
    //        {
    //            con.Open();
    //            sda.Fill(dt);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Database query error: " + ex.Message);
    //        }
    //    }
    //    return dt;
    //}

    //protected void ExportToExcel(object sender, EventArgs e)
    //{
    //    string query = "SELECT * FROM customerlist";
    //    DataTable dt = GetData(query);
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        string filePath = @"D:\ExportedData.xlsx"; // Change to actual pen drive location
    //        using (ExcelPackage pck = new ExcelPackage())
    //        {
    //            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Customer List");
    //            ws.Cells[1, 1].LoadFromDataTable(dt, true);
    //            File.WriteAllBytes(filePath, pck.GetAsByteArray());
    //        }
    //        Response.Write("File exported successfully to " + filePath);
    //    }
    //}


    //protected void LinkButton1_Click1(object sender, EventArgs e)
    //{
    //    string query = "SELECT * FROM customerlist";
    //    DataTable dt = GetData(query);
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        string filePath = @"D:\ExportedData.xlsx"; // Change to actual pen drive location
    //        using (ExcelPackage pck = new ExcelPackage())
    //        {
    //            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Customer List");
    //            ws.Cells[1, 1].LoadFromDataTable(dt, true);
    //            File.WriteAllBytes(filePath, pck.GetAsByteArray());
    //        }
    //        Response.Write("File exported successfully to " + filePath);
    //    }
    //}




}

