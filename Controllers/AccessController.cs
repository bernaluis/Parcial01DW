using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parcial01BM101219.Models;

namespace Parcial01BM101219.Controllers
{
    public class AccessController : Controller
    {
        // GET: AccessController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string email,string pw)
        {
            using (Bm101219Context db=new Bm101219Context())
            {
                var lst= from d in db.Usuarios
                         where d.Email==email
                         select d;
                if (lst.Count() > 0)
                {
                    Usuario u = lst.First();
                    if (BCrypt.Net.BCrypt.Verify(pw, u.Password))
                    {
                        HttpContext.Session.SetString("user", u.Email);
                        return View("~/Views/Home/Index.cshtml");
                    }
                    else {
                        return Content("Credenciales invalidas");
                    }

                    
                }
                else
                {
                    return Content("Usuario no registrado");
                }

            }
                
        }

      
    }
}
