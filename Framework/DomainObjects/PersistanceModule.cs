using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;

namespace DomainObjects
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PersistanceModule
    {
        // this array contains all assemblies that have types that need to be scanned by
        // nHibernate for mappings. Only include assemblies that contain domain objects
        private static string[] AssembliesToLoad = { "DomainObjects" };

        static PersistanceModule()
        {
            Initialize();
        }

        private static void Initialize()
        {
            // load all assemblies in AssembliesToLoad array

            List<Assembly> assemblyList = new List<Assembly>();
            foreach (string assemblyName in AssembliesToLoad)
            {
                assemblyList.Add(Assembly.Load(assemblyName));
            }

            IConfigurationSource config = ActiveRecordSectionHandler.Instance;

            if (!ActiveRecordStarter.IsInitialized)
            {
                ActiveRecordStarter.Initialize(assemblyList.ToArray(), config);
            }
        }
    }
}
