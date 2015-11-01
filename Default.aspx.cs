using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Skill_Calculator.utilities;

namespace Skill_Calculator {

    public partial class Default : Page {

        protected void Page_Load(object sender, EventArgs e) {
            // populate dropdown list
            SetConnectionString();
            SetSQLSelect();
            BindDropdownList();
        }

        private void SetConnectionString() {
            var dbMan = new dbManager();

            dsSkillNames.ConnectionString = dbMan.GetConnectionString();
            dsSkillNames.ProviderName = dbMan.GetProviderName();
        }

        private void SetSQLSelect() {
            var sql = "SELECT ID, SkillName FROM [Skill Names]";

            dsSkillNames.SelectCommandType = SqlDataSourceCommandType.Text;
            dsSkillNames.SelectCommand = sql;
            dsSkillNames.DataBind();
        }

        private void BindDropdownList() {
            ddlReverseSearch.DataSource = dsSkillNames;
            ddlReverseSearch.DataValueField = "ID";
            ddlReverseSearch.DataTextField = "SkillName";
            ddlReverseSearch.DataBind();
        }

        protected void ddlReverseSearch_SelectedIndexChanged(object sender, EventArgs e) {
            dbManager dbMan = new dbManager();
            SqlConnection conn = new SqlConnection(dbMan.GetConnectionString());
            SqlCommand cmd = new SqlCommand("[Normal Calculation]", conn);
            DataTable retrievalDT = new DataTable();
            DataTable resultDT = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.CommandType = CommandType.StoredProcedure;
            
            retrievalDT.Columns.Add("Skill1");
            retrievalDT.Columns.Add("Skill2");
            resultDT = retrievalDT.Clone();

            using (cmd) {
                for (var skill1 = 1; skill1 <= 50; ++skill1) {
                    for (var skill2 = skill1; skill2 <= 50; ++skill2) {
                        if (skill1 == skill2) {
                            continue;
                        }

                        cmd.Parameters.AddWithValue("@Skill1ID", skill1);
                        cmd.Parameters.AddWithValue("@Skill2ID", skill2);

                        adapter.SelectCommand = cmd;
                        adapter.Fill(retrievalDT);

                        cmd.Parameters.Clear();
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
    }
}