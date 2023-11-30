using cAtPI.Data;
using cAtPI.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata.Ecma335;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CatDTO> CreateCat(CatDTO catDTO) 
        {
            if (CatBase.catList.FirstOrDefault(u => u.Name.ToLower() == catDTO.Name.ToLower()) != null)
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
            catDTO.Id = CatBase.catList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            CatBase.catList.Add(catDTO);
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
            var cat = CatBase.catList.FirstOrDefault(u => u.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            CatBase.catList.Remove(cat);
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
            var cat = CatBase.catList.FirstOrDefault(u => u.Id == id);
            cat.Name = catDTO.Name;
            cat.IsCute = catDTO.IsCute;
            return NoContent();
        }
        [HttpPatch("{id:int}", Name = "UpdatePartialCat")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialCat(int id, JsonPatchDocument<CatDTO> patchDTO) 
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var cat = CatBase.catList.FirstOrDefault(u => u.Id == id);
            if (cat == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(cat, ModelState);
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
