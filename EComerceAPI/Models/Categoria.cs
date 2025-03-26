using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceAPI.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }


        [JsonIgnore] // Evita erro de referência cíclica ao serializar
        public ICollection<Produto>? Produtos { get; set; } // 🔹 Permite null
    }
}
