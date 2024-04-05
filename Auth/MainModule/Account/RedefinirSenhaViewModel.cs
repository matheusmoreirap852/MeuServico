// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI
{
    public class RedefinirSenhaViewModel
    {
        public string? NewPassword { get; set; } 

        public string? UserId { get; set; }
        public string? Token { get; set; }
        [Required(ErrorMessage = "O campo Nova Email é obrigatório.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O campo Nova Nome é obrigatório.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "O campo Nova Nome é obrigatório.")]
        public string? Nome { get; set; }

       
        [Required(ErrorMessage = "O campo Nova Senha é obrigatório.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "O campo Confirme a Nova Senha é obrigatório.")]
        [Compare("NewPassword", ErrorMessage = "As senhas não coincidem.")]
        public string? ConfirmPassword { get; set; }
    }
}