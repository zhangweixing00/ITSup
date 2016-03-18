using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoStart
{
    public class MongoDBHelper
    {
        #region 静态构造加载MongoClientSettings
        static MongoClientSettings setting = null;
        static List<MongoServerAddress> servicesList;
        static MongoDBHelper()
        {
            string connectionString = ConfigurationManager.AppSettings["mongodbServerList"];
             servicesList = new List<MongoServerAddress>();

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                var ips = connectionString.Split(';');

                foreach (var ip in ips)
                {
                    var host = ip.Split(':')[0];
                    var port = Convert.ToInt32(ip.Split(':')[1]);
                    servicesList.Add(new MongoServerAddress(host, port));
                }
            }

            setting = new MongoClientSettings();
            setting.ReplicaSetName = "datamip";

            //集群中的服务器列表
            setting.Servers = servicesList;
        }

        public static void AddConnection(string connectionString)
        {
            var host = connectionString.Split(':')[0];
            var port = Convert.ToInt32(connectionString.Split(':')[1]);
            servicesList.Add(new MongoServerAddress(host, port));
            setting.Servers = servicesList;
        }

        #endregion


        MongoServer server;
        MongoDatabase database;

        private string databaseName;

        public MongoDBHelper(string databaseName)
        {
            server = new MongoClient(setting).GetServer();
            this.databaseName = databaseName;
            database = server.GetDatabase(databaseName);
        }
        ~MongoDBHelper()
        {
            server.Shutdown();
        }

        private string GetTableName<T>()
        {
            Object[] objs = typeof(T).GetCustomAttributes(typeof(MongoInfoMapAttribute), true);
            if (objs != null && objs.Length > 0)
            {
                MongoInfoMapAttribute attribute = objs[0] as MongoInfoMapAttribute;
                return attribute.TableName;
            }
            else
            {
                throw new ArgumentException("请使用MongoInfoMapAttribute标记实体属性");
            }
        }
        private MongoCollection<T> GetCollection<T>()
        {
            return database.GetCollection<T>(GetTableName<T>());
        }

        #region 删除

        public bool Remove<T>(Expression<Func<T, bool>> func)
        {
            try
            {
                var collection = GetCollection<T>();

                var query = Query<T>.Where(func);

                var result = collection.Remove(query);

                return result.Response["ok"].AsInt32 > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveAll<T>()
        {
            try
            {
                var collection = GetCollection<T>();

                var result = collection.RemoveAll();

                return result.Response["ok"].AsInt32 > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region 插入及批量插入
        /// <summary>
        /// 单条覆盖，如果不存在插入，如果存在覆盖
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public bool Save<T>(T t)
        {
            try
            {
                var collection = GetCollection<T>();
                var result = collection.Save(t);
                return result.DocumentsAffected > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public bool InsertBatch<T>(IEnumerable<T> t)
        {
            try
            {
                var database = server.GetDatabase(databaseName);   //mongodb中的数据库

                var collection = GetCollection<T>();

                collection.InsertBatch(t);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 批量查询

        public List<T> Search<T>(Expression<Func<T, bool>> func, bool forcemaster = false)
        {
            var list = new List<T>();

            try
            {
                //是否强制使用 “主服务器”
                if (forcemaster)
                {
                    var collection = GetCollection<T>();
                    list = collection.Find(Query<T>.Where(func)).ToList();
                }
                else
                {
                    var collection = GetCollection<T>();

                    list = collection.Find(Query<T>.Where(func)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return list;
        }

        #endregion

        #region 单条查询
        /// <summary>
        /// 单条查询
        /// </summary>
        public T SearchOne<T>(Expression<Func<T, bool>> func, bool forcemaster = false)
        {
            T t = default(T);

            try
            {
                if (forcemaster)
                {
                    var collection = GetCollection<T>();

                    t = collection.FindOne(Query<T>.Where(func));
                }
                else
                {
                    var collection = GetCollection<T>();

                    t = collection.FindOne(Query<T>.Where(func));
                }

                return t;
            }
            catch (Exception ex)
            {
                return t;
            }
        }
        #endregion

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<T> SearchAll<T>()
        {
            var list = new List<T>();

            try
            {

                var collection = GetCollection<T>();

                list = collection.FindAll().ToList();

                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }
    }
}
