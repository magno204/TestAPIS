namespace TestAPIS.Data.Entities
{
    public class Rating : IEntity
    {
        public long Id { get; set; }

        public long MovieId { get; set; }

        public string Source { get; set; }

        public string Value { get; set; }
    }
}
