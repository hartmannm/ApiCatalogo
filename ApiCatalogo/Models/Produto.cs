using ApiCatalogo.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalogo.Models
{
    [Table("Produtos")]
    public class Produto : IValidatableObject
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [MaxLength(80)]
        [PrimeiraLetraMaiuscula]
        public string Nome { get; set; }

        [Required]
        [MaxLength(300)]
        public string Descricao { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Descricao))
            {
                var primeiraLetra = this.Descricao[0].ToString();
                if (primeiraLetra != primeiraLetra.ToUpper())
                {
                    yield return new ValidationResult("A primeira letra da descrição deve ser maiúscula", new[] { nameof(Descricao) });
                }
            }
            if (Estoque <= 0)
            {
                yield return new ValidationResult("O estoque deve ser maior que zero", new[] { nameof(Estoque) });
            }
        }
    }
}
