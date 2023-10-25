using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InduMovel.Models
{
    public class Movel
    {
        [Key]
        public int MovelId{get;set;}
        [Required(ErrorMessage = "Informe o Nome do Movel")]
        [Display(Name = "Nome do móvel")]
        [MinLength(5, ErrorMessage = "Nome do movel deve ter no mínimo {1} caracteres")]
        [MaxLength(30, ErrorMessage = "Nome do movel não pode exceder {1} caracteres")]
        public string Nome{get;set;}

        [Required(ErrorMessage = "Informe a cor")]
        [Display(Name = "Cor")]
        [MinLength(5, ErrorMessage = "Cor do movel deve ter no mínimo {1} caracteres")]
        public string Cor{get;set;}

        [Required(ErrorMessage = "Informe a Descrição")]
        [Display(Name = "Descrição")]
        [MinLength(20, ErrorMessage = "Descrição do movel deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição do movel não pode exceder {1} caracteres")]
        public string Descricao{get;set;}

        [Display(Name = "Imagem do móvel")]
        public string ImagemUrl{get;set;}

        [Display(Name = "Imagem menor do móvel")]
        public string ImagemCurta{get;set;}

        [Required(ErrorMessage = "Informe o valor do móvel")]
        [Display(Name = "Valor")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,999.99,ErrorMessage ="O valor deve estar entre 1 e 999,99")]
        public double Valor{get;set;}

        [Display(Name = "Em Produção")]
        public bool EmProducao{get;set;}

        [Display(Name = "Está em promoção")]
        public bool Promocao{get;set;}

        [Required(ErrorMessage = "Informe a categoria do móvel")]
        [Display(Name = "Categoria")]
        public int CategoriaId {get;set;}
        public virtual Categoria Categoria{get;set;}
    }
}