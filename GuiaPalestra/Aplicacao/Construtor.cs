using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuiaPalestra.Aplicacao;
using GuiaPalestra.Repositorio;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;

namespace GuiaPalestrasOnline.Aplicacao
{
    public class Construtor
    {
        public static PalestranteApp PalestranteApp()
        {
            return new PalestranteApp(new PalestranteRepositorio());
        }
        public static TrilhaApp TrilhaApp()
        {
            return new TrilhaApp(new TrilhaRepositorio());
        }

        public static SalaApp SalaApp()
        {
            return new SalaApp(new SalaRepositorio());
        }

        public static PalestraApp PalestraApp()
        {
            return new PalestraApp(new PalestraRepositorio());
        }

        public static UsuarioApp UsuarioApp()
        {
            return new UsuarioApp(new UsuarioRepositorio());
        }

        public static CoordenadorApp CoordenadorApp()
        {
            return new CoordenadorApp(new CoordenadorRepositorio());
        }
        public static EventoApp EventoApp()
        {
            return new EventoApp(new EventoRepositorio());
        }
    }
}