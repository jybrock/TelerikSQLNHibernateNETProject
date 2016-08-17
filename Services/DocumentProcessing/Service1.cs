using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;


namespace DocumentProcessing
{
    public partial class Service1 : ServiceBase
    {
        private static ManualResetEvent stopEvent = new ManualResetEvent(false);

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("eSmartChart Document Processing", DateTime.Now.ToLongTimeString() + " - starting the service.");
            ProcessFileUpload fileUpload = new ProcessFileUpload();
            fileUpload.StartFileWatcher();
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("eSmartChart Document Processing", DateTime.Now.ToLongTimeString() + " - stoping the service.");
        }
    }
}
