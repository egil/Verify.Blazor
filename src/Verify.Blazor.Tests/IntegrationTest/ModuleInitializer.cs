﻿using System.Runtime.CompilerServices;
using Verify.AngleSharp;
using VerifyTests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        // remove some noise from the html snapshot
        #region scrubbers
        VerifierSettings.ScrubEmptyLines();
        VerifierSettings.ScrubLinesWithReplace(s => s.Replace("<!--!-->", ""));
        HtmlPrettyPrint.All();
        VerifierSettings.ScrubLinesContaining("<script src=\"_framework/dotnet.");
        #endregion
        VerifyPlaywright.Enable();
        VerifyImageMagick.RegisterComparers();
    }
}