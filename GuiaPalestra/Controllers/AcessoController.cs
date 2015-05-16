using System.Web.Mvc;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;

namespace GuiaPalestra.Controllers
{
    public class AcessoController : Controller
    {
        // GET: Acesso
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string EmailUsuario, string SenhaUsuario)
        {

            if (!string.IsNullOrEmpty(EmailUsuario) && !string.IsNullOrEmpty(SenhaUsuario))
            {
                var usuario = Construtor.UsuarioApp().Logar(EmailUsuario, SenhaUsuario);
                if (!string.IsNullOrEmpty(usuario.Email))
                {
                    Seguranca.GerearSessaoDeUsuario(usuario);
                    this.Flash("Bem vindo " + usuario.Nome);
                    return RedirectToAction("Index", usuario.Permissao[0]);
                }
            }
            this.Flash("Login ou Senha Incorretos", LoggerEnum.Error);
            return RedirectToAction("Index", "Acesso");
        }
        public ActionResult LoginPalestrante()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPalestrante(string emailPalestrante, string senhaPalestrante)
        {
            if (!string.IsNullOrEmpty(emailPalestrante) && !string.IsNullOrEmpty(senhaPalestrante))
            {
              var usuario= Construtor.PalestranteApp().LogarPalestrante(emailPalestrante, senhaPalestrante);
                if (!string.IsNullOrEmpty(usuario.Email))
                {
                    Seguranca.GerearSessaoDeUsuario(usuario);
                    this.Flash("Bem vindo " + usuario.Nome);
                    return RedirectToAction("Index", "PalestrantePainel");
                }    
            }
            this.Flash("Login ou Senha Incorretos", LoggerEnum.Error);
            return RedirectToAction("LoginPalestrante", "Acesso");
        }
        public ActionResult LoginCoordenador()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginCoordenador(string emailCoordenador, string senhaCoordenador)
        {
            if (Construtor.CoordenadorApp().LoginCoordenador(emailCoordenador, senhaCoordenador)==true)
            {
                this.Flash("Bem vindo " );
                return RedirectToAction("Index", "Coordenador");
            }
            this.Flash("Login ou Senha Incorretos", LoggerEnum.Error);
            return RedirectToAction("LoginCoordenador","Acesso");
        }

        public ActionResult Sair()
        {
            Seguranca.DestruirSessaoDeUsuario();
            return RedirectToAction("Index");
        }
    }
}