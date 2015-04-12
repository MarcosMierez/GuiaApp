using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GuiaPalestrasOnline.Models
{
    public class Evento : Entidade
    {
        public List<Trilha> ListadeTrilhas { get; set; }
        [Required]
        public string Local { get; set; }
        public string Tema { get; set; }
        public DateTime DiaInicial { get; set; }
        public DateTime DiaFinal { get; set; }
        public string CoordenadorId { get; set; }
    }
}
