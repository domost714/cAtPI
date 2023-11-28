using cAtPI.Models.DTO;

namespace cAtPI.Data
{
    public static class CatBase
    {
        public static List<CatDTO> catList = new List<CatDTO>()
        {
                new() {Id = 1, Name = "Winston", IsCute = true},
                new() {Id = 2, Name = "Hana", IsCute = true},
                new() {Id = 3, Name = "Prezesowa", IsCute = true},
                new() {Id = 4, Name = "Burek", IsCute = false},
                new() {Id = 5, Name = "Kuku"}
        };
    }
}
