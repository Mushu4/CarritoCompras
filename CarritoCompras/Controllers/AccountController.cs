using CarritoCompras.Data;
using CarritoCompras.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarritoCompras.Controllers
{
    public class AccountController : OrigenController
    {
        public AccountController(ApplicationDbContext context) : base(context)
        { }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            try
            {
                if (usuario!= null)
                {
                    if (await _context.Usuarios.AnyAsync(u=>u.NombreUsuario==usuario.NombreUsuario))
                    {
                        ModelState.AddModelError(nameof(usuario.NombreUsuario), "El Nombre de Usuario ya existe.");
                        return View(usuario);
                    }

                    var ClienteRol = await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == "Cliente");

                    if (ClienteRol!=null)
                    {
                        usuario.RolId = ClienteRol.RolId;
                    }
                    _context.Usuarios.Add(usuario);
                    await _context.SaveChangesAsync();

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, usuario.NombreUsuario));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Cliente"));

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return RedirectToAction("Index", "Home");
                }
                return View(usuario);
            }
            catch(Exception e)
            {
                return HandleError(e);
            }
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == username && u.Contrasena == password);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.NameIdentifier, user.UsuarioId.ToString())
                    };

                    var rol = await _context.Roles.FirstOrDefaultAsync(r => r.RolId == user.RolId);

                    if (rol != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, rol.Nombre));
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    if (rol != null)
                    {
                        if (rol.Nombre=="Administrador")
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    
                }

                ModelState.AddModelError("", "Los datos suministrados no son válidos.");
                return View();
            }
            catch (Exception e)
            {
                return HandleError(e);
            }
        }

        public async Task<IActionResult>Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
