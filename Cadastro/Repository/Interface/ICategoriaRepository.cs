using Cadastro.Models;

namespace Cadastro.Repository.Interface
{
    public interface ICategoriaRepository
    {
        public IList<CategoriaModel> FindAll();

        public CategoriaModel FindById(int id);

        public int Insert(CategoriaModel model);

        public void Update(CategoriaModel model);

        public void Delete(int id);
    }
}
