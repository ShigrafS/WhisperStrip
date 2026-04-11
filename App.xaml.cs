using System.Configuration;
using System.Data;
using System.Windows;

namespace WhisperStrip;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        TrayService.Initialize();

        // start the main background strip window manager
        StripManager.Instance.Start();
    }
}

