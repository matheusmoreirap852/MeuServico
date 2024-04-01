using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class CriarUsuarioModel
    {

        [Required(ErrorMessage = "O campo 'Email' é obrigatório e deve ser único.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo 'Usuário' é obrigatório e deve ser único.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Nova Senha é obrigatório e deve conter 8 caracteres, incluindo números, letras e caracteres especiais.")]
        public string? Password { get; set; }
        [Required (ErrorMessage ="O campo 'Nome' é obrigatório.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "O campo 'Sobreno' é obrigatório.")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo 'CPF' é obrigatório.")]
        public string? cpf {  get; set; }
    }
}
