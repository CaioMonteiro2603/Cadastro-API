using AutoMapper;
using Cadastro.Data;
using Cadastro.Models;
using Cadastro.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;
        

        public UsuarioRepository(DataContext context)
        {
            _context = context; 
        }

        public async Task<IList<UsuarioModel>> FindAll()
        {
            var findAll = await _context.Usuarios.AsNoTracking().ToListAsync();

            return findAll;
        }

        public async Task<UsuarioModel> FindById(int id)
        {
            var findId = await _context.Usuarios
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync(i => i.UsuarioId == id);

            return findId; 
        }

        public async void Delete(int id)
        {
            var usuario = new UsuarioModel();
            usuario.UsuarioId = id;

            _context.Usuarios.Remove(usuario);
            _context.SaveChangesAsync(); 
        }

        public async Task<UsuarioModel> FindByEmailAndSenha(string email, string senha)
        {
            var findEmailSenha = await _context.Usuarios
                                              .AsNoTracking()
                                              .FirstOrDefaultAsync(g => g.EmailUsuario == email && g.Senha == senha);

            return findEmailSenha; 
        }

        public async Task<int> Insert(UsuarioModel usuarioModel)
        {
            _context.Usuarios.Add(usuarioModel);
            _context.SaveChanges();

            return usuarioModel.UsuarioId; 
        }

        public async void Update(UsuarioModel usuarioModel)
        {
            _context.Usuarios.Update(usuarioModel);
            _context.SaveChanges(); 
        }

        
    }
}
