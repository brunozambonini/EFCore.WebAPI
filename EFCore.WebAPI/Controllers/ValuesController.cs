using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly HeroiContext _context;

        public ValuesController(HeroiContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public ActionResult Get()
        {
            var listHeroi = _context.Herois.ToList(); //LINQ METHOD
            //var listHeroi = (from heroi in _context.Herois select heroi).ToList(); // LINQ Query
            return Ok(listHeroi);
        }

        // GET: api/heroi/nome
        [HttpGet("heroi/{nome}")]
        public ActionResult GetHeroi(string nome)
        {

            //Esses dois métodos de consulta fecha a conexão com o BD
            /*
            var listHeroi = _context.Herois
                .Where(h => h.Nome.Contains(nome)).
                ToList(); //LINQ METHOD
            */

            /*
            var listHeroi = (from heroi in _context.Herois 
                             where heroi.Nome.Contains(nome) // um método de fazer filtro
                             select heroi).ToList(); // LINQ Query
            */

            var listHeroi = _context.Herois
                            .Where(h => EF.Functions.Like(h.Nome, $"%{nome}%"))
                            .OrderBy(h => h.Nome)
                            .ToList();
            
            return Ok(listHeroi);
            
        }

        // GET api/values/5
        [HttpGet("{nameHero}")]
        public ActionResult Get(string nameHero)
        {
            var heroi = new Heroi { Nome = nameHero };

            {
                _context.Herois.Add(heroi);
                //_context.Add(heroi);
                _context.SaveChanges();
            }

            return Ok(); // status 200
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var heroi = _context.Herois
                                .Where(x => x.Id == id)
                                .Single();

            _context.Herois.Remove(heroi);
            _context.SaveChanges();
        }
    }
}
