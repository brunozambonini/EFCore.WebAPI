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
    public class HeroiController : ControllerBase
    {
        private readonly IEFCoreRepository _repository;

        public HeroiController(IEFCoreRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<HeroiController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            /*
            //estrutura do heroi
            var heroi = new Heroi();
            return Ok(heroi);
            */
            
            try
            {
                var herois = await _repository.GetAllHerois(true);
                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            
        }

        // GET api/<HeroiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var heroi = await _repository.GetHeroiById(id, true);
                return Ok(heroi);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<HeroiController>
        [HttpPost]
        public async Task<ActionResult> Post(Heroi model)
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

        // PUT api/<HeroiController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Heroi model)
        {
            // é preciso usar o AsNoTracking, porque caso seja usado Find, ele 'trava' o objeto e 
            // não é possivel fazer mudanças nele, logo não da pra salvar e fazer o update
            try
            {
                var heroi = await _repository.GetHeroiById(id);
                if (heroi != null)
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

        // DELETE api/<HeroiController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var heroi = await _repository.GetHeroiById(id);
                if (heroi != null)
                {
                    _repository.Delete(heroi);
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
