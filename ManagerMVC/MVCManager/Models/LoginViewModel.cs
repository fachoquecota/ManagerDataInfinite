using System.ComponentModel.DataAnnotations;

namespace MVCManager.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo Usuario es requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
