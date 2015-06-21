using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Dapper;
using GuiaPalestra.Models;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;
using Newtonsoft.Json.Serialization;

namespace GuiaPalestrasOnline.Aplicacao
{
    public class CoordenadorApp
    {
        private readonly ICrud<Coordenador> _repositorio;
        private readonly Contexto _contexto;

        public CoordenadorApp(ICrud<Coordenador> repo)
        {
            _contexto = new Contexto();
            _repositorio = repo;
        }

        public bool LoginCoordenador(string emailCoordenador, string senhaCoordenador)
        {
            if (!string.IsNullOrEmpty(emailCoordenador) && !string.IsNullOrEmpty(senhaCoordenador))
            {
                var coordenador = _contexto.SqlBd.Query<dynamic>(
                    "select Id,Nome,Email,Permissao,Foto from coordenador where email= @email and senha = @senha",
                    new { email = emailCoordenador, senha = senhaCoordenador }).FirstOrDefault();
                if (coordenador != null)
                {
                    string permissao = coordenador.Permissao;
                    var tempUsuario = new Usuario()
                    {
                        Email = coordenador.Email,
                        ID = coordenador.Id,
                        Nome = coordenador.Nome,
                        Foto = coordenador.Foto,
                        Permissao = permissao.Split(',').ToList()
                    };
                    Seguranca.GerearSessaoDeUsuario(tempUsuario);
                    return true;
                }
            }
            return false;
        }
        
        public IEnumerable<Coordenador> GetAll()
        {
           return _repositorio.GetAll();
        }

        public void Save(Coordenador entidade)
        {
           _repositorio.Save(entidade);
        }

        public Coordenador GetById(string id)
        {
            return _repositorio.GetByID(id);
        }

        public void Update(Coordenador entidade)
        {
            _repositorio.Update(entidade);
        }
    }
}