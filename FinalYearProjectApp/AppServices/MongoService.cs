using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;




namespace FinalYearProjectApp.AppServices
{
    public class MongoService
    {
        //public MongoCollection<T> Collection { get; private set; }

        //public MongoClient MongoHelper()
        //{
        //    //var connectionString = "mongodb://localhost:27020/mydb";
        //    //var mongoDBName = MongoUrl.Create(connectionString).DatabaseName;
        //    //var server = Mongo//.GetAllServers(connectionString);
        //    MongoClient mongoClient = new MongoClient();
        //    return mongoClient;
        //}
    }
    public class MongoHelper<T> where T : class
    {
        //public MongoCollection<T> Collection { get; private set; }

        //public MongoHelper()
        //{
        //    var con = new MongoConnectionStringBuilder("server=127.0.0.1;database=galary");

        //    var server = MongoServer.Create(con);
        //    var db = server.GetDatabase(con.DatabaseName);
        //    Collection = db.GetCollection<T>(typeof(T).Name.ToLower());
        //}
    }
}
