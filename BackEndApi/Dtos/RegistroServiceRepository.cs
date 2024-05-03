using System;
using System.ComponentModel.DataAnnotations;

namespace BackEndApi.Dtos;

public class RegistroServiceRepository
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(50, ErrorMessage = "O título deve ter no máximo 50 caracteres.")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; }

    [StringLength(50, ErrorMessage = "As anotações devem ter no máximo 50 caracteres.")]
    public string Anotacoes { get; set; }

    public DateTime? DataCadastro { get; set; } = DateTime.Now;
}

