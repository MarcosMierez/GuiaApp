using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using Dapper;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Repositorio
{
    public class UsuarioRepositorio : ICrud<Usuario>,IUser
    {
        private readonly Contexto contexto;

        public UsuarioRepositorio()
        {
            contexto = new Contexto();
        }
        public void Save(Usuario entidade)
        {
            
            contexto.SqlBd.Query(
                "insert into usuario (Id,NomeUsuario,SenhaUsuario,EmailUsuario,Permissao) values (@Id,@name,@email,@password,@claim)",
                new {ID=entidade.ID, name = entidade.Nome.ToLower(), email = entidade.Email.ToLower(), password = entidade.Senha.ToLower(), claim = "Usuario"});
        }

        public void Update(Usuario entidade)
        {
            contexto.SqlBd.Query("update usuario set NomeUsuario= @name , EmailUsuario = @email where Id = @id",
                new {name = entidade.Nome.ToLower(), email = entidade.Email.ToLower(),id=entidade.ID});
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public Usuario GetByID(string Id)
        {
            return contexto.SqlBd.Query<Usuario>("Select Id,NomeUsuario Nome,EmailUsuario Email from usuario where Id = @id", new { id = Id }).First();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return contexto.SqlBd.Query<Usuario>("select Id,NomeUsuario Nome,EmailUsuario Email from usuario").ToList();
        }

        public Usuario Logar(string email, string senha)
        {

            var tempUser = contexto.SqlBd.Query<dynamic>(
                    "Select Id,NomeUsuario,EmailUsuario,Permissao from usuario where SenhaUsuario= @Senha and EmailUsuario = @Email",
                    new { Email = email, Senha = senha }).FirstOrDefault();
            if (tempUser == null)
                return new Usuario();
            string permissoes = tempUser.Permissao;
            var tempUsusario = new Usuario
            {
                ID = tempUser.Id,
                Email = tempUser.EmailUsuario,
                Nome = tempUser.NomeUsuario,
                Permissao = permissoes.Split(',').ToList()
            };
            return tempUsusario;
        }

        public void ParticiparPalestra(string palestraId, string usuarioId, string eventoId, string status)
        {
            contexto.SqlBd.Query("insert into palestrasusuario(PalestraId,UsuarioId,EventoId,Status) values(@pId,@uId,@eId,@st)",
                new {pId = palestraId,uId=usuarioId,eId=eventoId,st=status });
        }

        public void DesistirPalestra(string usuarioId, string palestraId)
        {
            contexto.SqlBd.Query("delete from palestrasusuario where palestrasusuario.UsuarioId = @uId and palestrasusuario.PalestraId = @pId",
                new { uId = usuarioId, pId = palestraId });
        }


    }
}