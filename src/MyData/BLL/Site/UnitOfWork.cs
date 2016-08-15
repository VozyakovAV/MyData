using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyData.BLL.Site
{
    public interface ISaveDataBase
    {
        void SaveDataBase();
    }

    public class UnitOfWork : ISaveDataBase, IDisposable
    {
        private ProjectRepository _projects = null;
        public ProjectRepository Projects { get { return Get<ProjectRepository>(_projects, _db, _db.Projects); } }

        private TaskRepository _tasks = null;
        public TaskRepository Tasks { get { return Get<TaskRepository>(_tasks, _db, _db.Tasks); } }

        private IterationRepository _iterations = null;
        public IterationRepository Iterarions { get { return Get<IterationRepository>(_iterations, _db, _db.Iterations); } }

        private SetRepository _sets = null;
        public SetRepository Sets { get { return Get<SetRepository>(_sets, _db, _db.Sets); } }

        private TermRepository _questions = null;
        public TermRepository Questions { get { return Get<TermRepository>(_questions, _db, _db.Questions); } }

        private FolderRepository _folders = null;
        public FolderRepository Folders { get { return Get<FolderRepository>(_folders, _db, _db.Folders); } }

        private MyDbContext _db;

        public UnitOfWork(MyDbContext db = null)
        {
            if (db == null)
                this._db = new MyDbContext();
            _disposed = false;
            //SERIALIZE WILL FAIL WITH PROXIED ENTITIES
            //_db.Configuration.ProxyCreationEnabled = false;
            //ENABLING COULD CAUSE ENDLESS LOOPS AND PERFORMANCE PROBLEMS
            //_db.Configuration.LazyLoadingEnabled = false;
        }

        public void SaveDataBase()
        {
            _db.SaveChanges();
        }

        private T Get<T>(T obj, params object[] args)
        {
            if (obj == null)
                obj = (T)Activator.CreateInstance(typeof(T), args);
            return obj;
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_db != null) _db.Dispose();
                }
                _db = null;
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}