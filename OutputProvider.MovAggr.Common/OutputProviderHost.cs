using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OutputProvider.MovAggr.Common
{
    /// <summary>
    /// Output Provider Host helps getting providers and fetching output
    /// </summary>
    public class OutputProviderHost
    {
        private List<IOutputProvider> outputProviders;
        private byte[] _data;
        private IOutputProvider outputProvider;
        private const string PROVIDERFOLDERNAME = "Providers";

        /// <summary>
        /// List of available output providers
        /// </summary>
        public List<IOutputProvider> OutputProviders
        {
            get
            {
                if (null == outputProviders)
                    CheckForOutputProviders();

                return outputProviders;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OutputProviderHost()
        {
            if (null == outputProviders)
                CheckForOutputProviders();
        }

        /// <summary>
        /// Get available output formats
        /// </summary>
        /// <returns>List of formats</returns>
        public List<string> GetOutputProviderFormats()
        {
            return outputProviders.Select(p => p.Format).ToList();
        }

        /// <summary>
        /// Render output in required format using available providers
        /// </summary>
        /// <param name="data">Output data in byte[]</param>
        /// <param name="format">Output format</param>
        /// <returns></returns>
        public bool Output(byte[] data, string format)
        {
            _data = data;

            outputProvider = GetOutputProvider(format);

            var result = outputProvider.Output(data);

            outputProvider = null;

            return result;
        }

        /// <summary>
        /// Helps load provider assemblies
        /// </summary>
        /// <returns>List of provider assemblies</returns>
        private List<Assembly> LoadOutputProviderAssemblies()
        {
            DirectoryInfo dInfo = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, PROVIDERFOLDERNAME));
            FileInfo[] files = dInfo.GetFiles("*.dll");
            List<Assembly> outputProviderAssemblyList = new List<Assembly>();

            if (null != files)
            {
                foreach (FileInfo file in files)
                {
                    var assembly = Assembly.LoadFile(file.FullName);
                    
                    outputProviderAssemblyList.Add(assembly); 
                }
            }

            return outputProviderAssemblyList;
        }

        /// <summary>
        /// Helps to get output providers
        /// </summary>
        /// <param name="assemblies">List of provider assemblies</param>
        /// <returns>List of output providers</returns>
        private List<IOutputProvider> GetOutputProviders(List<Assembly> assemblies)
        {
            List<Type> availableTypes = new List<Type>();

            foreach (Assembly currentAssembly in assemblies)
                availableTypes.AddRange(currentAssembly.GetTypes());

            // Filter assemblies with OutputProviderInfo attribute and IOutputProvider interface implemented
            List<Type> _providers = availableTypes.FindAll(delegate (Type t)
            {
                List<Type> _interfaces = new List<Type>(t.GetInterfaces());
                object[] arr = t.GetCustomAttributes(typeof(OutputProviderInfo), true);
                return !(arr == null || arr.Length == 0) && _interfaces.Contains(typeof(IOutputProvider));
            });

            // Convert the list of providers to an instantiated list of IOutputProvider
            return _providers.ConvertAll<IOutputProvider>(delegate (Type t) { return Activator.CreateInstance(t) as IOutputProvider; });
        }

        /// <summary>
        /// Checks for available output providers
        /// </summary>
        private void CheckForOutputProviders()
        {
            // Load assemblies
            List<Assembly> outputProviderAssemblies = LoadOutputProviderAssemblies();

            // Get output providers
            List<IOutputProvider> _providers = GetOutputProviders(outputProviderAssemblies);

            foreach (IOutputProvider calc in _providers)
            {
                if(outputProviders == null)
                    outputProviders = new List<IOutputProvider>();

                outputProviders.Add(calc);
            }
        }

        /// <summary>
        /// Get output provider based on output format
        /// </summary>
        /// <param name="format">Output format</param>
        /// <returns>Output provider object</returns>
        private IOutputProvider GetOutputProvider(string format)
        {
            return outputProviders.Where(p => p.Format.Equals(format)).First();
        }
    }
}
