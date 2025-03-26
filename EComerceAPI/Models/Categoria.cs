using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}
