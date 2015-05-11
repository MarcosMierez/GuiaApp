using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaPalestra.ViewModel
{
    public class UsuariosConfirmadosVM
    {
        public string NomeUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string Tema { get; set; }
        public string Titulo { get; set; }
        public DateTime HorarioInicial { get; set; }
        public DateTime HorarioFinal { get; set; }
        public string Foto { get; set; }
        public string Sala { get; set; }
        public string Descricao { get; set; }
    }
}