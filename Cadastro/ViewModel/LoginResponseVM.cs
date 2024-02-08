namespace Cadastro.ViewModel
{
    public class LoginResponseVM
    {
        public int UsuarioId { get; set; }

        public string EmailUsuario { get; set; }

        public string NomeUsuario { get; set; }


        public string? Regra { get; set; }

        public string Token { get; set; }

        public LoginResponseVM()
        {

        }

        public LoginResponseVM(int usuarioId, string emailUsuario, string nomeUsuario, string? regra, string token)
        {
            UsuarioId = usuarioId;
            NomeUsuario = nomeUsuario;
            EmailUsuario = emailUsuario;
            Regra = regra;
            Token = token;
        }
    }
}
