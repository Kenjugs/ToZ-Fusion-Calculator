using System.Configuration;

namespace Skill_Calculator.utilities {

    public class dbManager {

        public string GetConnectionString() {
            return ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
        }

        public string GetProviderName() {
            return ConfigurationManager.ConnectionStrings["localDB"].ProviderName;
        }
    }
}