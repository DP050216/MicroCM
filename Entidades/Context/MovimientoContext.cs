using System.ComponentModel.DataAnnotations;

namespace MicroServicioCM.Entidades.Context
{
    public class MovimientoContext
    {
        [Key]
        public required int IdMovimiento { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string? Movimiento { get; set; }
        public double Valor { get; set; }
        public char TipoMovimiento { get; set; }
        public int NumeroCuenta { get; set; }

    }
}
