using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Skill_Calculator.utilities;

namespace Skill_Calculator {

    public partial class Default : Page {

        protected void Page_Load(object sender, EventArgs e) {

            SetConnectionString();
            SetSqlSelect();

            if (!IsPostBack) {
                BindDropdownList();
                gvReverseCalc.DataBind();
            }
        }

        private void BindDropdownList() {

            ddlReverseSearch.DataSource = dsSkillNames;
            ddlReverseSearch.DataValueField = "ID";
            ddlReverseSearch.DataTextField = "SkillName";
            ddlReverseSearch.DataBind();
        }

        private void SetConnectionString() {

            dsSkillNames.ConnectionString = DbManager.Connection.ConnectionString;
            dsSkillNames.ProviderName = DbManager.Provider;
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

        protected void ddlReverseSearch_SelectedIndexChanged(object sender, EventArgs e) {

            var retrievalDt = new DataTable();
            var paramList = new List<SqlParameter>();

            retrievalDt.Columns.Add("Skill1");
            retrievalDt.Columns.Add("Skill2");
            var resultDt = retrievalDt.Clone();

            DbManager.ConnectToDatabase();

            using (var cmd = new SqlCommand("SProc_Normal_Calculation", DbManager.Connection)) {
                cmd.CommandType = CommandType.StoredProcedure;

                for (var skill1 = 1; skill1 <= 50; ++skill1) {
                    for (var skill2 = skill1; skill2 <= 50; ++skill2) {
                        if (skill1 == skill2) {
                            continue;
                        }

                        paramList.Add(new SqlParameter("@Skill1ID", skill1));
                        paramList.Add(new SqlParameter("@Skill2ID", skill2));

                        DbManager.AppendDataTable(cmd, paramList, ref retrievalDt);

                        paramList.Clear();
                    }
                }
            }

            DbManager.CloseConnection();

            foreach (DataRow row in retrievalDt.Rows) {
                if (row["ID"].ToString() == ddlReverseSearch.SelectedValue) {
                    resultDt.ImportRow(row);
                }
            }

            ViewState["GridViewDataTable"] = resultDt;
            gvReverseCalc.PageIndex = 0;
            gvReverseCalc.DataSource = ViewState["GridViewDataTable"];
            gvReverseCalc.DataBind();
        }
    }
}