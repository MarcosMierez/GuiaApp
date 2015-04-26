using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using GuiaPalestrasOnline.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace GuiaPalestrasOnline.Helpers
{
    public static class Seguranca
    {
        public static void GerearSessaoDeUsuario(Usuario usuarioLogado) 
        {
            var claims = new List<Claim>
            {
                new Claim("ID", usuarioLogado.ID),
                new Claim(ClaimTypes.Name,usuarioLogado.Nome),
                new Claim(ClaimTypes.Email, usuarioLogado.Email),
            };
            claims.AddRange(usuarioLogado.Permissao.Select(permissao => new Claim(ClaimTypes.Role, permissao)));
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            HttpContext.Current.Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
        }

        public static void DestruirSessaoDeUsuario()
        {
            var ctx = HttpContext.Current.Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
        }

        public static Usuario Usuario()
        {

            var ctx = (OwinContext)HttpContext.Current.Request.GetOwinContext();
            var user = ctx.Authentication.User;
            if (user.FindFirst("ID") == null)
                return new Usuario();
           var usuario= new Usuario()
            {
                ID = user.FindFirst("ID").Value,
                Email = user.FindFirst(ClaimTypes.Email).Value,
                Nome = user.FindFirst(ClaimTypes.Name).Value,
               Permissao = new List<string>()
            };
            usuario.Permissao.Add(user.FindFirst(ClaimTypes.Role).Value);
            return usuario;
        }
    }
}