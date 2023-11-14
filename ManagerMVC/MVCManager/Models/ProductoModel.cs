using System;
using Newtonsoft.Json;

public class ProductoModel
{
    [JsonProperty("idProducto")]
    public int IdProducto { get; set; }

    [JsonProperty("producto")]
    public string Producto { get; set; }

    [JsonProperty("marca")]
    public string Marca { get; set; }

    [JsonProperty("precio")]
    public decimal Precio { get; set; }

    [JsonProperty("cantidad")]
    public int Cantidad { get; set; }

    [JsonProperty("idProveedor")]
    public int IdProveedor { get; set; }

    [JsonProperty("fechaIngreso")]
    public DateTime FechaIngreso { get; set; }

    [JsonProperty("idEmpresa")]
    public int IdEmpresa { get; set; }

    [JsonProperty("imagenCarpeta")]
    public string ImagenCarpeta { get; set; }

    [JsonProperty("imagenNombre")]
    public string ImagenNombre { get; set; }

    [JsonProperty("horaCreacion")]
    public DateTime HoraCreacion { get; set; }

    [JsonProperty("horaActualizacion")]
    public DateTime HoraActualizacion { get; set; }

    [JsonProperty("activo")]
    public bool Activo { get; set; }

    [JsonProperty("idGenero")]
    public int IdGenero { get; set; }

    [JsonProperty("idModeloProducto")]
    public int idModeloProducto { get; set; }

    [JsonProperty("idCategoria")]
    public int IdCategoria { get; set; }

    [JsonProperty("idCalidad")]
    public int IdCalidad { get; set; }

    [JsonProperty("costo")]
    public decimal costo { get; set; }
}
