namespace Evaluacion2.Models
{
    public class Dog
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TotalDogs { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalDogs / PageSize);
    }

    public class DogListResult
    {
        public List<Dog> Data { get; set; }
        public int Count { get; set; }
    }
}
