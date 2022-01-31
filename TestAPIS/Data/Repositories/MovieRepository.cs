namespace TestAPIS.Data.Repositories
{
    using Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Repositories;
    //using Models;

    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly DataContext context;

        public MovieRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
