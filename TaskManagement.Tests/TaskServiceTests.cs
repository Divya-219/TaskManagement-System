using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskManagement.Domain.Entities;
using TaskManagement.Application.Services;
using TaskManagement.Application.Abstractions.Persistence;
using Moq;
using Xunit;
namespace TaskManagement.Tests;
public class TaskServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateTask_WhenTitleIsValid()
    {
        // Arrange
        var mockRepo = new Mock<ITaskRepository>();
        var service = new TaskService(mockRepo.Object);
        string title = "Test Task";
        string description = "This is a test task.";
        // Act
        var result = await service.CreateAsync(title, description);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(title, result.Title);
        Assert.Equal(description, result.Description);
        mockRepo.Verify(r => r.AddAsync(It.IsAny<TaskItem>()), Times.Once);
    }
   [Fact]
   public async Task CreateAsync_ShouldThrowException_WhenTitleIsEmpty()
    {
        // Arrange
        var mockRepo = new Mock<ITaskRepository>();
        var service = new TaskService(mockRepo.Object);
        string title = "";
        string description = "This is a test task.";
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(title,description) ) ;
        mockRepo.Verify(r=>r.AddAsync(It.IsAny<TaskItem>()), Times.Never);


        
    }



}
