﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Windows;

namespace MWMP
{
    /// <summary>
    /// Manage the loading file and the service creator
    /// </summary>
    public class ModuleManager
    {
        #region Static fields
        static private ModuleManager instance;
        #endregion

        #region Fields
        static private Dictionary<string, Service> services = new Dictionary<string, Service>();
        #endregion /// Fields

        #region Methods
        /// <summary>
        /// Add services with an XML file
        /// </summary>
        /// <param name="filename"></param>
        static public void load(string filename)
        {
            XElement xml = XElement.Load(filename);
            IEnumerable<XElement> services = xml.Elements("service");
            foreach (XElement e in services)
            {
                Service s;
                try
                {
                    s = new Service(e.Element("name").Value, e.Element("file").Value,
                                    e.Element("class").Value, e.Element("interface").Value,
                                    e.Element("unique").Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    continue;
                }
                ModuleManager.services.Add(s.Name, s);
            }
        }

        /// <summary>
        /// Regist a service
        /// </summary>
        /// <param name="s"></param>
        static public void addService(ref Service s)
        {
            services.Add(s.Name, s);
        }

        /// <summary>
        /// Create and return a service
        /// </summary>
        /// <typeparam name="T">Interface of the service</typeparam>
        /// <param name="serviceName">Name of the service</param>
        /// <returns>The service or default(T)</returns>
        static public T getInstanceOf<T>(string serviceName)
        {
            Service s;
            if (services.TryGetValue(serviceName, out s))
            {
                if (s.Unique)
                    return (T)s.Instance;
                return (T)s.Constructor.Invoke(new Object[0]);
            }
            return default(T);
        }
        #endregion /// Methods
    }
}
