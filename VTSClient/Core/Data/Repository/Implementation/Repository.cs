using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CrossCutting;
using Core.Data.Helpers;
using Core.Exceptions;
using SQLite;

namespace Core.Data.Repository.Implementation
{
    public class Repository<T> where T : class, new()
    {
        protected static SQLiteAsyncConnection Connection;
        protected readonly IFileHelper _fileHelper;

        public Repository(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
            SqLiteAsyncConnection();
        }

        private void SqLiteAsyncConnection()
        {
            Connection = new SQLiteAsyncConnection(_fileHelper.GetDBPath(Configuration.DatabaseName));
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await Connection.Table<T>().ToListAsync();
            }
            catch (SQLiteException ex)
            {
                throw new DataException(ex.Message, ex);
            }
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                await Connection.InsertAsync(entity);
                return entity;
            }
            catch (SQLiteException ex)
            {
                throw new DataException(ex.Message, ex);
            }
        }

        public async Task<bool> Delete(T vacation)
        {
            try
            {
                await Connection.DeleteAsync(vacation);
                return true;
            }
            catch (SQLiteException ex)
            {
                throw new DataException(ex.Message, ex);
            }
        }

        public async Task<bool> Edit(T entity)
        {
            try
            {
                await Connection.UpdateAsync(entity);
                return true;
            }
            catch (SQLiteException ex)
            {
                throw new DataException(ex.Message, ex);
            }
        }
    }
}