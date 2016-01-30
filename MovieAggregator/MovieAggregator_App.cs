using OutputProvider.MovAggr.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MovieAggregator
{
    /// <summary>
    /// Main application class
    /// </summary>
    class MovieAggregator_App
    {
        private static List<IMovieDetails> movies;

        /// <summary>
        /// Application invoker
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            movies = new List<IMovieDetails>();
            var exportData = true;
            char keyResponse = Char.MinValue;
            OutputProviderHost outputProviderHost = null;

            try
            {
                outputProviderHost = new OutputProviderHost();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Application malfunctioned!");
            }

            Console.WriteLine("Welcome to your Movie Aggregator app!");
            Console.WriteLine("Let's get started!");

            // Add movie to the list
            AddMovie();

            Console.Write("Would you like to export the saved movies details to a readable format?");
            keyResponse = Console.ReadLine()[0];

            if (keyResponse != 'y' && keyResponse != 'Y')
                exportData = false;

            if (exportData)
            {
                // Create data from list of movies
                var movieDetailStr = CreateDataFromList();

                try
                {
                    // Export data using output providers
                    ExportData(outputProviderHost, Encoding.UTF8.GetBytes(movieDetailStr));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Application malfunctioned! Export Failed!");
                }

                Console.ReadKey();
            }
        }

        /// <summary>
        /// Create data using list of movies
        /// </summary>
        /// <param name="movies">List of movies to create data from</param>
        /// <returns>Data of string type</returns>
        private static string CreateDataFromList()
        {
            var detailStr = string.Empty;
            var textInfo = new CultureInfo("en-US", false).TextInfo;

            foreach (var movieDetail in movies)
            {
                detailStr = string.Concat(detailStr, "==== MOVIE DETAIL ====");
                detailStr = string.Concat(detailStr, "\r\n\r\n");
                                     
                detailStr = string.Concat(detailStr, "Name: ", textInfo.ToTitleCase(movieDetail.Name), "\r\n");
                detailStr = string.Concat(detailStr, "Runtime (minutes): ", movieDetail.Runtime, "\r\n");
                detailStr = string.Concat(detailStr, "Language: ", movieDetail.Language, "\r\n");
                detailStr = string.Concat(detailStr, "LeadActor: ", textInfo.ToTitleCase(movieDetail.LeadActor), "\r\n");
                detailStr = string.Concat(detailStr, "Genre: ", textInfo.ToTitleCase(movieDetail.Genre), "\r\n");
                                       
                detailStr = string.Concat(detailStr, "\r\n\r\n");
            }

            return detailStr;
        }

        /// <summary>
        /// Export data using output provider
        /// </summary>
        /// <param name="outputProviderHost">Output provider host object</param>
        /// <param name="data">Output data in byte[]</param>
        private static void ExportData(OutputProviderHost outputProviderHost, byte[] data)
        {
            var outputFormats = outputProviderHost.GetOutputProviderFormats();

            if (outputFormats.Count > 0)
            {
                Console.WriteLine("In which file format would you like to export the saved movies details?");

                for (int index = 0; index < outputFormats.Count; index++)
                {
                    Console.WriteLine(string.Concat(index + 1, ": ", outputFormats[index]));
                }

                Console.Write("Enter the format number: ");
                UInt16 formatChoice = Convert.ToUInt16(Console.ReadLine());

                var result = outputProviderHost.Output(data, outputFormats[formatChoice - 1]);

                if (result)
                {
                    Console.WriteLine("Data exported successfully!");
                }
                else
                {
                    Console.WriteLine("Data export failed!");
                }
            }
            else
            {
                Console.WriteLine("Data export failed!");
            }
        }

        /// <summary>
        /// Add movie to the list of movies 
        /// </summary>
        /// <param name="movies">List of movies to be added to</param>
        private static void AddMovie()
        {
            var addMovie = true;
            char keyResponse = Char.MinValue;

            while (addMovie)
            {
                var movie = new MovieDetails();

                Console.WriteLine("Add details for you favorite movie!");

                Console.Write("Name: ");
                movie.Name = Console.ReadLine();

                Console.Write("Run Time (minutes): ");
                movie.Runtime = Convert.ToUInt16(Console.ReadLine());

                Console.Write("Language: ");
                movie.Language = Console.ReadLine();

                Console.Write("Lead Actor: ");
                movie.LeadActor = Console.ReadLine();

                Console.Write("Genre: ");
                movie.Genre = Console.ReadLine();

                movies.Add(movie);

                Console.Write("Would you like add more movies?");
                keyResponse = Console.ReadLine()[0];

                if (keyResponse != 'y' && keyResponse != 'Y')
                    addMovie = false;
            }
        }
    }
}
