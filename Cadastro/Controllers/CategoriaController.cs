using Cadastro.Models;
using Cadastro.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public List<CategoriaModel> FindAll()
        {
            return (List<CategoriaModel>) _categoriaRepository.FindAll();
        }

        [HttpGet("{id:int}")]
        public CategoriaModel FindById([FromRoute]int id)
        {
            var findId = _categoriaRepository.FindById(id);
            return findId; 
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _categoriaRepository.Delete(id);
        }

        [HttpPost]
        public int Post([FromBody] CategoriaModel CategoriaModel) 
        {
            _categoriaRepository.Insert(CategoriaModel);
            return CategoriaModel.CategoriaId; 
        }

        [HttpPut("{id:int}")]
        public bool Put([FromRoute] int id, [FromBody] CategoriaModel categoriaModel) { 
            if(categoriaModel.CategoriaId == id)
            {
                _categoriaRepository.Update(categoriaModel);
                return true;
            } else
            {
                return false; 
            }
        }
    }
}
