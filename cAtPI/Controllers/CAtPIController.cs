using cAtPI.Data;
using cAtPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace cAtPI.Controllers
{
    [Route("api/cAtPI")]
    [ApiController]
    public class CAtPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<CatDTO> GetCats()
        {
            return CatBase.catList;
        }
        [HttpGet("{id:int}")]
        public CatDTO GetCats(int id)
        {
            return CatBase.catList.FirstOrDefault(u => u.Id == id);
        }
    }
}
