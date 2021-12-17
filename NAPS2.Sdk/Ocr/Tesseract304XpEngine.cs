﻿using NAPS2.Dependencies;

namespace NAPS2.Ocr;

public class Tesseract304XpEngine : Tesseract304Engine
{
    public Tesseract304XpEngine(string basePath) : base(basePath)
    {
        TesseractExePath = "tesseract_xp.exe";
        PlatformSupport = PlatformSupport.Windows;

        Component = new ExternalComponent("ocr", Path.Combine(TesseractBasePath, TesseractExePath),
            new DownloadInfo("tesseract_xp.exe.gz", Mirrors, 1.32, "98d15e4765caae864f16fa2ab106e3fd6adbe8c3", DownloadFormat.Gzip));
    }
}