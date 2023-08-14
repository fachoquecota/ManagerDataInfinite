namespace ProSalesManager._03_Models
{
    public class LoginModel
    {
        //[Required]
        //[MaxLength(50)]
        public string? email { get; set; }
        //[Required]
        //[MaxLength(50)]
        public string? Password { get; set; }
    }
}
