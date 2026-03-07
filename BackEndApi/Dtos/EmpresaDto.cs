namespace BackEndApi.Dtos
{
    public class EmpresaDto
    {
        public int Id { get; set; }
        // =============================
        // IDENTIFICAÇÃO
        // =============================
        public string NomeFantasia { get; set; } = default!;
        public string? RazaoSocial { get; set; }
        public string? Cnpj { get; set; }
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }
    }
}
