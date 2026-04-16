using System;
using System.Linq;
using System.Windows;
using WhisperStrip.Models;

namespace WhisperStrip;

public partial class MainWindow : Window
{
    private AppSettings _settings;

    public MainWindow()
    {
        InitializeComponent();
        
        _settings = AppSettings.Load();
        PopulateUI();
    }

    private void PopulateUI()
    {
        TextsList.Items.Clear();
        foreach (var t in _settings.Texts)
        {
            TextsList.Items.Add(t);
        }

        PauseCheck.IsChecked = StripManager.Instance.IsPaused;
        UseRandomCheck.IsChecked = _settings.UseRandomTimers;
        
        ShowMinInput.Text = _settings.ShowMin.ToString();
        ShowMaxInput.Text = _settings.ShowMax.ToString();
        ShowExactInput.Text = _settings.ShowExact.ToString();

        HideMinInput.Text = _settings.HideMin.ToString();
        HideMaxInput.Text = _settings.HideMax.ToString();
        HideExactInput.Text = _settings.HideExact.ToString();

        RotateInput.Text = _settings.RotateInterval.ToString();
        
        UpdatePanels();
    }

    private void UpdatePanels()
    {
        if (UseRandomCheck.IsChecked == true)
        {
            ShowRandomPanel.Visibility = Visibility.Visible;
            HideRandomPanel.Visibility = Visibility.Visible;
            ShowExactPanel.Visibility = Visibility.Collapsed;
            HideExactPanel.Visibility = Visibility.Collapsed;
        }
        else
        {
            ShowRandomPanel.Visibility = Visibility.Collapsed;
            HideRandomPanel.Visibility = Visibility.Collapsed;
            ShowExactPanel.Visibility = Visibility.Visible;
            HideExactPanel.Visibility = Visibility.Visible;
        }
    }

    private void UseRandomCheck_Changed(object sender, RoutedEventArgs e)
    {
        // small hack to avoid nullref during initialize
        if (ShowRandomPanel != null)
            UpdatePanels();
    }

    private void PauseCheck_Changed(object sender, RoutedEventArgs e)
    {
        if (PauseCheck.IsChecked == true && !StripManager.Instance.IsPaused)
            StripManager.Instance.TogglePause();
        else if (PauseCheck.IsChecked == false && StripManager.Instance.IsPaused)
            StripManager.Instance.TogglePause();
    }

    private void AddText_Click(object sender, RoutedEventArgs e)
    {
        string t = NewTextInput.Text.Trim();
        if (!string.IsNullOrEmpty(t))
        {
            TextsList.Items.Add(t);
            NewTextInput.Clear();
        }
    }

    private void DeleteText_Click(object sender, RoutedEventArgs e)
    {
        if (TextsList.SelectedIndex != -1)
        {
            TextsList.Items.RemoveAt(TextsList.SelectedIndex);
        }
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        _settings.Texts = TextsList.Items.Cast<string>().ToList();

        _settings.UseRandomTimers = UseRandomCheck.IsChecked ?? true;

        int.TryParse(ShowMinInput.Text, out int sMin);
        int.TryParse(ShowMaxInput.Text, out int sMax);
        int.TryParse(ShowExactInput.Text, out int sEx);
        
        int.TryParse(HideMinInput.Text, out int hMin);
        int.TryParse(HideMaxInput.Text, out int hMax);
        int.TryParse(HideExactInput.Text, out int hEx);

        int.TryParse(RotateInput.Text, out int ri);

        _settings.ShowMin = Math.Max(1, sMin);
        _settings.ShowMax = Math.Max(_settings.ShowMin, sMax);
        _settings.ShowExact = Math.Max(1, sEx);

        _settings.HideMin = Math.Max(1, hMin);
        _settings.HideMax = Math.Max(_settings.HideMin, hMax);
        _settings.HideExact = Math.Max(1, hEx);

        _settings.RotateInterval = Math.Max(1, ri);

        _settings.Save();

        StripManager.Instance.ReloadSettings();

        // feedback
        System.Windows.MessageBox.Show("Settings saved and applied!", "WhisperStrip", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Quit_Click(object sender, RoutedEventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
    }
}