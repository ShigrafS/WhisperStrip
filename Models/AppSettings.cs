using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WhisperStrip.Models;

public class AppSettings
{
    public List<string> Texts { get; set; } = new()
    {
        "Drink water",
        "Stand up straight",
        "Focus on your goal"
    };

    public bool UseRandomTimers { get; set; } = true;
    
    public int ShowMin { get; set; } = 20;
    public int ShowMax { get; set; } = 30;
    public int ShowExact { get; set; } = 25;

    public int HideMin { get; set; } = 30;
    public int HideMax { get; set; } = 40;
    public int HideExact { get; set; } = 35;

    public int RotateInterval { get; set; } = 20;

    public static string SettingsFilePath 
    {
        get
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WhisperStrip",
                "settings.json"
            );
        }
    }

    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                var json = File.ReadAllText(SettingsFilePath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json);
                if (settings != null) return settings;
            }
        }
        catch (Exception)
        {
            // silent fail, fallback to defaults
        }
        return new AppSettings();
    }

    public void Save()
    {
        try
        {
            var dir = Path.GetDirectoryName(SettingsFilePath);
            if (!Directory.Exists(dir!))
                Directory.CreateDirectory(dir!);

            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, json);
        }
        catch (Exception)
        {
            // hacky: ignore for now
        }
    }
}
