using System;
using System.Collections.Generic;
using System.IO;

namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            //init
            var Movies = new List<Movie>();
            var pullFile = "ml-latest-small/movies.csv";
            //var pushFile = "movies.check.txt";
            //add all movies from file to list
            StreamReader sr = new StreamReader(pullFile);
            sr.ReadLine();
            while(!sr.EndOfStream){
                Movie movie = new Movie();
                string line = sr.ReadLine();
                int indx = line.IndexOf('"');
                if(indx == -1){
                    //Movie isn't Quoted
                    string[] movieDetails = line.Split(',');
                    movie.movieId = Int32.Parse(movieDetails[0]);
                    movie.title = movieDetails[1];
                    movie.genres = new List<string>(movieDetails[2].Split('|'));
                }
                else{
                    //Movie is Quoted
                    //extract movieId
                    movie.movieId = Int32.Parse(line.Substring(0, indx-1));
                    //remove movieId from line
                    line = line.Substring(indx + 1);
                    //find next quote
                    indx = line.IndexOf('"');
                    //extract title from line
                    movie.title = line.Substring(0, indx);
                    //remove title from line
                    line = line.Substring(indx + 2);
                    //genres
                    movie.genres = new List<string>(line.Split('|'));
                }
                Movies.Add(movie);
            }
            sr.Close();
        }
    }
}
