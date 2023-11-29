using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Data.Sqlite;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var todoList = new TodoList();
            do
            {
                Console.WriteLine("1) Добавить задачу");
                Console.WriteLine("2) Вывести все задачи");
                Console.WriteLine("3) Сохранить задачи");
                Console.WriteLine("4) Загрузить задачи");
                Console.WriteLine("q) Выйти");
                var key = Console.ReadKey();
                Console.WriteLine();
                switch (key.KeyChar)
                {
                    case '1':
                        var task = ReadTask();
                        todoList.AddTask(task);
                        break;

                    case '2':
                        foreach (var todo in todoList.AllTasks())
                        {
                            PrintTask(todo);
                        }
                        break;

                    case '3':
                        SaveTasksMenu(todoList);
                        break;

                    case '4':
                        LoadTasksMenu(todoList);
                        break;
                }
            }
            while (Console.ReadKey().KeyChar != 'q');
        }

        private static void SaveTasksMenu(TodoList todoList)
        {
            Console.WriteLine("1) Сохранить в JSON");
            Console.WriteLine("2) Сохранить в XML");
            Console.WriteLine("3) Сохранить в SQLite");
            var saveKey = Console.ReadKey();
            Console.WriteLine();

            switch (saveKey.KeyChar)
            {
                case '1':
                    todoList.SaveTasksToJson();
                    break;

                case '2':
                    todoList.SaveTasksToXml();
                    break;

                case '3':
                    todoList.SaveTasksToSQLite();
                    break;
            }
        }

        private static void LoadTasksMenu(TodoList todoList)
        {
            Console.WriteLine("1) Загрузить из JSON");
            Console.WriteLine("2) Загрузить из XML");
            Console.WriteLine("3) Загрузить из SQLite");
            var loadKey = Console.ReadKey();
            Console.WriteLine();

            switch (loadKey.KeyChar)
            {
                case '1':
                    todoList.LoadTasksFromJson();
                    break;

                case '2':
                    todoList.LoadTasksFromXml();
                    break;

                case '3':
                    todoList.LoadTasksFromSQLite();
                    break;
            }
        }

        private static void PrintTask(Task task)
        {
            Console.WriteLine($"{task.Title}-{task.Deadline}");
        }

        private static Task ReadTask()
        {
            Console.Write("Введите название задачи: ");
            string taskName = Console.ReadLine();
            Console.Write("Введите приоритет: ");
            int priority = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите дедлайн (гггг-мм-дд): ");
            DateTime deadline = DateTime.Parse(Console.ReadLine());
            return new Task
            {
                Deadline = deadline,
                IsCompleted = false,
                Priority = priority,
                Title = taskName
            };
        }
    }
}