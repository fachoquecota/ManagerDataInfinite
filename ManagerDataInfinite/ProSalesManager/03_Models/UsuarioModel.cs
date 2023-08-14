namespace ProSalesManager._03_Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public int IdRol { get; set; }
        public DateTime? LastSession { get; set; }
        public bool? Activacion { get; set; }
        public DateTime HoraCreacion { get; set; }
        public DateTime HoraActualizacion { get; set; }
        public int IdEmpresa { get; set; }
    }
}
