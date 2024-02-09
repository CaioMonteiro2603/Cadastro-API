using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastro.Models
{
    [Table("Categoria")]
    public class CategoriaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O nome é requerido!")]
        [MaxLength(30)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(250)]
        public string? Descricao { get; set; }

        [NotMapped]
        public string? Token {  get; set; }

        public CategoriaModel()
        {
            
        }

        public CategoriaModel(int categoriaId, string nome, string? descricao)
        {
            CategoriaId = categoriaId;
            Nome = nome;
            Descricao = descricao;
        }

        public CategoriaModel(int categoriaId, string nome, string? descricao, string? token) : this(categoriaId, nome, descricao)
        {
            Token = token;
        }
    }
}
