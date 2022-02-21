using System;
using System.Collections.Generic;
namespace MovieLibrary
{
    class Movie
    {
        public int movieId {get; set;}
        public string title {get; set;}
        public List<string> genres {get; set;}
        public Movie(){
            genres = new List<string>();
        }
        public Movie(int MovieId, string Title, List<string> Genres){
            movieId = MovieId;
            title = Title;
            genres = Genres;
        }
        public override string ToString(){
            string genre = "";
            foreach(string s in genres){
                genre = genre + " | "  + s;
            }
            return $"{title}, {genre}";
        }
    }
}