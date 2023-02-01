using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
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
            var book = await _unitOfWork.TestRepository.GetByIdAsync(id);

            var bookDto = _mapper.Map<TestDto>(book);

            return bookDto;
        }

        public async Task<TestDto> UpdateAsync(TestDto model)
        {
            var book = _mapper.Map<Test>(model);

            _unitOfWork.TestRepository.Update(book);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<Test, TestDto>(book);
        }

        public async Task<IEnumerable<string>> GetAvaliableTestNames(string userId)
        {
            var k = (await _unitOfWork.TestRepository.GetAllAsync()).Select(x => x.Name);

            return k;
        }
    }
}
