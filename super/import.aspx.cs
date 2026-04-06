using OfficeOpenXml;
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

public partial class import : System.Web.UI.Page
{
    private string connString = System.Configuration.ConfigurationManager.ConnectionStrings["cfmsecurities"].ConnectionString;

    ClsConnection Cnn = new ClsConnection();
    public enum MessageType { Success, Error, Info, Warning };
    string filename;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Admin_id"] == null)
        {
            Response.Redirect("login.aspx");
        }
       
    }


    private void BulkInsert(DataTable dt)
    {
        using (SqlConnection con = new SqlConnection(connString))
        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
        {
            con.Open();

            bulkCopy.DestinationTableName = "customerlist";
            bulkCopy.BulkCopyTimeout = 0;

            // ❌ ID column remove (IDENTITY)
            if (dt.Columns.Contains("id"))
            {
                dt.Columns.Remove("id");
            }

            // ✅ STATUS column ko BOOL bana do
            if (dt.Columns.Contains("status"))
            {
                DataColumn oldCol = dt.Columns["status"];

                if (oldCol.DataType != typeof(bool))
                {
                    DataColumn newCol = new DataColumn("status_new", typeof(bool));
                    dt.Columns.Add(newCol);

                    foreach (DataRow row in dt.Rows)
                    {
                        string v = row["status"] == null ? "" : row["status"].ToString().Trim().ToLower();

                        if (v == "1" || v == "true" || v == "yes" || v == "y")
                            row["status_new"] = true;
                        else
                            row["status_new"] = false;
                    }

                    int ordinal = oldCol.Ordinal;
                    dt.Columns.Remove(oldCol);
                    newCol.ColumnName = "status";
                    newCol.SetOrdinal(ordinal);
                }
            }

            // Empty string → NULL
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    if (row[col.ColumnName] != DBNull.Value &&
                        col.DataType == typeof(string) &&
                        string.IsNullOrWhiteSpace(row[col.ColumnName].ToString()))
                    {
                        row[col.ColumnName] = DBNull.Value;
                    }
                }
            }

            // Column mapping
            bulkCopy.ColumnMappings.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
            }

            bulkCopy.WriteToServer(dt);
        }
    }






   protected void LinkButton2_Click(object sender, EventArgs e)
   {
       // Pehle table clear
       Cnn.Open();
       Cnn.ExecuteNonQuery("DELETE FROM customerlist");
       Cnn.Close();

       if (FileUpload1.HasFile)
       {
           string filePath = Server.MapPath("~/Uploads/") + Path.GetFileName(FileUpload1.FileName);
           string extension = Path.GetExtension(filePath).ToLower();

           if (extension != ".csv")
           {
               Response.Write("Only .csv files are supported.");
               return;
           }

           FileUpload1.SaveAs(filePath);

          DataTable dt = new DataTable();

          using (StreamReader sr = new StreamReader(filePath))
          {
              string headerLine = sr.ReadLine();
              string[] headers = headerLine.Split(',');

              foreach (string header in headers)
              {
                  dt.Columns.Add(header.Trim());
              }

              while (!sr.EndOfStream)
              {
                  string line = sr.ReadLine();

                  // empty line skip
                  if (string.IsNullOrWhiteSpace(line))
                      continue;

                  string[] rows = line.Split(',');
                  DataRow dr = dt.NewRow();

                  for (int i = 0; i < headers.Length; i++)
                  {
                      if (i < rows.Length)
                          dr[i] = rows[i].Trim();
                      else
                          dr[i] = "";
                  }

                  dt.Rows.Add(dr);
              }
          }


               // Bulk insert
               BulkInsert(dt);

               Response.Write("CSV file imported successfully!");
          
       }
   }

   
}