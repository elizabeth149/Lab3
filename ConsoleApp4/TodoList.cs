using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Data.Sqlite;

namespace ConsoleApp4
{
    public class TodoList
    {
        private readonly List<Task> tasks = new List<Task>();
        private const string JsonFilePath = "tasks.json";
        private const string XmlFilePath = "tasks.xml";
        private const string DbConnectionString = "Data Source=tasks.db";

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public void RemoveTask(Task task)
        {
            tasks.Remove(task);
        }

        public IEnumerable<Task> GetMostImportantTasks()
        {
            var highestPriority = tasks.Where(t => !t.IsCompleted).Max(t => t.Priority);
            return tasks.Where(t => !t.IsCompleted && t.Priority == highestPriority).ToList();
        }

        public IEnumerable<Task> GetNearestDeadlineTasks()
        {
            return tasks.Where(t => !t.IsCompleted).OrderBy(t => t.Deadline).ToList();
        }

        public IEnumerable<Task> AllTasks()
        {
            return tasks;
        }

        public void SaveTasksToJson()
        {
            var json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(JsonFilePath, json);
        }

        public void LoadTasksFromJson()
        {
            if (File.Exists(JsonFilePath))
            {
                var json = File.ReadAllText(JsonFilePath);
                tasks.Clear();
                tasks.AddRange(JsonSerializer.Deserialize<List<Task>>(json));
            }
        }

        public void SaveTasksToXml()
        {
            var serializer = new XmlSerializer(typeof(List<Task>));
            using (var stream = new StreamWriter(XmlFilePath))
            {
                serializer.Serialize(stream, tasks);
            }
        }

        public void LoadTasksFromXml()
        {
            if (File.Exists(XmlFilePath))
            {
                var serializer = new XmlSerializer(typeof(List<Task>));
                using (var stream = new StreamReader(XmlFilePath))
                {
                    tasks.Clear();
                    tasks.AddRange((List<Task>)serializer.Deserialize(stream));
                }
            }
        }

        public void SaveTasksToSQLite()
        {
            using (var connection = new SqliteConnection(DbConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    Title TEXT,
                    Priority INTEGER,
                    Deadline TEXT,
                    IsCompleted INTEGER
                )";

                    command.ExecuteNonQuery();

                    foreach (var task in tasks)
                    {
                        command.CommandText = $@"
                    INSERT INTO Tasks (Title, Priority, Deadline, IsCompleted)
                    VALUES ('{task.Title}', {task.Priority}, '{task.Deadline:yyyy-MM-dd}', {(task.IsCompleted ? 1 : 0)})";

                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        public void LoadTasksFromSQLite()
        {
            using (var connection = new SqliteConnection(DbConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Tasks";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new Task
                            {
                                Title = reader.GetString(0),
                                Priority = reader.GetInt32(1),
                                Deadline = DateTime.Parse(reader.GetString(2)),
                                IsCompleted = reader.GetInt32(3) == 1
                            });
                        }
                    }
                }
            }
        }
    }
}
