using DotNetTask.API.Services.Interfaces;
using DotNetTask.Data.Entities;
using DotNetTask.Data.Enums;
using DotNetTask.Data.Models;
using DotNetTask.Data.Repositories.Interfaces;

namespace DotNetTask.API.Services.Implementations
{
    public class ProgramService : IProgramService
    {
        private readonly IProgramRepository _programRepository;

        public ProgramService(IProgramRepository programRepository)
        {
            _programRepository = programRepository;
        }
        public async Task<BaseResponse> AddProgramAsync(CreateProgramRequest request)
        {
            var response = new BaseResponse { Message = "Program creation NOT successful" };
            var existingProgram = await _programRepository.GetProgramByTitleAsync(request.ProgramTitle);
            if (existingProgram != null)
            {
                response.Message = $"Program with name {request.ProgramTitle} already exist";
                return response;
            }

            var program = new ProgramData
            {
                Id = Guid.NewGuid().ToString(),
                ProgramTitle = request.ProgramTitle,
                ProgrammeDescription = request.ProgrammeDescription
            };

            var programQuestions = new List<Question>();

            foreach (var question in request.Questions)
            {
                var questionEntty = new Question
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = (QuestionType)Enum.Parse(typeof(QuestionType), question.Type),
                    QuestionText = question.QuestionText,
                    Options = question.Options,
                    IncludeOtherOption = question.IncludeOtherOption,
                    MaxOptions = question.MaxOptions
                };
                programQuestions.Add(questionEntty);
            }
            program.Questions = programQuestions;

            var programAdded = await _programRepository.AddProgramAsync(program);
            if (programAdded == null)
            {
                return response;
            }

            response.Message = $"Program {request.ProgramTitle} created successfully";
            response.Status = true;
            return response;
        }

        public async Task<BaseResponse> EditProgramAsync(string id, UpdateProgramRequest request)
        {
            var response = new BaseResponse { Message = "Program Update NOT successful" };
            var existingProgram = await _programRepository.GetProgramQuestionsByIdAsync(id);
            if (existingProgram == null)
            {
                response.Message = $"Program with id {id} NOT found";
                return response;
            }

            var programQuestions = new List<Question>();

            foreach (var question in request.Questions)
            {
                var questionEntty = new Question
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = (QuestionType)Enum.Parse(typeof(QuestionType), question.Type),
                    QuestionText = question.QuestionText,
                    Options = question.Options,
                    IncludeOtherOption = question.IncludeOtherOption,
                    MaxOptions = question.MaxOptions
                };
                programQuestions.Add(questionEntty);
            }

            existingProgram.ProgramTitle = request.ProgramTitle;
            existingProgram.ProgrammeDescription = request.ProgrammeDescription;
            existingProgram.Questions = programQuestions;

            var programAdded = await _programRepository.UpdateProgramAsync(id, existingProgram);
            if (programAdded == null)
            {
                return response;
            }

            response.Message = $"Program updated successfully";
            response.Status = true;
            return response;
        }

        public async Task<ProgramResponse> GetProgramQuestionsByIdAsync(string id)
        {
            var response = new ProgramResponse { Message = $"Program with Id {id} NOT found" };
            var program = await _programRepository.GetProgramQuestionsByIdAsync(id);
            if (program == null)
            {
                return response;
            }

            return new ProgramResponse
            {
                Status = true,
                Message = $"Program with Id {id} retrieved successfully",
                Id = program.Id,
                ProgramTitle = program.ProgramTitle,
                ProgrammeDescription = program.ProgrammeDescription,
                Questions = program.Questions.Select(q => new QuestionResponse
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    Type = q.Type.ToString(),
                    Options = q.Options,
                    IncludeOtherOption = q.IncludeOtherOption,
                    MaxOptions = q.MaxOptions
                }).ToList()
            };
        }
    }
}
