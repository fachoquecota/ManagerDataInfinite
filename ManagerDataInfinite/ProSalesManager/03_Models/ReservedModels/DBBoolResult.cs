using System.ComponentModel.DataAnnotations;

namespace ProSalesManager._03_Models.ReservedModels
{
    public class DBBoolResult
    {
        [Required]
        public int result { get; set; }
        public string value { get; set; }
    }
}
