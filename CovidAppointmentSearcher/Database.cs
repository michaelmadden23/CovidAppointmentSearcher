using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using CovidAppointmentSearcher.Model;

namespace CovidAppointmentSearcher
{
    public static class CovidSearchDatabase
    {
        private static SQLiteAsyncConnection _database;
        private static string dbPath = "D:\\Covid Search\\MasterDatabase.db";
        static CovidSearchDatabase()
        {
            _database = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);
        }

        public static async Task<object> AsyncInsert(object record)
        {
            await _database.InsertAsync(record);
            return record;
        }

        public static async Task<int> AsyncInsertAll(IEnumerable<object> records)
        {
            return await _database.InsertAllAsync(records);
        }

        public static async Task<object> AsyncUpdate(object record)
        {
            return await _database.UpdateAsync(record);
        }
        public static async Task<int> AsyncUpdateAll(IEnumerable<object> records)
        {
            return await _database.UpdateAllAsync(records);
        }

        public static async Task<List<User>> SelectAllUsers()
        {
            return await _database.Table<User>().ToListAsync();
        }

        public static Task<List<User>> GetUsers()
        {
            var allUsers = SelectAllUsers();

            return allUsers;
        }

        public static List<User> GetAllUsers()
        {
            var allUsers = SelectAllUsers();

            return allUsers.Result;
        }

    }

}
