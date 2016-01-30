using System;

namespace MovieAggregator
{
    /// <summary>
    /// Details of movie
    /// </summary>
    public class MovieDetails : IMovieDetails
    {
        /// <summary>
        /// Genre of the movie
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Language used in the movie
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Lead actor in the movie
        /// </summary>
        public string LeadActor { get; set; }

        /// <summary>
        /// Name of the movie
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Runtime of the movie in minutes
        /// </summary>
        public UInt16 Runtime { get; set; }
    }
}
