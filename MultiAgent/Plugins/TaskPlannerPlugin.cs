// using System;
// using System.Collections.Generic;

// namespace Plugins
// {
//     // Define the structure for each task
//     public class Task
//     {
//         public string TaskName { get; set; }
//         public DateTime Deadline { get; set; }
//         public List<string> Steps { get; set; }
//         public bool IsCompleted { get; set; }
//     }

//     public class TaskPlannerPlugin
//     {
//         // In-memory list to store tasks (in a real-world scenario, you might connect this to a database)
//         private readonly List<Task> _tasks = new List<Task>();

//         // Method to add a task with a deadline
//         [KernelFunction("add_task")]
//         [Description("Adds the task to the plan")]
//         public void AddTask(string taskName, DateTime deadline)
//         {
//             var task = new Task
//             {
//                 TaskName = taskName,
//                 Deadline = deadline,
//                 Steps = new List<string>(),
//                 IsCompleted = false
//             };
//             _tasks.Add(task);
//             Console.WriteLine($"Task '{taskName}' has been added with a deadline on {deadline.ToShortDateString()}.");
//         }

//         // Method to update a task's deadline
//         [KernelFunction("set_deadline")]
//         [Description("update a task's deadline")]
//         public void SetDeadline(string taskName, DateTime newDeadline)
//         {
//             var task = _tasks.Find(t => t.TaskName == taskName);
//             if (task != null)
//             {
//                 task.Deadline = newDeadline;
//                 Console.WriteLine($"Deadline for task '{taskName}' has been updated to {newDeadline.ToShortDateString()}.");
//             }
//             else
//             {
//                 Console.WriteLine($"Task '{taskName}' not found.");
//             }
//         }

//         // Method to create a study plan by allocating study time for each task
//         [KernelFunction("create_study_plan")]
//         [Description("create a study plan by allocating study time for each task")]
//         public void CreateStudyPlan(TimeSpan dailyStudyHours)
//         {
//             foreach (var task in _tasks)
//             {
//                 if (!task.IsCompleted)
//                 {
//                     TimeSpan timeToDeadline = task.Deadline - DateTime.Now;
//                     double daysRemaining = timeToDeadline.TotalDays;

//                     // Simple allocation logic: spread study time across remaining days
//                     Console.WriteLine($"For task '{task.TaskName}', study for {dailyStudyHours.TotalHours / daysRemaining:F2} hours per day.");
//                 }
//             }
//         }

//         // Method to get a list of pending tasks
//         [KernelFunction("list_pending_tasks")]
//         [Description("get a list of pending tasks")]
//         public void GetPendingTasks()
//         {
//             Console.WriteLine("Pending Tasks:");
//             foreach (var task in _tasks)
//             {
//                 if (!task.IsCompleted)
//                 {
//                     Console.WriteLine($"- {task.TaskName} (Deadline: {task.Deadline.ToShortDateString()})");
//                 }
//             }
//         }

//         // Method to break down larger tasks into smaller steps
//         [KernelFunction("break ")]
//         [Description("break down larger tasks into smaller steps")]
//         public void BreakDownTask(string taskName, List<string> steps)
//         {
//             var task = _tasks.Find(t => t.TaskName == taskName);
//             if (task != null)
//             {
//                 task.Steps.AddRange(steps);
//                 Console.WriteLine($"Task '{taskName}' has been broken down into steps:");
//                 foreach (var step in steps)
//                 {
//                     Console.WriteLine($"  - {step}");
//                 }
//             }
//             else
//             {
//                 Console.WriteLine($"Task '{taskName}' not found.");
//             }
//         }

//         // Method to send reminders for upcoming deadlines
//         [KernelFunction("send reminder")]
//         [Description("send reminders for upcoming deadlines")]
//         public void SendReminder()
//         {
//             DateTime currentDateTime = DateTime.Now;
//             foreach (var task in _tasks)
//             {
//                 TimeSpan timeToDeadline = task.Deadline - currentDateTime;
//                 if (!task.IsCompleted && timeToDeadline.TotalDays <= 3) // Send reminders for tasks due in 3 days or less
//                 {
//                     Console.WriteLine($"Reminder: Task '{task.TaskName}' is due in {timeToDeadline.Days} days.");
//                 }
//             }
//         }
//     }
// }
