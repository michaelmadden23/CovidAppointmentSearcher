using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using CovidWorkerService.Model;

namespace CovidWorkerService
{
    public class CovidSearchDatabase
    {
        private SQLiteAsyncConnection database;
        private  string dbPath = "C:\\Covid Search\\MasterDatabase.db";
        public CovidSearchDatabase()
        {
            database = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);
        }

        public async Task<object> AsyncInsert(object record)
        {
            int value = await database.InsertAsync(record);

            return value;
        }

        public  async Task<List<User>> SelectAllUsers()
        {
            return await database.Table<User>().ToListAsync();
        }

        public  async Task<List<SentNotification>> SelectAllSentNotifications()
        {
            return await database.Table<SentNotification>().ToListAsync();
        }

        public  List<User> GetAllUsers()
        {
            var allUsers = SelectAllUsers();

            return allUsers.Result;
        }

        public  List<SentNotification> GetAllNotifications()
        {
            var allNotifications = SelectAllSentNotifications();
            return allNotifications.Result;
        }

    }

}
