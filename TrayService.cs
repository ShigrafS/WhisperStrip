using System;
using System.Drawing;
using System.Windows.Forms;

namespace WhisperStrip;

public class TrayService
{
    private static NotifyIcon _trayIcon;

    public static void Initialize()
    {
        _trayIcon = new NotifyIcon
        {
            Icon = SystemIcons.Application,
            Visible = true,
            Text = "WhisperStrip"
        };

        var menu = new ContextMenuStrip();
        
        var settingsItem = new ToolStripMenuItem("Settings");
        settingsItem.Click += (s, e) => { new MainWindow().Show(); };
        menu.Items.Add(settingsItem);

        menu.Items.Add(new ToolStripSeparator());

        var exitItem = new ToolStripMenuItem("Exit WhisperStrip");
        exitItem.Click += (s, e) => 
        {
            _trayIcon.Visible = false;
            System.Windows.Application.Current.Shutdown();
        };
        menu.Items.Add(exitItem);

        _trayIcon.ContextMenuStrip = menu;

        _trayIcon.DoubleClick += (s, e) => { new MainWindow().Show(); };
    }
}
