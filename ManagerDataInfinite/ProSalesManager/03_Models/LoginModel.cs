namespace ProSalesManager._03_Models
{
    public class LoginModel
    {
        //[Required]
        //[MaxLength(50)]
        public string? Correo { get; set; }
        //[Required]
        //[MaxLength(50)]
        public string? Password { get; set; }
    }
}
