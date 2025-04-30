using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace Taks_Manager;


    internal class Program
{
    class Programs
    {
        static List<Task> tasks = new List<Task>();
        static HashSet<string> titles = new HashSet<string>();
        const string fileName = "taksk.json";

        static void Main(string[] args)
        {
            LoadTasks();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Task Manager ====");
                Console.WriteLine("1. Show all tasks");
                Console.WriteLine("2. Add task");
                Console.WriteLine("3. Mark as completed");
                Console.WriteLine("4. Remove  task");
                Console.WriteLine("5. Save and exit");
                Console.Write("Choice:");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowTasks();
                        break;
                    case "2":
                        AddTask();
                        break;
                    case "3":
                        CompleteTask();
                        break;
                    case "4":
                        RemoveTask();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid selection\r\n.");
                        break;
                }
                Console.WriteLine("\nPress key to continue...");
                Console.ReadKey();
            }
        }
        static void AddTask()
        {
            Console.Write("Title");
            string title = Console.ReadLine();

            if (titles.Contains(title))
            {
                Console.WriteLine("Task already exist");
                return;
            }
            Console.Write("Description:");
            string desc = Console.ReadLine();

            Console.Write("Deadline (yyyy-mm-dd):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime due))
            {
                Console.WriteLine("Invalid year");
                return;
            }
            Task task = new Task(title, desc, due);
            tasks.Add(task);
            titles.Add(title);

            Console.WriteLine("Task is added");
        }

        static void ShowTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Тhere are no tasks.");
                return;
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i = 1}. {tasks[i]}");
            }
        }
        static void CompleteTask()
        {
            ShowTasks();
            Console.Write("Select a task number to complete\r\n:");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= tasks.Count)
            {
                tasks[index - 1].IsCompleted = true;
                Console.WriteLine("Task marked as completed.");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        static void RemoveTask()
        {
            ShowTasks();
            Console.Write("Select task number to remove");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= tasks.Count)
            {
                titles.Remove(tasks[index - 1].Title);
                tasks.RemoveAt(index - 1);
                Console.WriteLine("Task is removed");
            }
            else
            {
                Console.WriteLine("Invalid selection ");
            }
        }
        static void LoadTasks()
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                tasks = JsonSerializer.Deserialize<List<Task>>(json);
                foreach (var task in tasks)
                    titles.Add(task.Title);
            }
        }
    }
}

    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }

        public Task(string title, string description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = false;
        }
        public override string ToString()
        {
            return $"{Title} - {(IsCompleted ? "✓" : "✗")} - From: {DueDate.ToShortDateString()}";
        }
    }
    


