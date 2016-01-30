using System;

namespace MovieAggregator
{
    /// <summary>
    /// Movie Details contract
    /// </summary>
    interface IMovieDetails
    {
        /// <summary>
        /// Name of movie
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Movie runtime in minutes
        /// </summary>
        UInt16 Runtime { get; set; }

        /// <summary>
        /// Lead actor in the movie
        /// </summary>
        string LeadActor { get; set; }

        /// <summary>
        /// Genre of the movie
        /// </summary>
        string Genre { get; set; }

        /// <summary>
        /// Language used in movie
        /// </summary>
        string Language { get; set; }
    }
}
