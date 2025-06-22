﻿using CNPM_QBCA.Models;

namespace CNPM_QBCA.Services
{
    public class TaskService
    {
        private static List<TaskModel> tasks = new List<TaskModel>
        {
        
        };

        public List<TaskModel> GetTasksForUser(string user)
        {
            return tasks.Where(t => t.AssignedTo == user).ToList();
        }

        public TaskModel GetTaskById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public void UpdateTaskStatus(int id, string status)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null) task.Status = status;
        }
    }
}
