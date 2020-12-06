using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;


namespace BannerlordTweaks {
    public static class DebugHelpers {
        [Conditional("DEBUG")]
        public static void DebugMessage(string message) {
            Message(message);
        }

        public static void Message(string message) {
            InformationManager.DisplayMessage(new InformationMessage(message));
        }

        public static void ColorRedMessage(string message)
        {
            InformationManager.DisplayMessage(new InformationMessage(message, Color.ConvertStringToColor("#FF0042FF")));
        }

        public static void ColorGreenMessage(string message)
        {
            InformationManager.DisplayMessage(new InformationMessage(message, Color.ConvertStringToColor("#42FF00FF")));
        }

        public static void ColorBlueMessage(string message)
        {
            InformationManager.DisplayMessage(new InformationMessage(message, Color.ConvertStringToColor("#0042FFFF")));
        }

        public static void ColorOrangeMessage(string message)
        {
            InformationManager.DisplayMessage(new InformationMessage(message, Color.FromUint(0x00F16D26)));
        }

        public static void QuickInformationMessage(string message)
        {
            InformationManager.AddQuickInformation(new TextObject(message, null), 0, null, "");
        }
        

        // From Modlib---
        public static void ShowError(string message, string title = "", Exception? exception = null) {
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
