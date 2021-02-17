using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly IEFCoreRepository _repository;

        public BatalhaController(IEFCoreRepository repository)
        {
            _repository = repository;
        }

        
        // GET: api/<BatalhasController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var herois = await _repository.GetAllBatalhas(true);

                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET: api/<HeroiController>/batalha/nomeBatalha
        [HttpGet("filtro/{nome}")]
        public async Task<ActionResult> GetFiltro(string nome)
        {
            try
            {
                var batalhas = await _repository.GetBatalhasByName(nome, true);
                return Ok(batalhas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

        }

        // GET api/<BatalhasController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var batalha = await _repository.GetBatalhaById(id, true);
                return Ok(batalha);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<BatalhasController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Batalha model)
        {
            try
            {
                _repository.Add(model);
                if (await _repository.SaveChangeAsync())
                {
                    return Ok("BAZINGA");
                }
                else
                {
                    return Ok("Não Salvou");
                }
                
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }

        
        // PUT api/<BatalhasController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Batalha model)
        {
            try
            {
                var batalha = await _repository.GetBatalhaById(id);
                if (batalha != null)
                {
                    _repository.Update(model);
                    if (await _repository.SaveChangeAsync())
                    {
                        return Ok("BAZINGA");
                    }
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não Atualizado!");
        }



        // DELETE api/<BatalhasController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var batalha = await _repository.GetBatalhaById(id);
                if (batalha != null)
                {
                    _repository.Delete(batalha);
                    if (await _repository.SaveChangeAsync())
                    {
                        return Ok("BAZINGA");
                    }
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não Deletado!");
        }  
    }
}
