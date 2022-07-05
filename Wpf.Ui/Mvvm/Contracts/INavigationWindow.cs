﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;
using System.Windows.Controls;
using Wpf.Ui.Controls.Interfaces;

namespace Wpf.Ui.Mvvm.Contracts;

/// <summary>
/// Represents a contract with a <see cref="System.Windows.Window"/> that contains <see cref="INavigation"/>.
/// Through defined <see cref="IPageService"/> service allows you to use the MVVM model in <c>WPF UI</c> navigation.
/// </summary>
public interface INavigationWindow
{
    /// <summary>
    /// Provides direct access to the <see cref="Frame"/> used in navigation.
    /// </summary>
    /// <returns>Instance of the <see cref="Frame"/> control.</returns>
    Frame GetFrame();

    /// <summary>
    /// Provides direct access to the control responsible for navigation.
    /// </summary>
    /// <returns>Instance of the <see cref="INavigation"/> control.</returns>
    INavigation GetNavigation();

    /// <summary>
    /// Lets you navigate to the selected page based on it's type. Should be used with <see cref="IPageService"/>.
    /// </summary>
    /// <param name="pageType"><see langword="Type"/> of the page.</param>
    bool Navigate(Type pageType);

    /// <summary>
    /// Lets you attach the service that delivers page instances to <see cref="INavigation"/>.
    /// </summary>
    /// <param name="pageService">Instance of the <see cref="IPageService"/> with attached service provider.</param>
    void SetPageService(IPageService pageService);

    /// <summary>
    /// Triggers the command to open a window.
    /// </summary>
    void ShowWindow();

    /// <summary>
    /// Triggers the command to close a window.
    /// </summary>
    void CloseWindow();
}
