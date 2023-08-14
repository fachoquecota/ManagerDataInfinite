using System.ComponentModel.DataAnnotations;

namespace ProSalesManager._03_Models.ReservedModels
{
    public class ErrorResult
    {
        [Required]
        public static string? ErrorMessage { get; set; }
    }
}
