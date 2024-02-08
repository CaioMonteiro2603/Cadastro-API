using AutoMapper;
using Cadastro.Models;
using Cadastro.Repository.Interface;
using Cadastro.Services;
using Cadastro.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioResponseVM>>> FindAllAsync()
        {
            var usuario = await _usuarioRepository.FindAll();

            if(usuario != null & usuario.Count > 0)
            {
                var response = _mapper.Map<List<UsuarioResponseVM>>(usuario);
                return Ok(response); 
            } else
            {
                return NoContent(); 
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioResponseVM>> FindIdAsync(int id)
        {
            var findId = await _usuarioRepository.FindById(id);

            if(findId != null)
            {
                var response = _mapper.Map<UsuarioResponseVM>(findId);
                return Ok(response);
            } else
            {
                return NotFound(); 
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponseVM>> Post([FromBody] UsuarioRequestVM usuarioRequestVM)
        {
            if(!ModelState.IsValid)
            {
                BadRequest();
            }

            var usuarioModel = _mapper.Map<UsuarioModel>(usuarioRequestVM);
            await _usuarioRepository.Insert(usuarioModel); 

            var url = Request.GetEncodedUrl().EndsWith("/") ?
                        Request.GetEncodedUrl() :
                        Request.GetEncodedUrl() + "/";

            url += usuarioModel.UsuarioId;
            var usuarioResponse = _mapper.Map<UsuarioResponseVM>(usuarioModel);

            return Created(url, usuarioResponse); 
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] UsuarioRequestVM usuarioRequestVM)
        {
            if(!ModelState.IsValid || (id != usuarioRequestVM.UsuarioId))
            {
                return BadRequest(); 
            } else
            {
                var userRequest = _mapper.Map<UsuarioModel>(usuarioRequestVM);
                _usuarioRepository.Update(userRequest);
                return NoContent(); 
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var usuario = _usuarioRepository.FindById(id);

            if(usuario == null)
            {
                return NotFound();
            }
            _usuarioRepository.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResponseVM>> Login([FromBody] LoginRequestVM loginRequestVM)
        {
            var loginUsuario = await _usuarioRepository.FindByEmailAndSenha(
                loginRequestVM.EmailUsuario,
                loginRequestVM.SenhaUsuario); 

            if(loginUsuario == null)
            {
                return NotFound();
            } else
            {
                var token = AuthenticationService.GetToken(loginUsuario);
                var loginResponseVM = _mapper.Map<LoginResponseVM>(loginUsuario);
                loginResponseVM.Token = token;
                return Ok(loginResponseVM); 
            }
        }
    }
}
