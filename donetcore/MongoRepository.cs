using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace donetcore
{
    /// <summary>
    /// 封装向业务层提供操作MongoDB的操作方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoRepository<T> : IMongoRepository<T> where T : class
    {
        /// <summary>
        /// 从指定的库与表中获取指定条件的数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, string dbName, string tableName)
        {
            var dbContext = new MongoDbContext(dbName);
            var collection = await dbContext.GetCollectionAsync<T>(tableName);
            return collection.AsQueryable<T>().Where(predicate).ToList();
        }


        /// <summary>
        /// 对指定的库与表中新增数据
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Add(List<T> list, string dbName, string tableName = "")
        {
            var dbContext = new MongoDbContext(dbName);
            var collection = await dbContext.GetCollectionAsync<T>(tableName);
            await collection.InsertManyAsync(list);
            return true;
        }
    }
}
