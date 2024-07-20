using Weilai.Forms;

namespace Weilai;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        if (!AppConfig.Default.Upgraded)
        {
            AppConfig.Default.Upgraded = true;
            AppConfig.Default.Upgrade();
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new FrmMain());
    }
}