using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestra.Models
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
        public string NomeCoordenador { get; set; }
        public string PalestraId { get; set; }
    }
}
