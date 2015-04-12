using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;

namespace GuiaPalestrasOnline.Aplicacao
{
    public class UsuarioApp
    {
        private readonly ICrud<Usuario> repositorio;
        private readonly IUser User=new UsuarioRepositorio();
        private readonly Contexto contexto;
        public UsuarioApp(ICrud<Usuario>repo)
        {          
            repositorio = repo;
        }

        public Usuario Logar(string email, string senha)
        {
          return  User.Logar(email, senha);
        }

        public void ParticiparPalestra(string usuarioId, string palestraId,string eventoId,string status)
        {
             User.ParticiparPalestra(usuarioId, palestraId,eventoId,status);
        }

        public void DesistirPalestra(string usuarioId, string palestraId)
        {
            User.DesistirPalestra(usuarioId,palestraId);
        }

        public void Save(Usuario entidade)
        {
            repositorio.Save(entidade);
        }

        public void Update(Usuario entidade)
        {
            repositorio.Update(entidade);
        }

        public Usuario GetById(string id)
        {
            return repositorio.GetByID(id);
        }

        public IEnumerable<Usuario> GetAll()
        {
          return  repositorio.GetAll();
        }
    }
}