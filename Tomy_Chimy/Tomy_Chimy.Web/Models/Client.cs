using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tomy_Chimy.Web.Models
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID_User { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [MaxLength(60, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Correo electronico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Dirección del hogar")]
        public string Dirección { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [StringLength(10, ErrorMessage = "El {0} no es un Número válido")]
        [Display(Name = "Número de telefono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [StringLength(11, ErrorMessage = "El {0} no es un Número válido")]
        [Display(Name = "Cédula de identidad")]
        public string Cédula { get; set; }

        public bool Employee { get; set; }

        public string FullName => $"{Nombres} {Apellidos}";
    }
}
