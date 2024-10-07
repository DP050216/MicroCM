using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MicroServicioCM.Entidades.Context
{
    public class CuentaContext
    {
        [Key]
        public required int NumeroCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public double? SaldoInicial { get; set; }
        public string? Estado { get; set; }
        public required string? Cliente { get; set; }
    }
}
