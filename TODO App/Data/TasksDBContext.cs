using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using TODO_App.Models;

namespace TODO_App.Data
{
    public class TasksDBContext
    {
        private const string _connectionString = "Server=localhost;Port=5432;Database=todoapp;User Id=postgres;Password=1234321";
        private const string _tableName = "task";
        private NpgsqlConnection _connection;
        public TasksDBContext()
        {
            _connection = new NpgsqlConnection(_connectionString);
            
        }
        public List<TaskModel> GetTasks()
        {
            _connection.Open();
            var list = new List<TaskModel>();
            var command = new NpgsqlCommand();
            command.Connection = _connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM " + _tableName;
            
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var task = new TaskModel()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    State = (TaskState)reader.GetInt32(3)
                };
                list.Add(task);
            }
            _connection.Close();
            return list;
        }
        public void AddTask(TaskModel task)
        {
            _connection.Open();
            var command = new NpgsqlCommand($"INSERT INTO {_tableName} (id,name,description,status) VALUES (default,$1,$2,$3)", _connection) { Parameters =
                {
                    new(){Value = task.Name},
                    new(){Value = task.Description},
                    new(){Value = (int)task.State}
                }
            };
            command.ExecuteNonQuery();
            
            _connection.Close();
        }
        public void EditTask(int ID, TaskModel task)
        {
            _connection.Open();
            var command = new NpgsqlCommand($"UPDATE {_tableName} SET (name,description,status) = ($1,$2,$3) WHERE id = {ID}", _connection)
            {
                Parameters =
                {
                    new(){Value = (task.Name!=null)?task.Name:string.Empty},
                    new(){Value = (task.Description!=null)?task.Description:string.Empty},
                    new(){Value = (int)task.State}
                }
            };
            command.ExecuteNonQuery();

            _connection.Close();
        }
        public void RemoveTask(int ID)
        {
            _connection.Open();
            var command = new NpgsqlCommand($"DELETE FROM {_tableName} WHERE id = {ID}", _connection);
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
