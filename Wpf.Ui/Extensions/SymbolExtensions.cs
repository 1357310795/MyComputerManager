﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;
using Wpf.Ui.Common;

namespace Wpf.Ui.Extensions;

/// <summary>
/// Set of extensions for the enumeration of icons to facilitate their management and replacement.
/// </summary>
public static class SymbolExtensions
{
    /// <summary>
    /// Replaces <see cref="SymbolRegular"/> with <see cref="SymbolFilled"/>.
    /// </summary>
    public static SymbolFilled Swap(this SymbolRegular icon)
    {
        // It is possible that the alternative icon does not exist
        return Glyph.ParseFilled(icon.ToString());
    }

    /// <summary>
    /// Replaces <see cref="SymbolFilled"/> with <see cref="SymbolRegular"/>.
    /// </summary>
    public static SymbolRegular Swap(this SymbolFilled icon)
    {
        // It is possible that the alternative icon does not exist
        return Glyph.Parse(icon.ToString());
    }

    /// <summary>
    /// Converts <see cref="SymbolRegular"/> to <see langword="char"/> based on the ID.
    /// </summary>
    public static char GetGlyph(this SymbolRegular icon)
    {
        return ToChar(icon);
    }

    /// <summary>
    /// Converts <see cref="SymbolFilled"/> to <see langword="char"/> based on the ID.
    /// </summary>
    public static char GetGlyph(this SymbolFilled icon)
    {
        return ToChar(icon);
    }

    /// <summary>
    /// Converts <see cref="SymbolRegular"/> to <see langword="string"/> based on the ID.
    /// </summary>
    public static string GetString(this SymbolRegular icon)
    {
        return icon.GetGlyph().ToString();
    }

    /// <summary>
    /// Converts <see cref="SymbolFilled"/> to <see langword="string"/> based on the ID.
    /// </summary>
    public static string GetString(this SymbolFilled icon)
    {
        return icon.GetGlyph().ToString();
    }

    /// <summary>
    /// Converts <see cref="SymbolRegular"/> to <see langword="char"/>.
    /// </summary>
    private static char ToChar(SymbolRegular icon)
    {
        return Convert.ToChar(icon);
    }

    /// <summary>
    /// Converts <see cref="SymbolFilled"/> to <see langword="char"/>.
    /// </summary>
    private static char ToChar(SymbolFilled icon)
    {
        return Convert.ToChar(icon);
    }
}
