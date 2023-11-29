
using ConsoleApp4;
using System;
using System.Linq;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TestAddTask()
        {
            // Arrange
            var todoList = new ConsoleApp4.TodoList();
            var task1 = new ConsoleApp4.Task { Title = "Task 1", Priority = 2, Deadline = DateTime.Today.AddDays(1) };
            var task2 = new ConsoleApp4.Task { Title = "Task 2", Priority = 1, Deadline = DateTime.Today.AddDays(2) };

            // Act
            todoList.AddTask(task1);
            todoList.AddTask(task2);

            // Assert
            var allTasks = todoList.AllTasks().ToList();
            Assert.Equal(2, allTasks.Count);
            Assert.Equal("Task 1", allTasks[0].Title);
            Assert.Equal("Task 2", allTasks[1].Title);
        }

        [Fact]
        public void TestRemoveTask()
        {
            // Arrange
            var todoList = new ConsoleApp4.TodoList();
            var task1 = new ConsoleApp4.Task { Title = "Task 1", Priority = 2, Deadline = DateTime.Today.AddDays(1) };
            var task2 = new ConsoleApp4.Task { Title = "Task 2", Priority = 1, Deadline = DateTime.Today.AddDays(2) };

            // Act
            todoList.AddTask(task1);
            todoList.AddTask(task2);

            // Assert (before removal)
            var allTasksBefore = todoList.AllTasks().ToList();
            Assert.Equal(2, allTasksBefore.Count);

            // Remove a task
            todoList.RemoveTask(task1);

            // Assert (after removal)
            var allTasksAfter = todoList.AllTasks().ToList();
            Assert.Single(allTasksAfter);
            Assert.Equal("Task 2", allTasksAfter[0].Title);
        }

        [Fact]
        public void TestGetMostImportantTasks()
        {
            //Arrange
            var todoList = new ConsoleApp4.TodoList();
            var task1 = new ConsoleApp4.Task { Title = "Task 1", Priority = 2, Deadline = DateTime.Today.AddDays(1) };
            var task2 = new ConsoleApp4.Task { Title = "Task 2", Priority = 1, Deadline = DateTime.Today.AddDays(2) };
            var task3 = new ConsoleApp4.Task { Title = "Task 3", Priority = 2, Deadline = DateTime.Today.AddDays(3) };

            // Act
            todoList.AddTask(task1);
            todoList.AddTask(task2);
            todoList.AddTask(task3);

            // Assert
            var importantTasks = todoList.GetMostImportantTasks().ToList();
            Assert.Equal(2, importantTasks.Count);
            Assert.Equal("Task 1", importantTasks[0].Title);
            Assert.Equal("Task 3", importantTasks[1].Title);
        }

        [Fact]
        public void TestGetNearestDeadlineTasks()
        {
            // Arrange
            var todoList = new ConsoleApp4.TodoList();
            var task1 = new ConsoleApp4.Task { Title = "Task 1", Priority = 2, Deadline = DateTime.Today.AddDays(1) };
            var task2 = new ConsoleApp4.Task { Title = "Task 2", Priority = 1, Deadline = DateTime.Today.AddDays(2) };
            var task3 = new ConsoleApp4.Task { Title = "Task 3", Priority = 2, Deadline = DateTime.Today.AddDays(3) };

            // Act
            todoList.AddTask(task1);
            todoList.AddTask(task2);
            todoList.AddTask(task3);

            // Assert
            var nearestDeadlineTasks = todoList.GetNearestDeadlineTasks().ToList();
            Assert.Equal(3, nearestDeadlineTasks.Count);
            Assert.Equal("Task 1", nearestDeadlineTasks[0].Title);
            Assert.Equal("Task 2", nearestDeadlineTasks[1].Title);
            Assert.Equal("Task 3", nearestDeadlineTasks[2].Title);
        }
        [Fact]
        public void TestSaveAndLoadTasksJson()
        {
            // Arrange
            var todoList = new ConsoleApp4.TodoList();
            var task1 = new ConsoleApp4.Task { Title = "Task 1", Priority = 2, Deadline = DateTime.Today.AddDays(1) };
            var task2 = new ConsoleApp4.Task { Title = "Task 2", Priority = 1, Deadline = DateTime.Today.AddDays(2) };

            // Act
            todoList.AddTask(task1);
            todoList.AddTask(task2);
            todoList.SaveTasksToJson();
            todoList = new ConsoleApp4.TodoList(); // Reset todoList
            todoList.LoadTasksFromJson();

            // Assert
            var allTasks = todoList.AllTasks().ToList();
            Assert.Equal(2, allTasks.Count);
            Assert.Equal("Task 1", allTasks[0].Title);
            Assert.Equal("Task 2", allTasks[1].Title);
        }

        [Fact]
        public void TestSaveAndLoadTasksXml()
        {
            // Arrange
            var todoList = new ConsoleApp4.TodoList();
            var task1 = new ConsoleApp4.Task { Title = "Task 1", Priority = 2, Deadline = DateTime.Today.AddDays(1) };
            var task2 = new ConsoleApp4.Task { Title = "Task 2", Priority = 1, Deadline = DateTime.Today.AddDays(2) };

            // Act
            todoList.AddTask(task1);
            todoList.AddTask(task2);
            todoList.SaveTasksToXml();
            todoList = new ConsoleApp4.TodoList(); // Reset todoList
            todoList.LoadTasksFromXml();

            // Assert
            var allTasks = todoList.AllTasks().ToList();
            Assert.Equal(2, allTasks.Count);
            Assert.Equal("Task 1", allTasks[0].Title);
            Assert.Equal("Task 2", allTasks[1].Title);
        }

        [Fact]
        public void TestSaveAndLoadTasksSqlite()
        {
            // Arrange
            var todoList = new ConsoleApp4.TodoList();
            var task1 = new ConsoleApp4.Task { Title = "Task 1", Priority = 2, Deadline = DateTime.Today.AddDays(1) };
            var task2 = new ConsoleApp4.Task { Title = "Task 2", Priority = 1, Deadline = DateTime.Today.AddDays(2) };

            // Act
            todoList.AddTask(task1);
            todoList.AddTask(task2);
            todoList.SaveTasksToSQLite();
            todoList = new ConsoleApp4.TodoList(); // Reset todoList
            todoList.LoadTasksFromSQLite();

            // Assert
            var allTasks = todoList.AllTasks().ToList();
            Assert.Equal(2, allTasks.Count);
            Assert.Equal("Task 1", allTasks[0].Title);
            Assert.Equal("Task 2", allTasks[1].Title);
        }
    }
}
