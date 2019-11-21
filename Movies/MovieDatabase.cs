using System;
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
    public static class MovieDatabase
    {
        private static List<Movie> movies;


        public static List<Movie> All {
            get {
                if (movies == null) {
                    using (StreamReader file = System.IO.File.OpenText("movies.json")) {
                        string json = file.ReadToEnd();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }

                return movies;
            }
        }

        public static List<Movie> Search(List<Movie> movList, string term) {
            List<Movie> results = new List<Movie>();

            foreach(Movie mov in movList) {
                if (mov.Title.Contains(term, StringComparison.OrdinalIgnoreCase)
                    || mov.Director != null && mov.Director.Contains(term, StringComparison.OrdinalIgnoreCase)) {
                    results.Add(mov);
                }
            }


            return results;
        }

        public static List<Movie> FilterByMPAA(List<Movie> movList, List<string> filters) {
            List<Movie> results = new List<Movie>();

            foreach(Movie mov in movList) {
                if (filters.Contains(mov.MPAA_Rating)) {
                    results.Add(mov);
                }
            }

            return results;
        }

        public static List<Movie> FilterByMinIMDB(List<Movie> movList, float min) {
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
