using Cadastro.Data;
using Cadastro.Models;
using Cadastro.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DataContext _context;

        public CategoriaRepository(DataContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var categoria = new CategoriaModel()
            {
                CategoriaId = id
            };

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
        }

        public IList<CategoriaModel> FindAll()
        {
            var findAll = _context.Categorias.AsNoTracking().ToList();
            return findAll; 
        }

        public CategoriaModel FindById(int id)
        {
            var findId = _context.Categorias
                                 .AsNoTracking()
                                 .FirstOrDefault(i => i.CategoriaId == id);
            return findId; 
        }

        public int Insert(CategoriaModel categoriaModel)
        {
            _context.Categorias.Add(categoriaModel);
            _context.SaveChanges();

            return categoriaModel.CategoriaId; 
        }

        public int Insert(CategoriaRepository model)
        {
            throw new NotImplementedException();
        }

        public void Update(CategoriaModel categoriaModel)
        {
            _context.Categorias.Update(categoriaModel);
            _context.SaveChanges(); 
        }

    }
}
