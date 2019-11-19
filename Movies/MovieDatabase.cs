﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }

        public List<Movie> Search(string term) {
            List<Movie> results = new List<Movie>();

            foreach(Movie mov in movies) {
                if (mov.Title.Contains(term, StringComparison.OrdinalIgnoreCase)
                    || mov.Director != null && mov.Director.Contains(term, StringComparison.OrdinalIgnoreCase)) {
                    results.Add(mov);
                }
            }


            return results;
        }

        public List<Movie> FilterByMPAA(List<Movie> movList, List<string> filters) {
            List<Movie> results = new List<Movie>();

            foreach(Movie mov in movList) {
                if (filters.Contains(mov.MPAA_Rating)) {
                    results.Add(mov);
                }
            }

            return results;
        }

        public List<Movie> FilterByMinIMDB(List<Movie> movList, float min) {
            List<Movie> results = new List<Movie>();

            foreach(Movie mov in movList) {
                if(mov.IMDB_Rating != null && mov.IMDB_Rating >= min) {
                    results.Add(mov);
                }
            }

            return results;
        }
    }
}
