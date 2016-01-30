using System;
using OutputProvider.MovAggr.Common;
using System.IO;
using System.Text;

namespace TextOutputProvider.MovAggr.Writer
{
    /// <summary>
    /// Text output provider
    /// </summary>
    [OutputProviderInfo("This provider helps to give output in text file format.")]
    public class TextOutputProvider : IOutputProvider
    {
        private string fileName = "TextOutput_{0}.txt";
        private string filePath = @"{0}";

        private string format = string.Empty;
        private string name = string.Empty;
        private OutputProviderType type = OutputProviderType.FILE;

        /// <summary>
        /// Constructor
        /// </summary>
        public TextOutputProvider()
        {
            format = ".txt";
            name = "TextOutput";
            type = OutputProviderType.FILE;
        }

        /// <summary>
        /// Output format
        /// </summary>
        public string Format { get { return format; } }

        /// <summary>
        /// Provider name
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// Output Provider type
        /// </summary>
        public OutputProviderType Type { get { return type; } }

        /// <summary>
        /// Render output using given data
        /// </summary>
        /// <param name="data">Data in byte[] form</param>
        /// <returns>If the output render was successful</returns>
        public bool Output(byte[] data)
        {
            try {
                fileName = String.Format(fileName, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
                filePath = String.Format(filePath, fileName);

                // Create new file
                File.Create(filePath).Close();

                // Push data
                File.WriteAllText(filePath, Encoding.UTF8.GetString(data));

                if (File.Exists(filePath))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to write to file!");
            }

            return false;
        }
    }
}
