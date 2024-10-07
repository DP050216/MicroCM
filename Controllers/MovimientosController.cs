using AutoMapper;
using MicroServicioCM.DBContext;
using MicroServicioCM.Entidades.Context;
using MicroServicioCM.Entidades.Request;
using MicroServicioCM.Entidades.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MicroServicioCM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovimientosController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private ResponseInformation _response;
        public MovimientosController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _response = new ResponseInformation();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseInformation GetMovimiento()
        {
            try
            {
                IEnumerable<MovimientoContext> lstMovimientos = _appDbContext.Movimiento.ToList();
                _response.Data = lstMovimientos;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("Fecha")]
        public ResponseInformation GetMovimiento(DateTime dtFechaInicio, DateTime dtFechaFin)
        {
            try
            {
                IEnumerable<MovimientoContext> lstMovimientos = _appDbContext.Movimiento.ToList().Where(c => c.Fecha >= dtFechaInicio && c.Fecha <= dtFechaFin);
                _response.Data = lstMovimientos;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("Cliente")]
        public ResponseInformation GetMovimiento(string Cliente)
        {
            try
            {
                var lstCuenta = _appDbContext.Cuenta.ToList().Where(c => c.Cliente == Cliente);
                var numCuenta = lstCuenta.Select(c => c.NumeroCuenta).ToList();
                IEnumerable<MovimientoContext> lstMovimientos = _appDbContext.Movimiento.Where(m => numCuenta.Contains(m.NumeroCuenta)).ToList();
                List<MovimientoClienteResponse> mcResponse = new List<MovimientoClienteResponse>();
                foreach (var a in lstMovimientos)
                    mcResponse.Add(_mapper.Map<MovimientoClienteResponse>(a));

                _response.Data = mcResponse;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }
            _response.Message = _response.Success ? "Consulta exitosa" : "Error al consultar registros. " + _response.Message;
            return _response;
        }

        [HttpPost]
        public ResponseInformation PostMovimiento([FromBody] MovimientoRequest reqMov)
        {
            try
            {
                MovimientoResponse resMov = _mapper.Map<MovimientoResponse>(reqMov);
                CuentaContext ctaContext = _appDbContext.Cuenta.FirstOrDefault(c => c.NumeroCuenta == reqMov.NumeroCuenta);
                resMov.SaldoInicial = (double)ctaContext.SaldoInicial;
                resMov.Cliente = ctaContext.Cliente;
                ctaContext.SaldoInicial = ctaContext.SaldoInicial - (-reqMov.Valor);

                if (ctaContext.SaldoInicial < 0)
                    throw new Exception("Saldo no disponible");

                resMov.SaldoDisponible = ctaContext.SaldoInicial;
                _appDbContext.Cuenta.Update(ctaContext);

                var MovContext = _mapper.Map<MovimientoContext>(reqMov);
                MovContext.NumeroCuenta = ctaContext.NumeroCuenta;
                MovContext.TipoMovimiento = reqMov.Valor < 0 ? 'R' : 'D';
                _appDbContext.Movimiento.Add(MovContext);
                _appDbContext.SaveChanges();
                _response.Message = _response.Success ? "Operacoón exitosa" : "Error al realizar la operación. " + _response.Message;
                _response.Data = resMov;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                if (string.IsNullOrEmpty(_response.Message))
                    _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPatch]
        public ResponseInformation PatchMovimiento([FromBody] MovimientoRequest reqMov)
        {
            try
            {
                _appDbContext.Movimiento.Update(_mapper.Map<MovimientoContext>(reqMov));
                _appDbContext.SaveChanges();
                _response.Data = _mapper.Map<MovimientoResponse>(reqMov);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseInformation PutMovimiento([FromBody] MovimientoRequest reqMov)
        {
            try
            {
                _appDbContext.Movimiento.Update(_mapper.Map<MovimientoContext>(reqMov));
                _appDbContext.SaveChanges();
                _response.Data = _mapper.Map<MovimientoResponse>(reqMov);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public ResponseInformation DeleteMovimiento(int id)
        {
            try
            {
                MovimientoContext movimiento = _appDbContext.Movimiento.FirstOrDefault(c => c.IdMovimiento == id);
                _appDbContext.Movimiento.Remove(movimiento);
                _appDbContext.SaveChanges();
                _response.Data = movimiento;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
