using Cadastro.Data;
using Cadastro.Models;
using Cadastro.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataContext _context;

        public ProdutoRepository(DataContext context)
        {
            _context = context;
        }

        public int Count()
        {
            var count = _context.Produto.Count();

            return count; 
        }

        public void Delete(int id)
        {
            var produto = new ProdutoModel()
            {
                ProdutoId = id, 
            };

            Delete(produto); 
        }

        public void Delete(ProdutoModel model)
        {
            _context.Produto.Remove(model);
            _context.SaveChanges();
        }

        public IList<ProdutoModel> FindAll()
        {
            var produtos = _context.Produto.AsNoTracking().ToList();

            return produtos == null ? new List<ProdutoModel>() : produtos;
        }

        public IList<ProdutoModel> FindAll(int pagina = 0, int tamanho = 5)
        {
            var produtos = _context.Produto
                                   .OrderBy(p => p.ProdutoId)
                                   .Skip(tamanho * pagina)
                                   .Take(tamanho)
                                   .AsNoTracking()
                                   .ToList();

            return produtos == null ? new List<ProdutoModel>() : produtos; 
        }

        public IList<ProdutoModel> FindAll(DateTime? dataReferencia, int tamanho)
        {
            var produtos = _context.Produto
                                   .Where(p => p.DataCadastro > dataReferencia)
                                   .OrderBy(p => p.DataCadastro)
                                   .Take(tamanho)
                                   .AsNoTracking()
                                   .ToList();

            return produtos == null ? new List<ProdutoModel>() : produtos; 
        }

        public ProdutoModel FindById(int id)
        {
            var produtos = _context.Produto
                                   .AsNoTracking()
                                   .FirstOrDefault(p => p.ProdutoId == id);

            return produtos; 
        }

        public IList<ProdutoModel> FindByName(string name)
        {
            var produtos = _context.Produto
                                   .AsNoTracking()
                                   .Include(p => p.Categoria)
                                   .Where(p => p.Nome.ToUpper().Contains(name.ToUpper()))
                                   .ToList();

            return produtos == null ? new List<ProdutoModel>() : produtos; 
        }

        public int Insert(ProdutoModel model)
        {
            _context.Produto.Add(model);
            _context.SaveChanges();

            return model.ProdutoId; 
        }

        public void Update(ProdutoModel model)
        {
            _context.Produto.Update(model);
            _context.SaveChanges();
        }
    }
}
