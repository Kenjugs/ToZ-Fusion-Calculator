using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Skill_Calculator.utilities;

namespace Skill_Calculator {
    public partial class Default : Page {
        private readonly DbManager _dbMan = new DbManager();

        protected void ddlReverseSearch_SelectedIndexChanged(object sender, EventArgs e) {
            var retrievalDt = new DataTable();
            var adapter = new SqlDataAdapter();

            retrievalDt.Columns.Add("Skill1");
            retrievalDt.Columns.Add("Skill2");
            var resultDt = retrievalDt.Clone();

            using (var cmd = new SqlCommand("SProc_Normal_Calculation", _dbMan.ConnectToDatabase())) {
                cmd.CommandType = CommandType.StoredProcedure;

                for (var skill1 = 1; skill1 <= 50; ++skill1) {
                    for (var skill2 = skill1; skill2 <= 50; ++skill2) {
                        if (skill1 == skill2) {
                            continue;
                        }

                        cmd.Parameters.AddWithValue("@Skill1ID", skill1);
                        cmd.Parameters.AddWithValue("@Skill2ID", skill2);

                        adapter.SelectCommand = cmd;
                        adapter.Fill(retrievalDt);

                        cmd.Parameters.Clear();
                    }
                }

                foreach (DataRow row in retrievalDt.Rows) {
                    if (row["ID"].ToString() == ddlReverseSearch.SelectedValue) {
                        resultDt.ImportRow(row);
                    }
                }

                ViewState["GridViewDataTable"] = resultDt;
                gvReverseCalc.DataSource = ViewState["GridViewDataTable"];
                gvReverseCalc.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            SetConnectionString();
            SetSqlSelect();

            if (IsPostBack)
                return;

            BindDropdownList();
            gvReverseCalc.DataBind();
        }

        private void BindDropdownList() {
            ddlReverseSearch.DataSource = dsSkillNames;
            ddlReverseSearch.DataValueField = "ID";
            ddlReverseSearch.DataTextField = "SkillName";
            ddlReverseSearch.DataBind();
        }

        private void SetConnectionString() {
            dsSkillNames.ConnectionString = _dbMan.ConnectionString;
            dsSkillNames.ProviderName = _dbMan.Provider;
        }

        private void SetSqlSelect() {
            const string sql = "SELECT ID, SkillName FROM [Skill_Names]";

            dsSkillNames.SelectCommandType = SqlDataSourceCommandType.Text;
            dsSkillNames.SelectCommand = sql;
            dsSkillNames.DataBind();
        }

        protected void gvReverseCalc_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gvReverseCalc.PageIndex = e.NewPageIndex;
            gvReverseCalc.DataSource = ViewState["GridViewDataTable"];
            gvReverseCalc.DataBind();
        }
    }
}