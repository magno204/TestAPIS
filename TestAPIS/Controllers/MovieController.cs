namespace TestAPIS.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Data.Entities;
    using Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Policy;
    using System.Threading.Tasks;
    using Models;

    [ApiController]
    [Route("Movie")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository movieRepository;
        private readonly IRatingRepository ratingRepository;

        public MovieController(IMovieRepository movieRepository,
            IRatingRepository ratingRepository)
        {
            this.movieRepository = movieRepository;
            this.ratingRepository = ratingRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetList()
        {
            var lista = this.movieRepository.GetAll();
            var ratings = ratingRepository.GetAll();
            // Listado de movies Req
            List<MovieRequest> listMovies = new List<MovieRequest>();
            // organizando...
            foreach (var movie in lista)
            {
                List<RatingRequest> ListRatings = new List<RatingRequest>();
                MovieRequest movieReq = ToMovieReq(movie);
                foreach (var rating in ratings)
                {
                    if (rating.MovieId == movie.Id)
                    {
                        RatingRequest ratingReq = ToNewRatingReq(rating);
                        ListRatings.Add(ratingReq);
                    }
                }
                // agregando lista de ratings a movie
                movieReq.Ratings = ListRatings;
                listMovies.Add(movieReq);
            }
            return Ok(listMovies);
        }

        private RatingRequest ToNewRatingReq(Rating rating)
        {
            return new RatingRequest
            {
                Source = rating.Source,
                Value = rating.Value
            };
        }

        private MovieRequest ToMovieReq(Movie movie)
        {
            return new MovieRequest
            {
                Actors = movie.Actors,
                Awards = movie.Awards,
                BoxOffice = movie.BoxOffice,
                Country = movie.Country,
                Director = movie.Director,
                DVD = movie.DVD,
                Genre = movie.Genre,
                imdbID = movie.imdbID,
                imdbRating = movie.imdbRating,
                imdbVotes = movie.imdbVotes,
                Language = movie.Language,
                Metascore = movie.Metascore,
                Plot = movie.Plot,
                Poster = movie.Poster,
                Production = movie.Production,
                Rated = movie.Rated,
                Released = movie.Released,
                Response = movie.Response,
                Runtime = movie.Runtime,
                Title = movie.Title,
                Type = movie.Type,
                Website = movie.Website,
                Writer = movie.Writer,
                Year = movie.Year,
            };
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> MovieCreate([FromBody] MovieRequest request)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            Movie entity = ToNewMovie(request);
            var newData = movieRepository.Create(entity);
            // Creando Lista de ratings
            List<Rating> ratings = new List<Rating>();
            // extrayendo rating
            foreach (var item in request.Ratings)
            {
                Rating ratingE = ToNewRating(item, newData.Id);
                var newRating = ratingRepository.Create(ratingE);
                ratings.Add(newRating);
            }
            // agregando Listado de ratings a movie
            newData.Ratings = ratings;
            // Actualizando
            var updateMovie = movieRepository.Update(newData);

            return Ok(updateMovie);
        }

        private Rating ToNewRating(RatingRequest data, long movieId)
        {
            return new Rating
            {
                MovieId = movieId,
                Source = data.Source,
                Value = data.Value
            };
        }

        private Movie ToNewMovie(MovieRequest request)
        {
            return new Movie
            {
                Actors = request.Actors,
                Awards = request.Awards,
                BoxOffice = request.BoxOffice,
                Country = request.Country,
                Director = request.Director,
                DVD = request.DVD,
                Genre = request.Genre,
                imdbID = request.imdbID,
                imdbRating = request.imdbRating,
                imdbVotes = request.imdbVotes,
                Language = request.Language,
                Metascore = request.Metascore,
                Plot = request.Plot,
                Poster = request.Poster,
                Production = request.Production,
                Rated = request.Rated,
                Released = request.Released,
                Response = request.Response,
                Runtime = request.Runtime,
                Title = request.Title,
                Type = request.Type,
                Website = request.Website,
                Writer = request.Writer,
                Year = request.Year
            };
        }
    }
}
