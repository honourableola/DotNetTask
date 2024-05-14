using DotNetTask.API.Services.Interfaces;
using DotNetTask.Data.Entities;
using DotNetTask.Data.Models;
using DotNetTask.Data.Repositories.Interfaces;

namespace DotNetTask.API.Services.Implementations
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        public async Task<BaseResponse> AddApplicationAsync(string programId, ApplicationRequest application)
        {
            var result = new BaseResponse { Message = "Application NOT successful" };
            var existingApplication = await _applicationRepository.GetApplicationWithEmailAsync(application.Email);
            if (existingApplication != null)
            {
                result.Message = $"Email {application.Email} already exist";
                return result;
            }

            var responses = new List<Response>();

            foreach (var answer in application.Responses)
            {
                var response = new Response
                {
                    Id = Guid.NewGuid().ToString(),
                    QuestionId = answer.QuestionId,
                };

                if (answer.MultipleResponse != null)
                {
                    // If it's multiple-choice, value should be a list
                    response.Value = string.Join(", ", answer.MultipleResponse); ;
                }
                else
                {
                    // Otherwise, it's a single value
                    response.Value = answer.SingleResponse?.ToString();
                }
                responses.Add(response);
            }

            var applicationEntity = new Application
            {
                Id = Guid.NewGuid().ToString(),
                ProgramId = programId,
                FirstName = application.FirstName,
                LastName = application.LastName,
                Email = application.Email,
                Phone = application.Phone,
                Nationality = application.Nationality,
                CurrentResidence = application.CurrentResidence,
                IDNumber = application.IDNumber,
                DateOfBirth = application.DateOfBirth,
                Gender = application.Gender,
                Responses = responses
            };

            var programAdded = await _applicationRepository.AddApplicationAsync(applicationEntity);
            if (programAdded == null)
            {
                return result;
            }

            result.Message = $"Application created successfully";
            result.Status = true;
            return result;
        }
    }
}
