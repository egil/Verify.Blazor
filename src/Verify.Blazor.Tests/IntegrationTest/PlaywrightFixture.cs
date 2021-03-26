﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using PlaywrightSharp;
using PlaywrightSharp.Chromium;
using Xunit;

public class PlaywrightFixture :
    IAsyncLifetime
{
    IPlaywright? playwright;
    Process? process;
    IChromiumBrowser? browser;

    public IChromiumBrowser Browser
    {
        get => browser!;
    }

    public async Task InitializeAsync()
    {
        StartBlazorApp();
        playwright = await Playwright.CreateAsync();
        browser = await playwright.Chromium.LaunchAsync();
    }

    void StartBlazorApp()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory!;
        var binPath = baseDirectory.Replace("Verify.Blazor.Tests", "BlazorApp");
        var projectDir = Path.GetFullPath(Path.Combine(binPath, "../../"));
        ProcessStartInfo startInfo = new("dotnet", "run --no-build --no-restore")
        {
            WorkingDirectory = projectDir
        };
        process = Process.Start(startInfo);
    }

    public async Task DisposeAsync()
    {
        if (browser != null)
        {
            await browser.DisposeAsync();
        }

        playwright?.Dispose();

        if (process != null)
        {
            process.Kill();
            process.Dispose();
        }
    }
}