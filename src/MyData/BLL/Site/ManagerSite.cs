using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyData.BLL.Site
{
    public class ManagerSite : ISaveDataBase, IDisposable
    {
        private ProjectManager _projects = null;
        public ProjectManager Projects { get { return Get<ProjectManager>(_projects, this, _uow.Projects); } }

        private TaskManager _tasks = null;
        public TaskManager Tasks { get { return Get<TaskManager>(_tasks, this, _uow.Tasks); } }

        private IterationManager _iterations = null;
        public IterationManager Iterations { get { return Get<IterationManager>(_iterations, this, _uow.Iterarions); } }

        private MemoryManager _memory = null;
        public MemoryManager Memory { get { return Get<MemoryManager>(_memory, this, _uow.Folders, _uow.Sets, _uow.Questions); } }

        private MapsiteMaster _mapsite = null;
        public MapsiteMaster MapSite { get { return Get<MapsiteMaster>(_mapsite, this); } }

        private ImportExportTermsManager _importExportTerms = null;
        public ImportExportTermsManager ImportExportTerms { get { return Get<ImportExportTermsManager>(_importExportTerms, this); } }

        private LernManager _lern = null;
        public LernManager Lern { get { return Get<LernManager>(_lern, this); } }

        private UnitOfWork _uow;

        public void SaveDataBase()
        {
            _uow.SaveDataBase();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _uow.Dispose();
                }
                this._disposed = true;
            }
        }

        public ManagerSite()
        {
            _uow = new UnitOfWork();
            _disposed = false;
        }

        private T Get<T>(T obj, params object[] args)
        {
            if (obj == null)
                obj = (T)Activator.CreateInstance(typeof(T), args);
            return obj;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.Collect();
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}