using Eventos.IO.Domain.Core.Models;
using Eventos.IO.Domain.INTERFACES;
using Eventos.IO.Infra.Data.CONTEXT;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Eventos.IO.Infra.Data.REPOSITORY;
public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
{
    protected EventosContext Db;
    protected DbSet<TEntity> DbSet;

    protected Repository(EventosContext context)
    {
        Db = context;
        DbSet = Db.Set<TEntity>();
    }

    public virtual void Adicionar(TEntity obj)
    {
        DbSet.Add(obj);
    }

    public virtual void Atualizar(TEntity obj)
    {
        DbSet.Update(obj);
    }

    public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
    {
        return DbSet.AsNoTracking().Where(predicate);
    }

    public virtual void Dispose()
    {
        Db.Dispose();
    }

    public virtual TEntity ObterPorId(Guid id)
    {
        return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
    }

    public virtual IEnumerable<TEntity> ObterTodos()
    {
        return DbSet.ToList();
    }

    public virtual void Remover(Guid id)
    {
        DbSet.Remove(DbSet.Find(id));
    }

    public int SaveChanges()
    {
        return Db.SaveChanges();
    }
}