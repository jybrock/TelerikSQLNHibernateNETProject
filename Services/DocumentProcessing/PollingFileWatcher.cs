using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace DocumentProcessing
{
    public class PollingFileWatcher
    {
        private Thread _WorkerThread = null;
        private int _CycleLength = 0;
        private WatchHelper _WatchHelper = null;
        private List<string> _DirectoryList =
            new List<string>();

        public delegate void CreateFile(FileInfo fileInfo);

        private CreateFile _CreateFileCallback;

        public PollingFileWatcher(int cycleLength)
        {
            _CycleLength = cycleLength;
        }

        public void AddDirectory(string directory)
        {
            _DirectoryList.Add(directory);
        }

        public void ClearDirectories()
        {
            _DirectoryList.Clear();
        }

        public CreateFile CreateFileCallback
        {
            get
            {
                return _CreateFileCallback;
            }

            set
            {
                _CreateFileCallback = value;
            }
        }

        /// <summary>
        /// Start watching directories in _DirectoryList
        /// This method will create a new thread to do the watching. Any delegates
        /// will be called from the new thread.
        /// </summary>
        public void StartWatching()
        {
            _WatchHelper =
                new WatchHelper(_CycleLength, _DirectoryList, _CreateFileCallback);

            _WorkerThread =
                new Thread(new ParameterizedThreadStart(_WatchHelper.WatchDirectories));

            _WorkerThread.Start();
        }

        public void StopWatching()
        {
            if (_WatchHelper != null)
            {
                _WatchHelper.StopWatching();
            }
        }
    }



    public class WatchHelper
    {
        private int _CycleLength;
        private Dictionary<string, List<string>> _DirectoryTrackers;
        private ManualResetEvent _StopEvent =
            new ManualResetEvent(false);
        private PollingFileWatcher.CreateFile _CreateFileCallback;

        public WatchHelper(int cycleLength,
                            List<string> directoryList,
                            PollingFileWatcher.CreateFile createFileCallback)
        {
            _CycleLength = cycleLength;
            _DirectoryTrackers =
                new Dictionary<string, List<string>>();
            _CreateFileCallback = createFileCallback;

            foreach (string directory in directoryList)
            {
                _DirectoryTrackers.Add(directory, new List<string>());
            }
        }

        public void WatchDirectories(object data)
        {
            WaitHandle[] handles = new WaitHandle[1];
            handles[0] = _StopEvent;

            try
            {
                do
                {
                    foreach (string directory in _DirectoryTrackers.Keys)
                    {
                        try
                        {
                            DirectoryInfo di = new DirectoryInfo(directory);
                            FileInfo[] files = di.GetFiles();
                            List<string> fileList = new List<string>();

                            foreach (FileInfo file in files)
                            {
                                fileList.Add(file.Name);

                                if (!_DirectoryTrackers[directory].Contains(file.Name))
                                {
                                    try
                                    {
                                        _CreateFileCallback(file);
                                    }
                                    catch
                                    {
                                        // ignore callback exception
                                    }
                                }
                            }

                            _DirectoryTrackers[directory].Clear();
                            _DirectoryTrackers[directory].AddRange(fileList);
                        }
                        catch (System.Exception exc)
                        {
                            //EventLog.WriteEntry(    "Polling File Watcher",
                            //                        "Fatal Error, processing for directory: " + directory + 
                            //                        " has failed - Error Details: " + exc.Message,
                            //                        EventLogEntryType.Error);
                        }
                    }

                    int handleIndex =
                        WaitHandle.WaitAny(handles, _CycleLength, false);

                    if (handleIndex == 0)
                    {
                        break;
                    }
                }
                while (true);
            }
            catch (System.Exception exc)
            {
                EventLog.WriteEntry("Polling File Watcher",
                                        "Fatal Error, watching loop aborted - Error Details: " + exc.Message,
                                        EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// get the initial lists of files in the directories that 
        /// </summary>
        private void PrimeList()
        {
            foreach (string directory in _DirectoryTrackers.Keys)
            {
                List<string> list = _DirectoryTrackers[directory];

                DirectoryInfo di = new DirectoryInfo(directory);
                FileInfo[] files = di.GetFiles();

                foreach (FileInfo file in files)
                {
                    list.Add(file.Name);
                }
            }
        }

        public void StopWatching()
        {
            _StopEvent.Set();
        }
    }
}
