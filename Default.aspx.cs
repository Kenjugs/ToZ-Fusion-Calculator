using Skill_Calculator.utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Skill_Calculator {

    public partial class Default : Page {
        private dbManager _dbMan = new dbManager();

        protected void ddlReverseSearch_SelectedIndexChanged(object sender, EventArgs e) {
            DataTable retrievalDT = new DataTable();
            DataTable resultDT = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            retrievalDT.Columns.Add("Skill1");
            retrievalDT.Columns.Add("Skill2");
            resultDT = retrievalDT.Clone();

            using (SqlCommand cmd = new SqlCommand("SProc_Normal_Calculation", _dbMan.ConnectToDB())) {
                cmd.CommandType = CommandType.StoredProcedure;

                for (var skill1 = 1; skill1 <= 50; ++skill1) {
                    for (var skill2 = skill1; skill2 <= 50; ++skill2) {
                        if (skill1 == skill2) {
                            continue;
                        }

                        paramList.Add(new SqlParameter("@Skill1ID", skill1));
                        paramList.Add(new SqlParameter("@Skill2ID", skill2));

                        foreach (SqlParameter param in paramList) {
                            cmd.Parameters.Add(param);
                        }

                        adapter.SelectCommand = cmd;
                        adapter.Fill(retrievalDT);

                        cmd.Parameters.Clear();
                        paramList.Clear();
                    }
                }

                foreach (DataRow row in retrievalDT.Rows) {
                    if (row["ID"].ToString() == ddlReverseSearch.SelectedValue) {
                        resultDT.ImportRow(row);
                    }
                }

                gvReverseCalc.DataSource = resultDT;
                gvReverseCalc.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            SetConnectionString();
            SetSQLSelect();

            if (!IsPostBack) {
                BindDropdownList();
            }
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

        private void SetSQLSelect() {
            var sql = "SELECT ID, SkillName FROM [Skill_Names]";

            dsSkillNames.SelectCommandType = SqlDataSourceCommandType.Text;
            dsSkillNames.SelectCommand = sql;
            dsSkillNames.DataBind();
        }
    }
}