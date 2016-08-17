using System;
using System.Diagnostics;
using System.Configuration;

namespace AbbyTest
{
  static internal class Configuration
  {

    #region Declorations
    internal enum Keys
    {
      TimerInterval = 1,
      SQLDBConnStr = 2,
      CommandTimeout = 3,
      EventLogSource = 4,
      DictionaryPath = 5,
      LearningPassCount = 6,
      LogActions = 7,
      LicenseKey = 8
    }
    #endregion

    #region Properties
    static internal bool LogActions
    {
      get { return ConfigurationManager.AppSettings[Enum.GetName(typeof(Keys), Keys.LogActions)].Trim().ToUpper().Equals("YES"); }
    }

    static internal int TimerInterval
    {
      get { return int.Parse(ConfigurationManager.AppSettings[Enum.GetName(typeof(Keys), Keys.TimerInterval)].Trim()); }
    }

    static internal int LearningPassCount
    {
      get { return int.Parse(ConfigurationManager.AppSettings[Enum.GetName(typeof(Keys), Keys.LearningPassCount)].Trim()); }
    }

    static internal string SQLDBConnStr
    {
      get { return ConfigurationManager.AppSettings[Enum.GetName(typeof(Keys), Keys.SQLDBConnStr)].Trim(); }
    }

    static internal string DictionaryPath
    {
      get { return ConfigurationManager.AppSettings[Enum.GetName(typeof(Keys), Keys.DictionaryPath)].Trim(); }
    }

    static internal string LicenseKey
    {
      get { return ConfigurationManager.AppSettings[Enum.GetName(typeof(Keys), Keys.LicenseKey)].Trim(); }
    }

    static internal int CommandTimeout
    {
      get { return int.Parse(ConfigurationManager.AppSettings[Enum.GetName(typeof(Keys), Keys.CommandTimeout)].Trim()); }
    }

    static internal string EventLogSource
    {
      get { return ConfigurationManager.AppSettings[Enum.GetName(typeof(Keys), Keys.EventLogSource)].Trim(); }
    }

    #endregion

    #region Method
    static internal object item(Keys key)
    {
      return ConfigurationManager.AppSettings[Enum.GetName(typeof(Keys), key)];
    }

    static internal void WriteToEventLog(string pSource, string pMsg, EventLogEntryType pEntryType)
    {
      EventLog elog = new EventLog();
      if (!EventLog.SourceExists(pSource)) createEventSource(pSource);
      elog.Source = pSource;
      elog.EnableRaisingEvents = true;
      elog.WriteEntry(pMsg, pEntryType);
    }

    static internal void createEventSource(string pSource)
    {
      if (!EventLog.SourceExists(pSource))
      {
        EventLog.CreateEventSource(pSource, "OCR Document Processing");
      }
    }

    #endregion

  }
}
