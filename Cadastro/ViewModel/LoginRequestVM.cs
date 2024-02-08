using System.ComponentModel.DataAnnotations;

namespace Cadastro.ViewModel
{
    public class LoginRequestVM
    {
        [Required(ErrorMessage = "O email é requerido!")]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "A senha é requerida!")]
        public string SenhaUsuario { get; set; }

        public LoginRequestVM()
        {

        }

        public LoginRequestVM(string emailusuario, string senhaUsuario)
        {
            EmailUsuario = emailusuario;
            SenhaUsuario = senhaUsuario;
        }
    }
}
