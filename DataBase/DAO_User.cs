using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secure_privacy.DataBase
{
    public class DAO_User
    {
        public static async Task<bool> CreateUser(User user)
        {
            try
            {
                // Check user is existed or not
                var existUser = await GetUser(user.loginName);
                if (existUser != null) return false;

                var collection = DAO_Manager._database.GetCollection<User>("User");
                await collection.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "UpdateUser");
            }

            return true;
        }

        public static async Task<bool> UpdateUser(User user)
        {
            try
            {
                var collection = DAO_Manager._database.GetCollection<User>("User");
                var filter = Builders<User>.Filter.Where(x => x.loginName == user.loginName);
                var update = Builders<User>.Update
                    .Set(x => x.loginName, user.loginName)
                    .Set(x => x.password, user.password)                    
                    .Set(x => x.department, user.department)
                    .Set(x => x.fullName, user.fullName);
                var result = await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = false });

                if (result.ModifiedCount > 0) return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "UpdateUser");
            }

            return false;
        }

        public static async Task<bool> DeleteUser(string loginName)
        {
            try
            {
                var collection = DAO_Manager._database.GetCollection<User>("User");                
                var result = await collection.DeleteOneAsync(x => x.loginName == loginName);
                if (result.DeletedCount > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "DeleteUser");
            }

            return true;
        }

        public static async Task<User> GetUser(string loginName)
        {
            try
            {
                var collection = DAO_Manager._database.GetCollection<User>("User");
                var filter = Builders<User>.Filter.Where(x => x.loginName == loginName);
                return (await collection.FindAsync(filter))?.FirstOrDefault();

            }
            catch (Exception ex)
            {
                Log.Error(ex, "GetUser");
            }

            return new User();
        }
    }
}
