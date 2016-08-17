// © ABBYY. 2011.
// SAMPLES code is property of ABBYY, exclusive rights are reserved. 
// DEVELOPER is allowed to incorporate SAMPLES into his own APPLICATION and modify it 
// under the terms of License Agreement between ABBYY and DEVELOPER.

using System;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using FREngine;

namespace EngineLoader
{
  // Class for loading/unloading FineReader Engine.
  // Loading is performed in constructor, unloading in Dispose()
  // Throws exceptions when loading fails
  public class EngineLoader : IDisposable
  {
    // Load FineReader Engine with settings stored in SamplesConfig.cs
    public EngineLoader()
    {
        string developerSN = ConfigurationManager.AppSettings["SerialNumber"];

      // Changing current directory so the system could locate all dlls needed
      string oldDirectory = System.Environment.CurrentDirectory;
      System.Environment.CurrentDirectory = ConfigurationManager.AppSettings["DllFolder"];

      try
      {
        // Call the GetEngineObject function
        int hresult = GetEngineObject(
            developerSN, null, null,
            ref engine);

        Marshal.ThrowExceptionForHR(hresult);
      }
      catch (Exception)
      {
        engine = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();

        throw;
      }
      finally
      {
        // Restore current directory
        System.Environment.CurrentDirectory = oldDirectory;
      }
    }

    // Unload FineReader Engine
    public void Dispose()
    {
      if (engine == null)
      {
        // Engine was not loaded
        return;
      }

      engine = null;
      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();
      GC.WaitForPendingFinalizers();

      int hresult = DeinitializeEngine();

      // Throwing exception after cleaning up
      Marshal.ThrowExceptionForHR(hresult);
    }

    // Returns pointer to FineReader Engine's main object
    public IEngine Engine
    {
      get
      {
        return engine;
      }
    }

    // FREngine.dll functions
    [DllImport("FREngine.dll", CharSet = CharSet.Unicode), PreserveSig]
    private static extern int GetEngineObject(string devSN, string reserved1,
        string reserved2, ref FREngine.IEngine engine);
    [DllImport("FREngine.dll", CharSet = CharSet.Unicode), PreserveSig]
    private static extern int DeinitializeEngine();

    // private variables
    private FREngine.IEngine engine = null;
  }
}
