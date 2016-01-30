namespace OutputProvider.MovAggr.Common
{
    /// <summary>
    /// Output provider contract
    /// </summary>
    [OutputProviderInfo("This provider helps to give output in desired format.")]
    public interface IOutputProvider
    {
        /// <summary>
        /// Name of the output provider
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Type of output provider
        /// </summary>
        OutputProviderType Type { get; }

        /// <summary>
        /// Format of output
        /// </summary>
        string Format { get; }

        /// <summary>
        /// Render output using given data
        /// </summary>
        /// <param name="data">Data in byte[] form</param>
        /// <returns>If the output render was successful</returns>
        bool Output(byte[] data);
    }
}
