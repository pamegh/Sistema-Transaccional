namespace Proyecto.Models
{
    public class UsuarioModel
    {
        public int Id_Usuario { get; set; }

        public string Nombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string Email { get; set; }

        public string Contrasena { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string TipoUsuario { get; set; }

        public string Direccion { get; set; }

 
    }
}
