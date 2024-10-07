using System.ComponentModel.DataAnnotations;

namespace MicroServicioCM.Entidades.Request
{
    public class MovimientoRequest
    {
        public  int IdMovimiento { get; set; }
        public  int NumeroCuenta { get; set; }
        public  string TipoCuenta { get; set; }
        public  string TipoMovimiento { get; set; }
        public  double Valor { get; set; }
        public string? Estado { get; set; }
        public string? Movimiento { get; set; }
    }
}
