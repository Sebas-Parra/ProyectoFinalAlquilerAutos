using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using CapaNegocio;
using CapaEntidad;
using System;

namespace AlquilerAutosProyecto.Controllers
{
    public class LoginController : Controller
    {
        private int id;
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string email_signIn, string password_signIn)
        {
            // Validar las credenciales del usuario
            Console.WriteLine(email_signIn, password_signIn);
            ClienteBL obj = new ClienteBL();
            Cliente usuario = await obj.recuperarEmailContrasenaAsync(email_signIn);

            if (usuario != null && usuario.password == password_signIn)
            {
                // Asignar el ID al campo de instancia
                id = usuario.id;
                string userRole = (email_signIn == "admin@admin") ? "Admin" : "Cliente";

                // Crear las "claims" del usuario, incluyendo el ID
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email_signIn),
                new Claim("UserId", usuario.id.ToString()), // Guardar el ID en las claims
                new Claim(ClaimTypes.Role, userRole)
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Establecer la cookie de autenticación
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                /*if(email_signIn == "admin@admin" && password_signIn == "admin")
                    {
                        return RedirectToAction("Admin", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }*/
                return RedirectToAction("Index", "Home");

            }
            else
            {
                // Si las credenciales son incorrectas, mostrar un error
                ModelState.AddModelError("", "Credenciales inválidas.");
                return View();
            }
        }

        // Cerrar sesión (logout)
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        public bool validateLogin()
        {
            Console.WriteLine(User.Identity.IsAuthenticated);
            if (User.Identity.IsAuthenticated == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //[HttpGet]
        //public JsonResult validateLogin()
        //{
        //    bool isAuthenticated = User.Identity.IsAuthenticated;
        //    return Json(new { isAuthenticated });
        //}


        public int obtenerCliente()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

        public Cliente RecuperarEmail(string email)
        {
            ClienteBL obj = new ClienteBL();

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("El correo electrónico no puede estar vacío.", nameof(email));
            }

            try
            {
                return obj.recuperarEmail(email);
            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o lanzar una excepción personalizada
                Console.WriteLine($"Error al recuperar contraseña: {ex.Message}");
                return null;
            }
        }

    }
}
