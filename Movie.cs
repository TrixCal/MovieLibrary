using System;

namespace MovieLibrary
{
    class Movie
    {
        int movieId;
        string title;
        List<string> genres;
        public Movie(int MovieId, string Title, List<string> Genres){
            movieId = MovieId;
            title = Title;
            genres = Genres;
        }
    }
}