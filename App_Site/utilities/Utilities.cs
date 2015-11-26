using System.Web.UI;

namespace Skill_Calculator.utilities {

    public class Utilities {

        public static Control FindControlRecursive(Control root, string id) {

            if (root.ID == id) {
                return root;
            }

            foreach (Control cntrl in root.Controls) {
                Control ret = FindControlRecursive(cntrl, id);

                if (ret != null) {
                    return ret;
                }
            }

            return null;
        }
    }
}