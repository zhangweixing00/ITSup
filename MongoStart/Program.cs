using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoStart
{
    class Program
    {
        static void Main(string[] args)
        {


            //连接信息mongodb://
            string conn = "localhost:27017";
            string database = "BPM";
            //
            MongoDBHelper.AddConnection(conn);
            MongoDBHelper helper = new MongoDBHelper(database);

            helper.Save<Person>(new Person() { Name="xx", PassWord="666", Uid=123 });


            return;string collection = "Person";
            MongoServer mongodb = new MongoClient(conn).GetServer();//连接数据库
            MongoDatabase mongoDataBase = mongodb.GetDatabase(database);//选择数据库名
            MongoCollection mongoCollection = mongoDataBase.GetCollection(collection);//选择集合，相当于表

            mongodb.Connect();

            //普通插入
            var o = new { Uid = 123, Name = "xixiNormal", PassWord = "111111" };
            mongoCollection.Insert(o);

            //对象插入
            Person p = new Person { Uid = 124, Name = "xixiObject", PassWord = "222222" };
            mongoCollection.Insert(p);

            //BsonDocument 插入
            BsonDocument b = new BsonDocument();
            b.Add("Uid", 125);
            b.Add("Name", "xixiBson");
            b.Add("PassWord", "333333");
            mongoCollection.Insert(b);
            mongoDataBase.DropCollection(conn);
            Console.ReadLine();
        }
    }
}
