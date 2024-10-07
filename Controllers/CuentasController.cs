using AutoMapper;
using MicroServicioCM.DBContext;
using MicroServicioCM.Entidades.Context;
using MicroServicioCM.Entidades.Request;
using MicroServicioCM.Entidades.Response;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicioCM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CuentasController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private ResponseInformation _response;
        private readonly IMapper _mapper;

        public CuentasController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _response = new ResponseInformation();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseInformation GetCuenta()
        {
            try
            {
                IEnumerable<CuentaContext> lstCuenta = _appDbContext.Cuenta.ToList();
                _response.Data = lstCuenta;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        public ResponseInformation PostCuenta([FromBody] CuentaRequest reqCta)
        {
            try
            {
                var CtaContext = _mapper.Map<CuentaContext>(reqCta);
                _appDbContext.Cuenta.Add(CtaContext);
                _appDbContext.SaveChanges();
                _response.Data = _mapper.Map<CuentaResponse>(reqCta);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }
            _response.Message = _response.Success ? "Registro exitoso" : "Error al insertar registro. " + _response.Message;
            return _response;
        }

        [HttpPatch]
        public ResponseInformation PatchCuenta([FromBody] CuentaRequest reqCta)
        {
            try
            {
                _appDbContext.Cuenta.Update(_mapper.Map<CuentaContext>(reqCta));
                _appDbContext.SaveChanges();
                _response.Data = _mapper.Map<CuentaResponse>(reqCta);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseInformation PutCuenta([FromBody] CuentaRequest reqCta)
        {
            try
            {
                _appDbContext.Cuenta.Update(_mapper.Map<CuentaContext>(reqCta));
                _appDbContext.SaveChanges();
                _response.Data = _mapper.Map<CuentaResponse>(reqCta);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public ResponseInformation DeleteCuenta(int numeroCta)
        {
            try
            {
                CuentaContext cuenta = _appDbContext.Cuenta.FirstOrDefault(c => c.NumeroCuenta == numeroCta);
                _appDbContext.Cuenta.Remove(cuenta);
                _appDbContext.SaveChanges();
                _response.Data = cuenta;
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
