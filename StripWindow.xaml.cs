using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WhisperStrip;

public partial class StripWindow : Window
{
    private readonly Duration fadeDuration = new(TimeSpan.FromMilliseconds(300));

    public StripWindow()
    {
        InitializeComponent();
        Opacity = 0;

        // hacky way to shift position, click anywhere on the strip window
        MouseDown += (s, e) =>
        {
            StripManager.Instance.CyclePosition();
        };
    }

    public void FadeIn()
    {
        var animation = new DoubleAnimation(0, 1, fadeDuration);
        Opacity = 0;
        Show();
        BeginAnimation(OpacityProperty, animation);
    }

    public void FadeOut()
    {
        var animation = new DoubleAnimation(1, 0, fadeDuration);
        animation.Completed += (s, e) =>
        {
            Hide();
            Opacity = 0;
        };
        BeginAnimation(OpacityProperty, animation);
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
