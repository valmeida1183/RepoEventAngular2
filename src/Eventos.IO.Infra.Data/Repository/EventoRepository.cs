using Dapper;
using Eventos.IO.Domain.Eventos;
using Eventos.IO.Domain.Eventos.Repository;
using Eventos.IO.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eventos.IO.Infra.Data.Repository
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(EventosContext context) : base(context)
        {

        }

        public override IEnumerable<Evento> ObterTodos()
        {
            //Usando o Dapper para retornar a listagem dos eventos
            var sql = "SELECT * FROM EVENTOS E " +
                      "WHERE E.EXCLUIDO = 0 " +
                      "ORDER BY E.DATAFIM DESC ";

            return Db.Database.GetDbConnection().Query<Evento>(sql);
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            Db.Enderecos.Add(endereco);
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            Db.Enderecos.Update(endereco);
        }

        public IEnumerable<Categoria> ObterCategorias()
        {
            //"@" é para ignorar os caracteres de escape. "C:\\Users\\Rich" is the same as @"C:\Users\Rich"
            var sql = @"SELECT * FROM Categorias";
            return Db.Database.GetDbConnection().Query<Categoria>(sql);
        }

        public Endereco ObterEnderecoPorId(Guid id)
        {
            //EF
            //return Db.Enderecos.Find(id);

            var sql = @"SELECT * FROM ENDERECOS E " +
                       "WHERE E.Id = @uid";

            var endereco = Db.Database.GetDbConnection().Query<Endereco>(sql, new { uid = id });
            return endereco.SingleOrDefault();
        }

        public IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId)
        {
            //EF 
            //return Db.Eventos.Where(e => e.OrganizadorId == organizadorId);

            //Dapper
            var sql = @"SELECT * FROM EVENTOS E " +
                       "WHERE E.EXCLUIDO = 0 " +
                       "AND E.ORGANIZADORID = @oid " +
                       "ORDER BY E.DATAFIM DESC";

            return Db.Database.GetDbConnection().Query<Evento>(sql, new { oid = organizadorId });
        }        
        public override Evento ObterPorId(Guid id)
        {   //EF
            // Traz o Evento juntamente com o Endereço (INNER JOIN)
            //return Db.Eventos.Include(e => e.Endereco).FirstOrDefault(e => e.Id == id);

            //Dapper
            // @ antes de uma string significa que ela deve ser interpretada literalmente, desconsiderando caracteres de escape.
            var sql = @"SELECT * FROM Eventos E " +
                       "LEFT JOIN Enderecos EN " +
                       "ON E.Id = EN.EventoId " +
                       "WHERE E.Id = @uid";

            var evento = Db.Database.GetDbConnection().Query<Evento, Endereco, Evento>(sql,
                (e, en) =>
                {
                    if (en != null)
                    {
                        e.AtribuirEndereco(en);
                    }

                    return e;
                }, new { uid = id });

            return evento.FirstOrDefault();
        }

        public override void Remover(Guid id)
        {
            var evento = ObterPorId(id);
            evento.ExcluirEvento();
            Atualizar(evento);
        }        
    }
}
