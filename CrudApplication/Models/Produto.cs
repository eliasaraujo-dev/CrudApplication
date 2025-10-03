using CrudApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudApplication.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        [Column(TypeName = "decimal(18,2)")] // Define o tipo da coluna no banco
        public decimal Valor { get; set; }

        public bool Disponivel { get; set; }

        // Chave Estrangeira
        [Required(ErrorMessage = "É obrigatório associar um cliente ao produto.")]
        public int IdCliente { get; set; }

        // Propriedade de navegação: este produto pertence a um cliente
        [ForeignKey("IdCliente")]
        public virtual Cliente? Cliente { get; set; }
    }
}