using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaPalestrasOnline.Models
{
    public class Usuario :Entidade
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public List<string> Permissao { get; set; }
        public string Senha { get; set; }
    }
}