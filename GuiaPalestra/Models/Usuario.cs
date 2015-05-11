using System.Collections.Generic;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestra.Models
{
    public class Usuario :Entidade
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public List<string> Permissao { get; set; }
        public string Senha { get; set; }
        public string Foto { get; set; }
        public char Sexo { get; set; }
    }
}