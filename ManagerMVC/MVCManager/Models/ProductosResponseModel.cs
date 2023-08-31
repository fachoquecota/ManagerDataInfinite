using Newtonsoft.Json;

namespace MVCManager.Models
{
    public class ProductosResponseModel
    {
        [JsonProperty("products")]
        public List<ProductoModel> Products { get; set; }
    }
}
