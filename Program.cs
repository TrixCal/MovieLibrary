using System;
using System.Collections.Generic;
using System.IO;
using NLog.Web;

namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            //init
            var Movies = new List<Movie>();
            var file = "ml-latest-small/movies.csv";
            //Logger
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
            logger.Info("Program Started");
            //add all movies from file to list
            try
            {
                StreamReader sr = new StreamReader(file);
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
                logger.Info($"{Movies.Count} movies added to list");
                sr.Close();
            }
            catch (Exception ex){
                logger.Error(ex.Message);
            }
            //Display or Add Movies
            string c = "";
            while(c != "exit"){
                Console.WriteLine("1) Add new Movie\n2) Display Movies\n(Type \"exit\" to leave)");
                c = Console.ReadLine();
                if(c == "1"){
                    try{
                        //Movie Title input
                        Console.Write("Movie Title: ");
                        string title = Console.ReadLine();
                        //Duplicate Check
                        bool duplicate = false;
                        foreach(Movie m in Movies){
                            if(title.ToLower() == m.title.ToLower())
                                duplicate = true;
                        }
                        if(!duplicate){
                            //Calculate MovieID
                            int movieID = 0;
                            foreach(Movie m in Movies){
                                if(movieID <= m.movieId)
                                    movieID = m.movieId + 1;
                            }
                            //Input Genres
                            List<string> genres = new List<string>();
                            while(c != "n"){
                                Console.Write("Genre(s) (Type \"n\" to stop): ");
                                c = Console.ReadLine();
                                if(c != "n")
                                    genres.Add(c);
                            }
                            genres.Sort();
                            //Create Movie object
                            Movie movie = new Movie(movieID, title, genres);
                            //Add to List
                            Movies.Add(movie);
                            //Add to File
                            StreamWriter sw = new StreamWriter(file, true);
                            sw.WriteLine(movie.ToString());
                            sw.Close();
                        }
                        else{
                            logger.Info("Movie was a duplicate");
                        }
                    }catch (Exception ex){
                        logger.Error(ex.Message);
                    }
                }
                else if(c == "2"){
                    foreach(Movie m in Movies){
                        m.Display();
                    }
                }
            }
        }
    }
}
