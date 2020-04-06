using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModLib
{
    public static class ModDebug
    {
        public static void ShowError(string message, string title, Exception exception)
        {
            if (string.IsNullOrWhiteSpace(title))
                title = "";
            MessageBox.Show($"{message}\n\n{exception.ToStringFull()}", title);
        }

        public static void ShowMessage(string message, string title="")
        {
            MessageBox.Show(message, title);
        }

        public static void LogError(string error)
        {

        }
    }
}
