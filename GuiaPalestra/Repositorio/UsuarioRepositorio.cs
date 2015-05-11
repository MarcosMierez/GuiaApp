using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using Dapper;
using GuiaPalestra.Models;
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
                "insert into usuario (Id,NomeUsuario,SenhaUsuario,EmailUsuario,Permissao,Foto,Sexo) values (@Id,@name,@password,@email,@claim,'GenericPhoto.png',@sx)",
                new {ID=entidade.ID, name = entidade.Nome.ToLower(), password = entidade.Senha.ToLower(),email=entidade.Email.ToLower(), claim = "Usuario",sx=entidade.Sexo});
        }

        public void Update(Usuario entidade)
        {
            contexto.SqlBd.Query("update usuario set NomeUsuario= @name , EmailUsuario = @email,Foto = @photo where Id = @id",
                new {name = entidade.Nome.ToLower(), email = entidade.Email.ToLower(),photo=entidade.Foto,id=entidade.ID});
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public Usuario GetByID(string Id)
        {
            return contexto.SqlBd.Query<Usuario>("Select Id,NomeUsuario Nome,EmailUsuario Email,Foto from usuario where Id = @id", new { id = Id }).First();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return contexto.SqlBd.Query<Usuario>("select Id,NomeUsuario Nome,EmailUsuario Email from usuario").ToList();
        }

        public Usuario Logar(string email, string senha)
        {

            var tempUser = contexto.SqlBd.Query<dynamic>(
                    "Select Id,NomeUsuario,EmailUsuario,Permissao,Foto from usuario where SenhaUsuario= @Senha and EmailUsuario = @Email",
                    new { Email = email, Senha = senha }).FirstOrDefault();
            if (tempUser == null)
                return new Usuario();
            string permissoes = tempUser.Permissao;
            var tempUsusario = new Usuario
            {
                ID = tempUser.Id,
                Email = tempUser.EmailUsuario,
                Nome = tempUser.NomeUsuario,
                Foto = tempUser.Foto,
                Permissao = permissoes.Split(',').ToList()
            };
            return tempUsusario;
        }


    }
}