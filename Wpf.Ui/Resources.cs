﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;
using System.Windows;
using System.Windows.Media;
using Wpf.Ui.Appearance;

namespace Wpf.Ui;

/// <summary>
/// Facilitates convenient management of WPF UI resources in the application that uses it.
/// </summary>
public class Resources : System.Windows.ResourceDictionary
{
    private static bool _themeInitialized = false;

    private static bool _accentInitialized = false;

    /// <summary>
    /// Gets or sets the application theme.
    /// </summary>
    public ThemeType Theme
    {
        set => InitializeTheme(value);
    }

#if DEBUG

    /// <summary>
    /// Gets or sets the application accent color.
    /// </summary>
    public Color Accent
    {
        set => InitializeAccent(value);
    }

#endif

    /// <summary>
    /// Initializes library resources.
    /// </summary>
    protected void InitializeTheme(ThemeType themeType)
    {
        if (_themeInitialized)
            return;

        _themeInitialized = true;
#if DEBUG
        System.Diagnostics.Debug.WriteLine($"INFO | {typeof(Resources)} initialized, selected theme: {themeType}",
            "Wpf.Ui.Resources");
#endif
        MergedDictionaries.Add(new ResourceDictionary { Source = GetResourceUri("Wpf.Ui") });
        MergedDictionaries.Add(new ResourceDictionary { Source = GetResourceUri(GetThemeResourceName(themeType)) });
        //MergedDictionaries.Add(new ResourceDictionary { Source = GetResourceUri("Assets/Brushes") });

    }

#if DEBUG

    /// <summary>
    /// Tries to apply accent color to the application controls.
    /// </summary>
    protected void InitializeAccent(Color accent)
    {
        if (_accentInitialized)
            return;

        _accentInitialized = true;
#if DEBUG
        System.Diagnostics.Debug.WriteLine($"INFO | {typeof(Resources)} initialized, selected accent: {accent}",
            "Wpf.Ui.Resources");
#endif
        // TODO: Initialize accent
    }

#endif

    /// <summary>
    /// Gets absolute path to the library resource.
    /// </summary>
    protected Uri GetResourceUri(string path)
    {
        return new Uri($"pack://application:,,,/Wpf.Ui;component/Styles/{path}.xaml"); // 
    }

    /// <summary>
    /// Gets path to the selected theme resource.
    /// </summary>
    protected string GetThemeResourceName(ThemeType themeType)
    {
        return themeType switch
        {
            ThemeType.HighContrast => "Theme/HighContrast",
            ThemeType.Light => "Theme/Light",
            _ => "Theme/Dark"
        };
    }
}
