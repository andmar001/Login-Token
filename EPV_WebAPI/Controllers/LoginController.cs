using Microsoft.AspNetCore.Mvc;

namespace EPV_WebAPI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // generamos el token que usara el cliente
        [HttpPost("getTokenLogin")]
        public ActionResult GetTokenLogin(string email, string password)
        {
            Clases.Log.LogWrite($"GetTokenLogin: user={email}, password={password}");
            Clases.Login log = new Clases.Login();
            return Ok(log.getTokenLogin(email, password));
        }

        [HttpPost("loginByToken")]
        public ActionResult LoginByToken(string loginToken) 
        {
            Clases.Log.LogWrite($"LoginByToken: logintoken={loginToken}");
            Clases.Login log = new Clases.Login();
            string token = log.LoginByToken(loginToken);

            switch (token) 
            {
                case "-1": return BadRequest("Limite de tiempo excedido");
                case "-2": return BadRequest("Usuario o clave incorrectos");
                case "-3": return BadRequest("No se pudo hacer el login, revise los datos enviados");
                default: return Ok(token); // regresa token si el login fue correcto 
            }
        }

        //cambiar contraseña
        [HttpPost("setPassword")]
        public ActionResult SetPassword(string token, string encriptedOldPassword, string encriptedNewPassword)
        {
            Clases.Log.LogWrite($"SetPassword: token={token}, encriptedOldPassword={encriptedOldPassword}, encriptedNewPassword={encriptedNewPassword} ");
            Clases.Login log = new Clases.Login();
            bool resultado = log.SetPassword(token, encriptedOldPassword, encriptedNewPassword);
            if (resultado)
                return Ok(resultado);
            else
                return BadRequest(resultado);
        }
        //destruir token al cerrar la sesion
        [HttpPost("logout")]
        public ActionResult Logout(string token)
        {
            Clases.Log.LogWrite("Logout");
            return Ok("");
        }
    }
}
