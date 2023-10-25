using System.ComponentModel.DataAnnotations;

namespace InduMovel.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaID{get; set;}

        [Required(ErrorMessage ="Informe o nome da categoria")]
        [Display(Name = "Nome da Categoria")]
        public string Nome{get;set;}

        public List<Movel> Moveis{get;set;}
    }
}