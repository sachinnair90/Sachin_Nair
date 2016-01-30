using System;

namespace OutputProvider.MovAggr.Common
{
    /// <summary>
    /// Output provider info attribute
    /// 
    /// Add this to the output provider to be used with the application
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class OutputProviderInfo : Attribute
    {
        /// <summary>
        /// Provider description field
        /// </summary>
        private string a_description;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="description">Output provider description</param>
        public OutputProviderInfo(string description)
        {
            a_description = description;
        }

        /// <summary>
        /// Provider description
        /// </summary>
        public string Description
        {
            get { return a_description; }
            set { a_description = value; }
        }
    }
}
