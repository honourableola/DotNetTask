using DotNetTask.API.Services.Implementations;
using DotNetTask.Data.Entities;
using DotNetTask.Data.Models;
using DotNetTask.Data.Repositories.Interfaces;
using Moq;

namespace DotNetTask.UnitTests.Services
{
    public class ApplicationServiceTests
    {
        private readonly Mock<IApplicationRepository> _mockApplicationRepository;
        private readonly ApplicationService _applicationService;

        public ApplicationServiceTests()
        {
            _mockApplicationRepository = new Mock<IApplicationRepository>();
            _applicationService = new ApplicationService(_mockApplicationRepository.Object);
        }
        [Fact]
        public async Task AddApplicationAsync_EmailAlreadyExists_ReturnsErrorMessage()
        {
           //Arrange
            var applicationRequest = new ApplicationRequest { Email = "existing@example.com" };
            _mockApplicationRepository.Setup(r => r.GetApplicationWithEmailAsync("existing@example.com")).ReturnsAsync(new Application());

            // Act
            var result = await _applicationService.AddApplicationAsync("programId", applicationRequest);

            // Assert
            Assert.False(result.Status);
            Assert.Equal("Email existing@example.com already exist", result.Message);
        }

        [Fact]
        public async Task AddApplicationAsync_NewApplication_SuccessfullyCreated()
        {
            // Arrange
            var applicationRepositoryMock = new Mock<IApplicationRepository>();
            applicationRepositoryMock.Setup(repo => repo.GetApplicationWithEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((Application)null);
            // No existing application with the same email
            applicationRepositoryMock.Setup(repo => repo.AddApplicationAsync(It.IsAny<Application>()))
                .ReturnsAsync(new Application()); 

            var service = new ApplicationService(applicationRepositoryMock.Object);
            var request = new ApplicationRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "test@example.com",
                Phone = "123456789",
                Nationality = "US",
                CurrentResidence = "New York",
                IDNumber = "123456",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "Male",
                Responses = new List<AnswerRequest>
                {
                    new AnswerRequest
                    {
                        QuestionId = "questionId1",
                        SingleResponse = "Yes"
                    },
                    new AnswerRequest
                    {
                         QuestionId = "questionId3",
                         SingleResponse = "Visual"
                    },
                    new AnswerRequest
                    {
                        QuestionId = "questionId4",
                        MultipleResponse = new List<string> { "June 15-20", "July 10-15" }
                    }
                }
            };

            // Act
            var result = await service.AddApplicationAsync("programId", request);

            // Assert
            Assert.True(result.Status);
            Assert.Equal("Application created successfully", result.Message);
        }
    }
}
