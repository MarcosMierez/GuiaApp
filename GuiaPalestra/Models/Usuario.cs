using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestra.Models
{
    public class Usuario : Entidade
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Nome { get; set; }
        public List<string> Permissao { get; set; }
        [Required]
        public string Senha { get; set; }
        public string Foto { get; set; }
        [Required]
        public char Sexo { get; set; }
    }
}