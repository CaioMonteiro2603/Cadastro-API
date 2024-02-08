using Cadastro.Models;

namespace Cadastro.Repository.Interface
{
    public interface IUsuarioRepository
    {
        public Task<IList<UsuarioModel>> FindAll();

        public Task<UsuarioModel> FindById(int id);

        public Task<UsuarioModel> FindByEmailAndSenha(string email, string senha);

        public Task<int> Insert(UsuarioModel model);

        public void Update(UsuarioModel model);

        public void Delete(int id);
    }
}
