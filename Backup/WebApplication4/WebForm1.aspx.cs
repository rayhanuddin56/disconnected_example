using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication4
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        private void LoadFromDatabase()
        {
            string sql = "SELECT * FROM Students";
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            DataSet ds = new DataSet();
            adapter.Fill(ds, "Students");

            ds.Tables["Students"].PrimaryKey = new DataColumn[] { ds.Tables["Students"].Columns["StudentId"] };

            Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
            Cache.Insert("ADAPTER", adapter, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);

        }

        private void LoadGridViewData()
        {
            if (Cache["DATASET"] == null)
            {
                this.LoadFromDatabase();
            }

            DataSet ds = (DataSet)Cache["DATASET"];
            GridView1.DataSource = ds.Tables["Students"];
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            this.LoadGridViewData();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.LoadGridViewData();

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Cache["DATASET"] == null)
            {
                this.LoadFromDatabase();
            }

            DataSet ds = (DataSet)Cache["DATASET"];
            DataRow dr = ds.Tables["Students"].Rows.Find(e.Keys["StudentId"]);

            dr.Delete();

            Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
            this.LoadGridViewData();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            this.LoadGridViewData();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Cache["DATASET"] == null)
            {
                this.LoadFromDatabase();
            }

            DataSet ds = (DataSet)Cache["DATASET"];
            DataRow dr = ds.Tables["Students"].Rows.Find(e.Keys["StudentId"]);
            dr["StudentName"] = e.NewValues["StudentName"];
            dr["Email"] = e.NewValues["Email"];
            dr["Phone"] = e.NewValues["Phone"];
            Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
            GridView1.EditIndex = -1;
            this.LoadGridViewData();

        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (Cache["DATASET"] == null)
            {
                this.LoadFromDatabase();
            }

            DataSet ds = (DataSet)Cache["DATASET"];
            DataRow dr = ds.Tables["Students"].NewRow();
            dr["StudentId"] = TextBoxID.Text;
            dr["StudentName"] = TextBoxName.Text;
            dr["Email"] = TextBoxEmail.Text;
            dr["Phone"] = TextBoxPhone.Text;
            ds.Tables["Students"].Rows.Add(dr);
            Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
            this.LoadGridViewData();
            LabelMessage.Text = "Insert successfull";
        }

        protected void Undo_Click(object sender, EventArgs e)
        {
            if (Cache["DATASET"] == null)
            {
                this.LoadFromDatabase();
            }

            DataSet ds = (DataSet)Cache["DATASET"];
            if (ds.HasChanges())
            {
                ds.Tables["Students"].RejectChanges();
                Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
                this.LoadGridViewData();
                LabelMessage.Text = "Undo Complete";
            }
            else
            {
                LabelMessage.Text = "Nothing to be undone";
            }
            
           
        }

        protected void ButtonUpdateDB_Click(object sender, EventArgs e)
        {
            if (Cache["DATASET"] == null)
            {
                this.LoadFromDatabase();
            }

            DataSet ds = (DataSet)Cache["DATASET"];
            SqlDataAdapter adapter = (SqlDataAdapter)Cache["ADAPTER"];
            adapter.Update(ds.Tables["Students"]);
            ds.Tables["Students"].AcceptChanges();
            LabelMessage.Text = "Saved to database";
        }
    }
}