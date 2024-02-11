using Cadastro.Models;
using Cadastro.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        [ApiVersion("3.0")]
        public ActionResult<IList<dynamic>> GetProdutos(
            [FromQuery] string dataReferencia,
            [FromQuery] int tamanho = 5)
        {
            var data = (string.IsNullOrEmpty(dataReferencia)) ? DateTime.UtcNow.AddYears(-50) :
                DateTime.ParseExact(dataReferencia, "yyyy-MM-ddTHH:mm:ss.fffffff", null, System.Globalization.DateTimeStyles.RoundtripKind);

            var produtos = _produtoRepository.FindAll(data, tamanho);
            var novaDataReferencia = produtos.LastOrDefault().DataCadastro.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");

            var linkProximo = $"/api/produto?dataReferencia={novaDataReferencia}&tamanho={tamanho}"; 

            if(produtos == null || produtos.Count == 0)
            {
                return NoContent();
            }

            var retorno = new
            {
                produtos,
                linkProximo
            };

            return Ok(retorno); 
        }

        [HttpGet]
        [ApiVersion("2.0", Deprecated = true)]
        public ActionResult<IList<dynamic>> GetProdutos(
            [FromQuery] int pagina = 0,
            [FromQuery] int tamanho = 5)
        {
            var totalGeral = _produtoRepository.Count();
            var totalPaginas = Convert.ToInt16(Math.Ceiling((double) totalGeral/tamanho));
            var linkProximaPagina = (pagina < totalPaginas - 1) ? $"/api/produto?pagina={pagina + 1}&tamanho={tamanho}" : " ";
            var linkPaginaAnterior = (pagina > 0) ? $"/api/produto?pagina={pagina - 1}%tamanho={tamanho}" : " "; 

            if(pagina > totalPaginas)
            {
                return NotFound();
            }

            var produtos = _produtoRepository.FindAll(pagina, tamanho);

            if (produtos == null || produtos.Count == 0)
            {
                return NoContent();
            }

            var retorno = new
            {
                produtos,
                totalPaginas,
                totalGeral,
                linkProximaPagina,
                linkPaginaAnterior
            };

            return Ok(retorno); 


        }

        [HttpGet]
        [ApiVersion("1.0", Deprecated = true)]
        public ActionResult<IList<ProdutoModel>> GetProdutos()
        {
            var produtos = _produtoRepository.FindAll();
            if (produtos == null || produtos.Count == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(produtos);
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProdutoModel> GetProdutos([FromRoute]int id)
        {
            if(id == 0)
            {
                return BadRequest(); 
            } else
            {
                var produto = _produtoRepository.FindById(id); 
                if(produto == null)
                {
                    return NotFound(id);
                } else
                {
                    return Ok(id); 
                }
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult PutProduto([FromRoute] int id, [FromBody] ProdutoModel produtoModel)
        {
            if(id != produtoModel.ProdutoId)
            {
                return BadRequest();
            } else
            {
                var consultaProduto = _produtoRepository.FindById(id); 
                if(consultaProduto == null)
                {
                    return NotFound();
                } else
                {
                    _produtoRepository.Update(produtoModel);
                    return NoContent();
                }
            }
        }

        [HttpPost]
        public ActionResult<ProdutoModel> PostProdutos(ProdutoModel produtoModel)
        {
            try
            {
                _produtoRepository.Insert(produtoModel);

                var url = Request.GetEncodedUrl().EndsWith("/") ?
                                                Request.GetEncodedUrl() :
                                                Request.GetEncodedUrl() + "/";

                var post = new Uri(url + produtoModel.ProdutoId);
                return Created(url, post); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduto([FromRoute]int id)
        {
            if(id == 0)
            {
                BadRequest();
            }

            var produto = _produtoRepository.FindById(id);
            
            if(produto == null)
            {
                return NotFound();
            } else
            {
                _produtoRepository.Delete(produto);
                return NoContent();
            }
        }
    }
}
