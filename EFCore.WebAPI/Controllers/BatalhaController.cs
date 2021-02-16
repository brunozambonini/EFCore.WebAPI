using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {

        private readonly HeroiContext _context;
        public BatalhaController(HeroiContext context)
        {
            _context = context;
        }

        // GET: api/<BatalhasController>
        [HttpGet]
        public ActionResult Get()
        {
            {
                try
                {
                    var listBatlhas = _context.Batalhas.ToList(); //LINQ METHOD
                    return Ok(listBatlhas);
                }
                catch (Exception ex)
                {

                    return BadRequest($"Erro: {ex}");
                }
            }
        }

        // GET api/<BatalhasController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                return Ok(new Batalha());
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<BatalhasController>
        [HttpPost]
        public ActionResult Post([FromBody] Batalha model)
        {
            try
            {
                _context.Batalhas.Add(model);
                _context.SaveChanges();
                return Ok("BAZINGA");
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT api/<BatalhasController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Batalha model)
        {
            // é preciso usar o AsNoTracking, porque caso seja usado Find, ele 'trava' o objeto e 
            // não é possivel fazer mudanças nele, logo não da pra salvar e fazer o update
            if (_context.Batalhas.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
            {
                _context.Update(model);
                _context.SaveChanges();

                return Ok("BAZINGA");
            }
            return Ok("Não Encontrado!");
        }

        // DELETE api/<BatalhasController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var batalha = _context.Batalhas
                                .Where(x => x.Id == id)
                                .Single();

                _context.Batalhas.Remove(batalha);
                _context.SaveChanges();

                return Ok("BAZINGA");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }
    }
}
