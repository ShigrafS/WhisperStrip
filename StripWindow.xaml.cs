using System.Windows;

namespace WhisperStrip;

public partial class StripWindow : Window
{
    public StripWindow()
    {
        InitializeComponent();

        // hacky way to shift position, click anywhere on the strip window
        MouseDown += (s, e) =>
        {
            StripManager.Instance.CyclePosition();
        };
    }

    public void UpdateText(string text)
    {
        TextDisplay.Text = text;
    }

    private void Settings_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
    }
}
