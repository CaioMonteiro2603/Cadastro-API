using Cadastro.Models;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Data
{
    public class DataContext : DbContext
    {

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<ProdutoModel> Produto { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected DataContext()
        {
        }
    }
}
