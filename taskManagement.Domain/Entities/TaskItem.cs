
using System;
using System.Collections.Generic;

namespace taskManagement.Domain.Entities;


public enum TaskStatus
{
    Pending = 1,
    InProgress = 2,
    Completed = 3

}

public class TaskItem
{
    public int id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime createdAt { get; private set; }
    public DateTime updatedAt { get; private set; }
    public TaskStatus status { get; private set; }



    public TaskItem(string title, string description)
    {

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        Title = title;
        Description = description ?? "";
        createdAt = DateTime.UtcNow;
        updatedAt = createdAt;
        status = TaskStatus.Pending;


    }
    public void MarkAsInProgress()
    {
        if (status == TaskStatus.Completed)
            throw new InvalidOperationException("Cannot mark a completed task as in progress.");
        status = TaskStatus.InProgress;
        updatedAt = DateTime.UtcNow;
    }
    public void MarkAsCompleted()
    {
        if (status == TaskStatus.Completed)
            throw new InvalidOperationException("Task is already completed.");
        status = TaskStatus.Completed;
        updatedAt = DateTime.UtcNow;
    }

    public void UpdateTitle(string title)
    {

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        Title = title;
    }


    public void Update(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");

        Title = title;
        Description = description ?? "";
        updatedAt = DateTime.UtcNow;
    }
}
