using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Domain
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        void Save(T item);
        bool Delete(int id);
        bool Delete(T item);
    }

    public abstract class RepositoryBase<T> : IRepository<T> where T : CBaseEntity
    {
        protected MyDbContext DB { get; private set; }
        protected DbSet<T> Collection { get; private set; }

        public RepositoryBase(MyDbContext db, DbSet<T> collection)
        {
            this.DB = db;
            this.Collection = collection;
        }

        public virtual T GetById(int id)
        {
            T res = GetAll().SingleOrDefault(x => x.Id == id);
            return res;
        }

        public virtual IQueryable<T> GetAll()
        {
            var res = Collection.AsQueryable<T>();
            return res;
        }

        public virtual void Save(T item)
        {
            if (item.Id == 0)
                Collection.Add(item);
            else
                DB.Entry<T>(item).State = EntityState.Modified;
        }

        public virtual bool Delete(T item)
        {
            Collection.Remove(item);
            return true;
        }

        public virtual bool Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
                return Delete(item);
            return false;
        }
    }
}