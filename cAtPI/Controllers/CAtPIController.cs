using cAtPI.Data;
using cAtPI.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace cAtPI.Controllers
{
    [Route("api/cAtPI")]
    [ApiController]
    public class CAtPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CatDTO>> GetCats()
        {
            return Ok(CatBase.catList);
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
            var cat = CatBase.catList.FirstOrDefault(u => u.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CatDTO> CreateCat(CatDTO catDTO) 
        {
            if (catDTO == null)
            {
                return BadRequest(catDTO);
            }
            if (catDTO.Id > 0) 
            {
                return BadRequest(catDTO);
            }
            catDTO.Id = CatBase.catList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            CatBase.catList.Add(catDTO);
            return CreatedAtRoute("GetCat", new { id = catDTO.Id }, catDTO);
        }
    }
}
