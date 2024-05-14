using DotNetTask.API.Services.Implementations;
using DotNetTask.Data.Entities;
using DotNetTask.Data.Enums;
using DotNetTask.Data.Models;
using DotNetTask.Data.Repositories.Interfaces;
using Moq;

namespace DotNetTask.UnitTests.Services
{
    public class ProgramServiceTests
    {
        private readonly Mock<IProgramRepository> _mockProgramRepository;
        private readonly ProgramService _programService;

        public ProgramServiceTests()
        {
            _mockProgramRepository = new Mock<IProgramRepository>();
            _programService = new ProgramService(_mockProgramRepository.Object);
        }

        [Fact]
        public async Task AddProgramAsync_WhenProgramDoesNotExist_ReturnsSuccess()
        {
            // Arrange
            var createProgramRequest = new CreateProgramRequest
            {
                ProgramTitle = "New Program",
                ProgrammeDescription = "Description",
                Questions = new List<QuestionModel>
                {
                    // Add some sample questions here
                }
            };
            _mockProgramRepository.Setup(x => x.GetProgramByTitleAsync(It.IsAny<string>()))
                                  .ReturnsAsync((ProgramData)null);
            _mockProgramRepository.Setup(x => x.AddProgramAsync(It.IsAny<ProgramData>()))
                                  .ReturnsAsync(new ProgramData { Id = Guid.NewGuid().ToString() });

            // Act
            var result = await _programService.AddProgramAsync(createProgramRequest);

            // Assert
            Assert.True(result.Status);
            Assert.Equal("Program New Program created successfully", result.Message);
        }

        [Fact]
        public async Task EditProgramAsync_WhenProgramExists_ReturnsSuccess()
        {
            // Arrange
            var id = "programId";
            var updateProgramRequest = new UpdateProgramRequest
            {
                ProgramTitle = "Updated Program",
                ProgrammeDescription = "Updated Description",
                Questions = new List<QuestionModel>
                {
                    // Add some sample questions here
                }
            };
            var existingProgram = new ProgramData { Id = id }; // Existing program
            _mockProgramRepository.Setup(x => x.GetProgramQuestionsByIdAsync(id))
                                  .ReturnsAsync(existingProgram);
            _mockProgramRepository.Setup(x => x.UpdateProgramAsync(id, It.IsAny<ProgramData>()))
                                  .ReturnsAsync(existingProgram);

            // Act
            var result = await _programService.EditProgramAsync(id, updateProgramRequest);

            // Assert
            Assert.True(result.Status);
            Assert.Equal("Program updated successfully", result.Message);
        }

        [Fact]
        public async Task GetProgramQuestionsByIdAsync_WhenProgramExists_ReturnsProgramResponse()
        {
            var sampleQuestions = new List<Question>
            {
                new Question
                {
                    QuestionText = "Please describe your motivation for joining this bootcamp (Paragraph)",
                    Type = QuestionType.Paragraph,
                    Options = new List<string>(),
                    IncludeOtherOption = false,
                    MaxOptions = null
                },
                new Question
                {
                    QuestionText = "Have you attended any programming bootcamps before? (Yes/No)",
                    Type = QuestionType.YesNo,
                    Options = new List<string>(),
                    IncludeOtherOption = false,
                    MaxOptions = null
                }
            };

            var id = "programId";
            var existingProgram = new ProgramData
            {
                Id = id,
                ProgramTitle = "Program Title",
                ProgrammeDescription = "Program Description",
                Questions = sampleQuestions
            };

            //Arrange
            _mockProgramRepository.Setup(x => x.GetProgramQuestionsByIdAsync(id))
                                  .ReturnsAsync(existingProgram);

            // Act
            var result = await _programService.GetProgramQuestionsByIdAsync(id);

            // Assert
            Assert.True(result.Status);
            Assert.Equal($"Program with Id {id} retrieved successfully", result.Message);
            Assert.Equal(id, result.Id);
            Assert.Equal("Program Title", result.ProgramTitle);
            Assert.Equal("Program Description", result.ProgrammeDescription);
            Assert.NotNull(result.Questions);
            Assert.NotEmpty(result.Questions);
        }    
    }
}
