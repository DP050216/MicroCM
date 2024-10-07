namespace MicroServicioCM.Entidades.Response
{
    public partial class MovimientoResponse
    {
        public string Fecha { get; set; } = DateTime.Now.Date.ToShortDateString();
        public string Cliente { get; set; }
        public required int NumeroCuenta { get; set; }
        public required string TipoCuenta { get; set; }
        public double SaldoInicial { get; set; }
        public string? Estado { get; set; }
        public string? Movimiento { get; set; }
        public double? SaldoDisponible { get; set; }

    }

    public partial class MovimientoClienteResponse
    {
        public string? Fecha { get; set; }
        public string? Cliente { get; set; }
        public int NumeroCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public double SaldoInicial { get; set; }
        public string? Estado { get; set; }
        public string? Movimiento { get; set; }
        public double? SaldoDisponible { get; set; }

    }
}
