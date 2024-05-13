using AutoFixture;
using Escola.Application.Service;
using Escola.Domain.Dtos;
using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Dtos.ViewModels;
using Escola.Domain.Models;
using Escola.Infrastructure.Repository;
using Moq;
using NUnit.Framework;

namespace Escola.Tests.Service
{
    public class AlunoServiceTest
    {
        private AlunoService _alunoService;
        private Mock<AlunoRepository> _alunoRepository;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {

            _alunoRepository = new();
            _fixture = new Fixture();
        }

        [Test]
        public async Task CreateAluno_WhenValidData()
        {
            //Arrange            
            var aluno = _fixture.Create<AlunoInputModel>();

            //Action
            var result = await _alunoService.Create(aluno);

            //Assert
            _alunoRepository
                .Verify(e => e.Create(It.IsAny<Aluno>()), Times.Once);
            Assert.Equals(ResponseGeneric.Successful(), result);
        }

        [Test]
        public async Task Create_RepositoryThrowsException_ReturnsFailureResponse()
        {
            // Arrange
            var alunoInputModel = new AlunoInputModel();
            var exceptionMessage = "Simulated exception";
            _alunoRepository.Setup(repo => repo.Create(It.IsAny<Aluno>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _alunoService.Create(alunoInputModel);

            // Assert
            Assert.Equals(result.Success, false);
            Assert.Equals(exceptionMessage, result.Message);
        }

        [Test]
        public async Task UpdateAluno_WhenValidData()
        {
            //Arrange            
            var aluno = _fixture.Create<AlunoInputModel>();

            //Action
            var result = await _alunoService.Update(aluno);

            //Assert
            _alunoRepository
                .Verify(e => e.Create(It.IsAny<Aluno>()), Times.Once);
            Assert.Equals(ResponseGeneric.Successful(), result);
        }

        [Test]
        public async Task Delete_AlunoSuccessfully_CallsRepositoryDelete()
        {
            // Arrange
            var id = 1;
            _alunoRepository.Setup(repo => repo.Delete(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _alunoService.Delete(id);

            // Assert
            _alunoRepository.Verify(repo => repo.Delete(id), Times.Once);
            Assert.Equals(result.Success, true);
        }

        [Test]
        public async Task Delete_RepositoryThrowsException_ReturnsFailureResponse()
        {
            // Arrange
            var id = 1;
            var exceptionMessage = "Simulated exception";
            _alunoRepository.Setup(repo => repo.Delete(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _alunoService.Delete(id);

            // Assert
            Assert.Equals(result.Success, false);
            Assert.Equals(exceptionMessage, result.Message);
        }

        [Test]
        public async Task Get_AlunoExists_ReturnsAlunoViewModel()
        {
            // Arrange
            var id = 1;
            var aluno = new Aluno();
            var expectedViewModel = AlunoViewModel.Map(aluno);
            _alunoRepository.Setup(repo => repo.Get(id)).ReturnsAsync(aluno);
            

            // Act
            var result = await _alunoService.Get(id);

            // Assert
            Assert.Equals(expectedViewModel, result);
        }

        [Test]
        public async Task Get_AlunoDoesNotExist_ReturnsNull()
        {
            // Arrange
            var id = 1;
            _alunoRepository.Setup(repo => repo.Get(id)).ReturnsAsync(new Aluno());

            // Act
            var result = await _alunoService.Get(id);

            // Assert
            Assert.Equals(result, null);
        }        
    }
}
