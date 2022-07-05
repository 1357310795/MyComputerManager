﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common;
using Wpf.Ui.Controls.Interfaces;

namespace Wpf.Ui.Services;

/// <summary>
/// Internal activator for navigation purposes.
/// </summary>
internal static class NavigationServiceActivator
{
    /// <summary>
    /// Creates new instance of type derived from <see cref="FrameworkElement"/>.
    /// </summary>
    /// <param name="pageType"><see cref="FrameworkElement"/> to instantiate.</param>
    /// <returns>Instance of the <see cref="FrameworkElement"/> object or <see langword="null"/>.</returns>
    public static FrameworkElement CreateInstance(Type pageType)
    {
        return CreateInstance(pageType, null);
    }

    /// <summary>
    /// Creates new instance of type derived from <see cref="FrameworkElement"/>.
    /// </summary>
    /// <param name="pageType"><see cref="FrameworkElement"/> to instantiate.</param>
    /// <param name="dataContext">Additional context to set.</param>
    /// <returns>Instance of the <see cref="FrameworkElement"/> object or <see langword="null"/>.</returns>
    public static FrameworkElement CreateInstance(Type pageType, object dataContext)
    {
        if (!typeof(FrameworkElement).IsAssignableFrom(pageType))
            throw new InvalidCastException(
                $"PageType of the ${typeof(INavigationItem)} must be derived from {typeof(FrameworkElement)}. {pageType} is not.");

        if (DesignerHelper.IsInDesignMode)
            return new Page { Content = new TextBlock { Text = "Pages are not rendered while using the Designer. Edit the page template directly." } };

        // Very poor dependency injection
        if (dataContext != null)
        {
            var dataContextConstructor = pageType.GetConstructor(new[] { dataContext.GetType() });

            // Return instance which has constructor with matching datacontext type
            if (dataContextConstructor != null)
                return dataContextConstructor.Invoke(new[] { dataContext }) as FrameworkElement;
        }

        var emptyConstructor = pageType.GetConstructor(Type.EmptyTypes);

        if (emptyConstructor == null)
            throw new InvalidOperationException($"The {pageType} page does not have a parameterless constructor. If you are using {typeof(Mvvm.Contracts.IPageService)} do not navigate initially and don't use Cache or Precache.");

        var instance = emptyConstructor.Invoke(null) as FrameworkElement;

        if (dataContext != null)
            instance!.DataContext = dataContext;

        return instance;
    }
}
