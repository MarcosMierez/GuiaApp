using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace GuiaPalestrasOnline.Models
{
    public class Trilha:Entidade
    {
        [Display(Name = "Trilha")]
        public string NomeTrilha { get; set; }

        public string CoordenadorId { get; set; }
        public string EventoId { get; set; }
    }
}