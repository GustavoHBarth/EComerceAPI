using Microsoft.AspNetCore.Identity;

namespace EComerceAPI.Models
{
    public class User : IdentityUser
    {
        // Propriedades adicionais (se necessário)
        public string? NomeCompleto { get; set; }
    }
}