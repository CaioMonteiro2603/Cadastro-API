using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Cadastro.Models
{
    [Table("Produto")]
    [Index(nameof(DataCadastro), IsUnique = false)]
    public class ProdutoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public bool Disponivel { get; set; }

        [StringLength(150)]
        public string Descricao { get; set; }

        public double Valor {  get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime DataExpiracao {  get; set; }
        
        public int UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public UsuarioModel Usuario { get; set; }

        public int CategoriaId { get; set; }

        [ForeignKey(nameof(CategoriaId))]

        public CategoriaModel Categoria { get; set; }

        public ProdutoModel()
        {
            
        }

        public ProdutoModel(int produtoId, string nome, bool disponivel, string descricao,
            double valor, DateTime dataCadastro, DateTime dataExpiracao,
            int usuarioId, UsuarioModel usuario, int categoriaId, CategoriaModel categoria)
        {
            ProdutoId = produtoId;
            Nome = nome;
            Disponivel = disponivel;
            Descricao = descricao;
            Valor = valor;
            DataCadastro = dataCadastro;
            DataExpiracao = dataExpiracao;
            UsuarioId = usuarioId;
            Usuario = usuario;
            CategoriaId = categoriaId;
            Categoria = categoria;
        }
    }
}
