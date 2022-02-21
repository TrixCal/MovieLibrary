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
        public void Display(){
            string genre = "";
            for(int i = 0; i < genres.Count; i++){
                if(i != 0){
                    genre += "|" + genres[i];
                }
                else{
                    genre = genres[i];
                }
            }
            Console.WriteLine($"{movieId}: {title}, {genre}");
        }
        public override string ToString(){
            string genre = "";
            for(int i = 0; i < genres.Count; i++){
                if(i != 0){
                    genre += "|" + genres[i];
                }
                else{
                    genre = genres[i];
                }

            }
            string _title;
            int index = title.IndexOf(',');
            if(index == -1){
                _title = title;
            }
            else{
                _title = $"\"{title}\"";
            }
            return $"{movieId},{_title},{genre}";
        }
    }
}