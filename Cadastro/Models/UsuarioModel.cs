using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastro.Models
{
    [Table("Usuarios")]
    public class UsuarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(30)]
        public string NomeUsuario { get; set; }

        [Required]
        [MaxLength(30)]
        public string EmailUsuario { get; set; }

        [Required]
        [MaxLength(30)]
        public string Senha { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Regra { get; set; }

        public UsuarioModel()
        {

        }

        public UsuarioModel(int usuarioId, string nomeUsuario, string emailUsuario, string senha, string? regra)
        {
            UsuarioId = usuarioId;
            NomeUsuario = nomeUsuario;
            EmailUsuario = emailUsuario;
            Senha = senha;
            Regra = regra;
        }
    }
}