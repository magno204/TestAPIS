using TestAPIS.Data.Entities;

namespace TestAPIS.Data.Repositories
{
    public class RatingRepositories : GenericRepository<Rating>, IRatingRepository
    {
        private readonly DataContext context;

        public RatingRepositories(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
