using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TestService: ITestService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(TestDto model)
        {
            var test = _mapper.Map<Test>(model);

            _unitOfWork.TestRepository.Add(test);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(string modelId)
        {
            await _unitOfWork.TestRepository.DeleteByIdAsync(modelId);

            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TestDto>> GetAllAsync()
        {
            var allTests = await _unitOfWork.TestRepository.GetAllAsync();

            var allTestsDto = _mapper.Map<IEnumerable<Test>, IEnumerable<TestDto>>(allTests);

            return allTestsDto;
        }

        public async Task<TestDto> GetByIdAsync(string id)
        {
            var test = await _unitOfWork.TestRepository.GetByIdAsync(id);

            var testDto = _mapper.Map<TestDto>(test);

            return testDto;
        }

        public async Task<TestDto> GetByIdWithDetailsAsync(string id)
        {
            var test = await _unitOfWork.TestRepository.GetByIdWithDetailsAsync(id);

            var resultTest = _mapper.Map<TestDto>(test);

            return resultTest;
        }

        public async Task<TestDto> UpdateAsync(TestDto model)
        {
            var test = _mapper.Map<Test>(model);

            _unitOfWork.TestRepository.Update(test);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<Test, TestDto>(test);
        }

        public async Task<IEnumerable<TestNames>> GetAvaliableTestNames(string userId)
        {
            var tests = (await _unitOfWork.UserTestsRepository.GetAllByUserIdWithDetailsAsync(userId))
                .Select(ut=>ut.Test);

            var testNames = _mapper.Map<IEnumerable<Test>, IEnumerable<TestNames>>(tests);

            return testNames;
        }

        public async Task<TestDescriptionModel> GetTestDescription(string testId)
        {
            var test = await _unitOfWork.TestRepository.GetByIdAsync(testId);

            var testDescription = _mapper.Map<TestDescriptionModel>(test);

            return testDescription;
        }

        public async Task<ResultModel> CheckTestAndGetResults(TestingAnswer testingAnswers)
        {
            var test = await _unitOfWork.TestRepository.GetByIdWithDetailsAsync(testingAnswers.TestId);

            var testAnswers = test.Questions.SelectMany(q => q.Answers);

            int resultScore = 0;

            foreach (var answer in testingAnswers.Answers)
            {
                if(testAnswers.FirstOrDefault(a=>a.Id == answer.Id).IsAnswerCorrect)
                    resultScore++;
            }
            var result = new ResultModel() { Result = resultScore, MaxTestResults = test.Questions.Count() };

            return result;
        }
    }
}
