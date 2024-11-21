namespace Proyecto.Models
{
    public class CuentaModel
    {
        public int Id_Cuenta { get; set; }

        public int Id_Usuario { get; set; }

        public string NumeroCuenta { get; set; }

        public float Saldo { get; set; }

        public string TipoCuenta { get; set; }

        public DateTime FechaApertura { get; set; }

    }
}
