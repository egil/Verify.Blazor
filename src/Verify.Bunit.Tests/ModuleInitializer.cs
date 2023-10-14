﻿
public static class ModuleInitializer
{
    #region BunitEnable

    [ModuleInitializer]
    public static void Initialize() =>
        VerifyBunit.Initialize(false);

    #endregion

    [ModuleInitializer]
    public static void InitializeOther() =>
        VerifyDiffPlex.Initialize();
}