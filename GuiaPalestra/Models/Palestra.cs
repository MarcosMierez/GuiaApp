﻿using System;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestra.Models
{
    public class Palestra:Entidade
    {
        public Palestra()
        {
            Sala=new Sala();
            Trilha=new Trilha();
            Palestrante=new Palestrante();
        }
        public string Titulo { get; set; }
        public DateTime Duracao { get; set; }
        public string PalestranteId { get; set; }
        public string TrilhaId { get; set; }
        public string SalaId { get; set; }
        public virtual Sala Sala { get; set; }
        public virtual Palestrante Palestrante { get; set; }
        public virtual Trilha Trilha { get; set; }
        public bool Status { get; set; }
    }
}