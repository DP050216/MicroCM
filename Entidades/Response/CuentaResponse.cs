using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MicroServicioCM.Entidades.Response
{
    public class CuentaResponse
    {
        public required int NumeroCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public double? SaldoInicial { get; set; }
        public string? Estado { get; set; }
        public required string? Cliente { get; set; }
    }
}
