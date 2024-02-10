using Cadastro.Models;

namespace Cadastro.Repository.Interface
{
    public interface IProdutoRepository
    {
        public IList<ProdutoModel> FindAll();

        public IList<ProdutoModel> FindAll(int pagina, int tamanho);

        public IList<ProdutoModel> FindAll(DateTime? dataReferencia, int tamanho);

        public int Count();

        public IList<ProdutoModel> FindByName(string name);

        public ProdutoModel FindById(int id);

        public int Insert(ProdutoModel model);

        public void Update(ProdutoModel model);

        public void Delete(int id);

        public void Delete(ProdutoModel model);
    }
}
