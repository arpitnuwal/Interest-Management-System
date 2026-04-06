using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddParty : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    // SAVE PARTY
    protected void btnSave_Click(object sender, EventArgs e)
    {
        con.Open();

        string query = "INSERT INTO Party (Name, Mobile, Address) VALUES (@n,@m,@a)";
        SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@n", txtName.Text);
        cmd.Parameters.AddWithValue("@m", txtMobile.Text);
        cmd.Parameters.AddWithValue("@a", txtAddress.Text);

        cmd.ExecuteNonQuery();
        con.Close();

        lblMsg.Text = "✅ Party Added Successfully";

        txtName.Text = "";
        txtMobile.Text = "";
        txtAddress.Text = "";

        LoadData();
    }

    // LOAD DATA
    void LoadData()
    {
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Party ORDER BY PartyID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();

        con.Close();
    }

    // PAGINATION
    protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadData();
    }

    // EDIT
    protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        LoadData();
    }

    // CANCEL EDIT
    protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        LoadData();
    }

    // UPDATE
    protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

        string name = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
        string mobile = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
        string address = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

        con.Open();

        string query = "UPDATE Party SET Name=@n, Mobile=@m, Address=@a WHERE PartyID=@id";
        SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@n", name);
        cmd.Parameters.AddWithValue("@m", mobile);
        cmd.Parameters.AddWithValue("@a", address);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
        con.Close();

        GridView1.EditIndex = -1;
        LoadData();
    }

    // DELETE
    protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

        con.Open();

        SqlCommand cmd = new SqlCommand("DELETE FROM Party WHERE PartyID=@id", con);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
        con.Close();

        LoadData();
    }
}