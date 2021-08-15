using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drift_City_Tools
{
    class EdsomeLibrary
    {
        // Input a Application Name, And This Method Will Try to Open it
        public void OpenApplication(string Target)
        {
            try
            {
                System.Diagnostics.Process.Start(Target);
            }
            catch (Exception)
            {
                MsgError("Can not open application named " + Target + " for an unknown reason.");
            }
        }

        // Show an Error MessageBox
        public void MsgError(string Content)
        {
            MessageBox.Show(Content, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Show a Warning MessageBox
        public void MsgWarning(string Content)
        {
            MessageBox.Show(Content, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
