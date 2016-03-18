﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoStart
{
    public class MongoDBHelperT<T>
    {
        #region 静态构造加载MongoClientSettings
        private static readonly string connectionString = ConfigurationManager.AppSettings["mongodbServerList"];
        static MongoClientSettings setting = null;
        static void MongoDBHelper()
        {
            var ips = connectionString.Split(';');
            var servicesList = new List<MongoServerAddress>();

            foreach (var ip in ips)
            {
                var host = ip.Split(':')[0];
                var port = Convert.ToInt32(ip.Split(':')[1]);
                servicesList.Add(new MongoServerAddress(host, port));
            }

            setting = new MongoClientSettings();
            setting.ReplicaSetName = "datamip";

            //集群中的服务器列表
            setting.Servers = servicesList;
        }
        #endregion


        MongoServer server = null;

        private string tableName;

        private string databaseName;

        public MongoDBHelperT(string databaseName, string tableName)
        {
            this.databaseName = databaseName;
            this.tableName = tableName;

            server = new MongoClient(setting).GetServer();
        }

        public bool Remove(Expression<Func<T, bool>> func)
        {
            try
            {
                var database = server.GetDatabase(databaseName);

                var collection = database.GetCollection<T>(tableName);

                var query = Query<T>.Where(func);

                var result = collection.Remove(query);

                return result.Response["ok"].AsInt32 > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveAll()
        {
            try
            {
                var database = server.GetDatabase(databaseName);   //mongodb中的数据库

                var collection = database.GetCollection<T>(tableName);

                var result = collection.RemoveAll();

                return result.Response["ok"].AsInt32 > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region 单条插入
        /// <summary>
        /// 单条插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public bool Insert(T t)
        {
            try
            {
                var database = server.GetDatabase(databaseName);   //mongodb中的数据库

                var collection = database.GetCollection<T>(tableName);

                var result = collection.Insert(t);
                return result.DocumentsAffected > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 单条覆盖，如果不存在插入，如果存在覆盖
        /// <summary>
        /// 单条覆盖，如果不存在插入，如果存在覆盖
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public bool Save(T t)
        {
            try
            {
                var database = server.GetDatabase(databaseName);   //mongodb中的数据库

                var collection = database.GetCollection<T>(tableName);
                var result = collection.Save(t);
                return result.DocumentsAffected > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 批量插入
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public bool Insert(IEnumerable<T> t)
        {
            try
            {
                var database = server.GetDatabase(databaseName);   //mongodb中的数据库

                var collection = database.GetCollection<T>(tableName);

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

        public List<T> Search(Expression<Func<T, bool>> func, bool forcemaster = false)
        {
            var list = new List<T>();

            try
            {
                //是否强制使用 “主服务器”
                if (forcemaster)
                {
                    var database = server.GetDatabase(databaseName);   //mongodb中的数据库

                    var collection = database.GetCollection<T>(tableName);
                    list = collection.Find(Query<T>.Where(func)).ToList();
                }
                else
                {
                    var database = server.GetDatabase(databaseName);    //mongodb中的数据库

                    var collection = database.GetCollection<T>(tableName);

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
        public T SearchOne(Expression<Func<T, bool>> func, bool forcemaster = false)
        {
            T t = default(T);

            try
            {
                if (forcemaster)
                {
                    var database = server.GetDatabase(databaseName);   //mongodb中的数据库

                    var collection = database.GetCollection<T>(tableName);

                    t = collection.FindOne(Query<T>.Where(func));
                }
                else
                {
                    var database = server.GetDatabase(databaseName);   //mongodb中的数据库

                    var collection = database.GetCollection<T>(tableName);

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
        public List<T> SearchAll()
        {
            var list = new List<T>();

            try
            {
                var database = server.GetDatabase(databaseName);    //mongodb中的数据库

                var collection = database.GetCollection<T>(tableName);

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
