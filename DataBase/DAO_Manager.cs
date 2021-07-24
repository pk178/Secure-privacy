using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secure_privacy.DataBase
{
    public class DAO_Manager
    {
        public static IMongoDatabase _database;

        public static bool Init(string connectionString, string dbName)
        {
            try
            {
                _database = new MongoClient(connectionString).GetDatabase(dbName);
                BsonSerializer.RegisterSerializer(DateTimeSerializer.LocalInstance);
                ConventionRegistry.Register("IgnoreExtraElements", new ConventionPack { new IgnoreExtraElementsConvention(true) }, type => true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "InitMongoDB", "Khởi tạo MongoDB");

                return false;
            }

            return true;
        }
    }
}
