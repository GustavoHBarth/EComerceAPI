using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [Required]
        public int Estoque { get; set; }

        [ForeignKey("Categoria")]
        public int? CategoriaId { get; set; } // 🔹 Torne este campo opcional
        public Categoria? Categoria { get; set; } // 🔹 Torne este campo opcional
    }
}
