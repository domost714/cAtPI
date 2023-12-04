using cAtPI.Data;
using cAtPI.Models;
using cAtPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace cAtPI.Controllers
{
    [Route("api/cAtPI")]
    [ApiController]
    public class CAtPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CAtPIController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CatDTO>> GetCats()
        {
            return Ok(_db.Cats);
        }
        [HttpGet("{id:int}", Name = "GetCat")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CatDTO> GetCats(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var cat = _db.Cats.FirstOrDefault(u => u.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CatDTO> CreateCat(CatDTO catDTO) 
        {
            if (_db.Cats.FirstOrDefault(u => u.Name.ToLower() == catDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Cat already exists!");
                return BadRequest(ModelState);
            }
            if (catDTO == null)
            {
                return BadRequest(catDTO);
            }
            if (catDTO.Id > 0) 
            {
                return BadRequest(catDTO);
            }
            Cat model = new()
            {
                Id = catDTO.Id,
                Name = catDTO.Name,
                IsCute = catDTO.IsCute
            };
            _db.Cats.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetCat", new { id = catDTO.Id }, catDTO);
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteCat")]
        public IActionResult DeleteCat(int id) 
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var cat = _db.Cats.FirstOrDefault(u => u.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            _db.Cats.Remove(cat);
            _db.SaveChanges();
            return NoContent(); 
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateCat")]
        public IActionResult UpdateCat(int id, [FromBody]CatDTO catDTO)
        {
            if (catDTO == null || id != catDTO.Id)
            {
                return BadRequest();
            }
            Cat model = new()
            {
                Id = catDTO.Id,
                Name = catDTO.Name,
                IsCute = catDTO.IsCute
            };
            _db.Cats.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
