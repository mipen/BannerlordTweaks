using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using TaleWorlds.Core;

namespace BannerlordTweaks {
    public static class DebugHelpers {
        [Conditional("DEBUG")]
        public static void DebugMessage(string message) {
            Message(message);
        }

        public static void Message(string message) {
            InformationManager.DisplayMessage(new InformationMessage(message));
        }

        // From Modlib---
        public static void ShowError(string message, string title = "", Exception exception = null) {
            if (string.IsNullOrWhiteSpace(title)) {
                title = "";
            }

            MessageBox.Show(message + "\n\n" + exception?.ToStringFull(), title);
        }

        public static string ToStringFull(this Exception ex) => ex != null ? GetString(ex) : "";

        private static string GetString(Exception ex) {
            StringBuilder sb = new StringBuilder();
            GetStringRecursive(ex, sb);
            sb.AppendLine();
            sb.AppendLine("Stack trace:");
            sb.AppendLine(ex.StackTrace);
            return sb.ToString();
        }

        private static void GetStringRecursive(Exception ex, StringBuilder sb) {
            while (true) {
                sb.AppendLine(ex.GetType().Name + ":");
                sb.AppendLine(ex.Message);
                if (ex.InnerException == null) {
                    return;
                }

                sb.AppendLine();
                ex = ex.InnerException;
            }
        }
        // --------------
    }
}
