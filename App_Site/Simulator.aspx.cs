using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Skill_Calculator.utilities;

namespace Skill_Calculator {

    public partial class Simulator : Page {

        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack) {
                LoadDropdowns();
            }
        }

        private void LoadDropdowns() {

            var strSql = "SELECT ID, SkillName FROM [Skill_Names]";

            dsSkillNames.SelectCommand = strSql;
            dsSkillNames.ConnectionString = DbManager.Connection.ConnectionString;
            dsSkillNames.ProviderName = DbManager.Provider;
            dsSkillNames.DataBind();

            for (var idx = 1; idx <= 4; ++idx) {
                var left = (DropDownList) Utilities.FindControlRecursive(Page, string.Format("ddlLeft{0}", idx));
                var right = (DropDownList) Utilities.FindControlRecursive(Page, string.Format("ddlRight{0}", idx));

                left.DataSource = dsSkillNames;
                left.DataTextField = "SkillName";
                left.DataValueField = "ID";
                left.DataBind();

                right.DataSource = dsSkillNames;
                right.DataTextField = "SkillName";
                right.DataValueField = "ID";
                right.DataBind();
            }
        }

        [WebMethod]
        public static string Calculate(string sender, string other) {

            var skill1 = sender.Substring(sender.LastIndexOf('=') + 1);
            var skill2 = other.Substring(other.LastIndexOf('=') + 1);

            if (skill1 != "0" || skill2 != "0") {
                var retrievalDt = new DataTable();
                var paramList = new List<SqlParameter>();

                retrievalDt.Columns.Add("Skill1");
                retrievalDt.Columns.Add("Skill2");

                DbManager.ConnectToDatabase();

                using (var cmd = new SqlCommand("SProc_Normal_Calculation", DbManager.Connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    paramList.Add(new SqlParameter("@Skill1ID", skill1));
                    paramList.Add(new SqlParameter("@Skill2ID", skill2));

                    retrievalDt = DbManager.GetDataSet(cmd, paramList).Tables[0];
                }

                DbManager.CloseConnection();

                return retrievalDt.Rows[0]["SkillName"].ToString();
            } else {
                return "(BLANK)";
            }
        }
    }
}