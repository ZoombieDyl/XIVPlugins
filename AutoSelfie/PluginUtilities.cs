using System.Windows.Forms;

namespace AutoSelfie
{
    public static class PluginUtilities
    {
        public static void TakeScreenshot()
        {
            SendKeys.SendWait("{PRTSC}");
        }
    }
}
