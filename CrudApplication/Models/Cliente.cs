using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudApplication.Models
{
    [Table("Clientes")] // Define o nome da tabela no banco de dados
    public class Cliente
    {
        [Key] // Marca como chave primária
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")] // Validação
        [StringLength(50)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
        [StringLength(100)]
        public string? Sobrenome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [StringLength(150)]
        public string? Email { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        // Propriedade de navegação: um cliente pode ter vários produtos
        public virtual ICollection<Produto>? Produtos { get; set; }
    }
}