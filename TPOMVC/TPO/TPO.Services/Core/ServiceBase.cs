using System;
using System.Data.Entity.Validation;
using System.Text;
using TPO.Services.Application;
using WebMatrix.WebData;

namespace TPO.Services.Core
{
    public abstract class ServiceBase: IDisposable
    {
        protected IUnitOfWork _repository;

        protected ServiceBase()
        {
            _repository = new UnitOfWork();
        }

        protected ServiceBase(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public string CurrentUserName
        {
            get { return WebSecurity.CurrentUserName ; }
        }

        public void CommitUnitOfWork()
        {
            try
            {
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                HandleValidationException(valEx);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        private static IApplicationLogService _log = ApplicationLogService.GetInstance();

        protected void LogException(Exception ex)
        {
            if (_log.IsErrorEnabled) 
                _log.Error("Error in TPO.Web", ex, CurrentUserName, Environment.MachineName);
        }

        protected void HandleValidationException(DbEntityValidationException valEx)
        {
            var sb = new StringBuilder();

            foreach (var failure in valEx.EntityValidationErrors)
            {
                sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                foreach (var error in failure.ValidationErrors)
                {
                    sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                    sb.AppendLine();
                }
            }

            LogException(valEx);

            throw new DbEntityValidationException(
                "Entity Validation Failed - errors follow:\n" +
                sb.ToString(), valEx
                ); // Add the original exception as the innerException
        }


    }
}
