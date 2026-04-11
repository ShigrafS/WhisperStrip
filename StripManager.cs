using System;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Windows;
using WhisperStrip.Models;

namespace WhisperStrip;

public class StripManager
{
    public static StripManager Instance { get; } = new StripManager();

    private StripWindow _Window = null!;
    private DispatcherTimer visibilityTimer = null!;
    private DispatcherTimer? textTimer;

    private AppSettings _settings = null!;

    private int _idx = 0;
    private Random rnd = new();

    public void Start()
    {
        _Window = new StripWindow();
        
        _settings = AppSettings.Load();

        visibilityTimer = new DispatcherTimer();
        visibilityTimer.Tick += Visibility_Tick;
        ScheduleNextAppearance();
    }

    public void ReloadSettings()
    {
        // load updated settings from disk
        _settings = AppSettings.Load();

        _Window.Hide();
        textTimer?.Stop();
        
        ScheduleNextAppearance();
    }

    private void Visibility_Tick(object? sender, EventArgs e)
    {
        if (_Window.IsVisible)
        {
            _Window.Hide();
            textTimer?.Stop();
            ScheduleNextAppearance();
        }
        else
        {
            ShowStrip();
        }
    }

    private void ShowStrip()
    {
        if (_settings.Texts == null || _settings.Texts.Count == 0)
        {
            return; 
        }

        // start with a random phrase
        _idx = rnd.Next(0, _settings.Texts.Count);
        _Window.UpdateText(_settings.Texts[_idx]);
        _Window.Show();

        StartTextRotation();

        int showSecs = _settings.UseRandomTimers 
            ? rnd.Next(_settings.ShowMin, _settings.ShowMax + 1)
            : _settings.ShowExact;

        visibilityTimer.Interval = TimeSpan.FromSeconds(showSecs);
    }

    private void ScheduleNextAppearance()
    {
        if (_settings.Texts == null || _settings.Texts.Count == 0)
        {
            // just loop to check later if user adds text
            visibilityTimer.Interval = TimeSpan.FromSeconds(5);
            visibilityTimer.Start();
            return;
        }

        int hideSecs = _settings.UseRandomTimers 
            ? rnd.Next(_settings.HideMin, _settings.HideMax + 1)
            : _settings.HideExact;

        visibilityTimer.Interval = TimeSpan.FromSeconds(hideSecs);
        visibilityTimer.Start();
    }

    private void StartTextRotation()
    {
        if (_settings.Texts.Count <= 1) return;

        textTimer = new DispatcherTimer();
        textTimer.Interval = TimeSpan.FromSeconds(_settings.RotateInterval);
        textTimer.Tick += (s, e) =>
        {
            _idx = (_idx + 1) % _settings.Texts.Count;
            _Window.UpdateText(_settings.Texts[_idx]);
        };
        textTimer.Start();
    }

    public void CyclePosition()
    {
        if (_Window.Left < 100)
            _Window.Left = SystemParameters.PrimaryScreenWidth - 320;
        else
            _Window.Left = 0;
    }
}
