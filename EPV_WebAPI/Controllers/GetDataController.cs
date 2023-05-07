using EPV_WebAPI.Clases;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EPV_WebAPI.Controllers
{
    public class GetDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("GetClientes")]
        public async Task<ActionResult> GetClientes(string token)
        {
            try
            {
                Clases.Log.LogWrite($"GetClientes: token={token}");

                // Validar token
                Clases.Login log = new Clases.Login();
                if (!log.ValidarTokenUsuario(token)) 
                    return BadRequest("Token caducado o incorrecto");

                //Ejecutar la acción
                string jsonResultado = await Clases.AccesoDatos.JsonDataReader("SELECT * FROM dbo.Clientes");
                
                return Content(jsonResultado, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("obtenerCliente_sp")]
        public async Task<ActionResult> getClientes(string token)
        {
            try
            {
                Clases.Log.LogWrite($"GetClientes Stored Procedure: token={token}");

                // Validar token
                Clases.Login log = new Clases.Login();
                if (!log.ValidarTokenUsuario(token))
                    return BadRequest("Token caducado o incorrecto");

                //Ejecutar la acción
                List<SqlParameter> SqlParams = new List<SqlParameter>();

                var jsonResultado = await Clases.AccesoDatos.JsonStoredProcedure("dbo.spObtenerClientes", SqlParams.ToArray());

                return Content(jsonResultado, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
