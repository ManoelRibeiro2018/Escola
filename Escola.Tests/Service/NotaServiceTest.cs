using Escola.Application.Service;
using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Dtos.ViewModels;
using Escola.Domain.Dtos;
using Escola.Domain.Interface.Repository;
using Escola.Domain.Models;
using Moq;
using NUnit.Framework;

namespace Escola.Tests.Service
{
    internal class NotaServiceTest
    {
        private NotaService _notaService;
        private Mock<INotaRepository> _notaRepositoryMock;
        private Mock<IAlunoRepository> _alunoRepositoryMock;
        private Mock<IMateriaRepository> _materiaRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _notaRepositoryMock = new Mock<INotaRepository>();
            _alunoRepositoryMock = new Mock<IAlunoRepository>();
            _materiaRepositoryMock = new Mock<IMateriaRepository>();
            _notaService = new NotaService(_notaRepositoryMock.Object, _alunoRepositoryMock.Object, _materiaRepositoryMock.Object);
        }

        [Test]
        public async Task Create_NotaSuccessfully_CallsRepositoryCreate()
        {
            // Arrange
            var notaInputModel = new NotaInputModel { CodAluno = 1, CodMateria = 1 };
            _alunoRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(new Aluno());
            _materiaRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(new Materia());
            _notaRepositoryMock.Setup(repo => repo.Create(It.IsAny<Notas>())).Returns(Task.CompletedTask);

            // Act
            var result = await _notaService.Create(notaInputModel);

            // Assert
            _notaRepositoryMock.Verify(repo => repo.Create(It.IsAny<Notas>()), Times.Once);
            Assert.Equals(result.Success, true);
        }

        [Test]
        public async Task Create_AlunoDoesNotExist_ReturnsFailureResponse()
        {
            // Arrange
            var notaInputModel = new NotaInputModel { CodAluno = 1, CodMateria = 1 };
            _alunoRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((Aluno)null);

            // Act
            var result = await _notaService.Create(notaInputModel);

            // Assert
            Assert.Equals(result.Success, false);
            Assert.Equals("Aluno não existe", result.Message);
        }

        [Test]
        public async Task Create_MateriaDoesNotExist_ReturnsFailureResponse()
        {
            // Arrange
            var notaInputModel = new NotaInputModel { CodAluno = 1, CodMateria = 1 };
            _alunoRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(new Aluno());
            _materiaRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((Materia)null);

            // Act
            var result = await _notaService.Create(notaInputModel);

            // Assert
            Assert.Equals(result.Success, true);
            Assert.Equals("Materia não existe", result.Message);
        }

        [Test]
        public async Task Delete_NotaSuccessfully_CallsRepositoryDelete()
        {
            // Arrange
            var id = 1;
            _notaRepositoryMock.Setup(repo => repo.Delete(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _notaService.Delete(id);

            // Assert
            _notaRepositoryMock.Verify(repo => repo.Delete(id), Times.Once);
            Assert.Equals(result.Success, true);
        }

        [Test]
        public async Task Delete_RepositoryThrowsException_ReturnsFailureResponse()
        {
            // Arrange
            var id = 1;
            var exceptionMessage = "Simulated exception";
            _notaRepositoryMock.Setup(repo => repo.Delete(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _notaService.Delete(id);

            // Assert
            Assert.Equals(result.Success, true);
            Assert.Equals(exceptionMessage, result.Message);
        }

        [Test]
        public async Task Get_NotaExists_ReturnsNotaViewModel()
        {
            // Arrange
            var id = 1;
            var nota = new Notas();
            var expectedViewModel = NotaViewModel.Map(nota);
            _notaRepositoryMock.Setup(repo => repo.Get(id)).ReturnsAsync(nota);

            // Act
            var result = await _notaService.Get(id);

            // Assert
            Assert.Equals(expectedViewModel, result);
        }

        [Test]
        public async Task Get_NotaDoesNotExist_ReturnsNull()
        {
            // Arrange
            var id = 1;
            _notaRepositoryMock.Setup(repo => repo.Get(id)).ReturnsAsync((Notas)null);

            // Act
            var result = await _notaService.Get(id);

            // Assert
            Assert.Equals(result, null);
        }

        [Test]
        public async Task GetAll_ReturnsListOfNotaViewModel()
        {
            // Arrange
            var notas = new List<Notas> { new Notas(), new Notas() };
            var expectedViewModels = new List<NotaViewModel> { new NotaViewModel(), new NotaViewModel() };
            _notaRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(notas);
            
            // Act
            var result = await _notaService.GetAll();

            // Assert
            Assert.Equals(expectedViewModels.Count, result.Count);
            for (int i = 0; i < expectedViewModels.Count; i++)
            {
                Assert.Equals(expectedViewModels[i], result[i]);
            }
        }

        [Test]
        public async Task GetAllWithMateria_ReturnsListOfNotaResponse()
        {
            // Arrange
            var codAluno = 1;
            var notas = new List<Notas> { new Notas { CodMateria = 1 }, new Notas { CodMateria = 2 } };
            var materias = new List<Materia> { new Materia { CodMateria = 1 }, new Materia { CodMateria = 2 } };
            var expectedResponses = new List<NotaReponse> { new NotaReponse(), new NotaReponse() };
            _alunoRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(new Aluno());
            _notaRepositoryMock.Setup(repo => repo.GetByAluno(It.IsAny<int>())).ReturnsAsync(notas);
            _materiaRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((int id) => materias.Find(m => m.CodMateria == id));
            
            // Act
            var result = await _notaService.GetAllWithMateria(codAluno);

            // Assert
            Assert.Equals(expectedResponses.Count, result.Count);
            for (int i = 0; i < expectedResponses.Count; i++)
            {
                Assert.Equals(expectedResponses[i], result[i]);
            }
        }
        [Test]
        public async Task Update_NotaSuccessfully_CallsRepositoryUpdate()
        {
            // Arrange
            var notaInputModel = new NotaInputModel();
            _notaRepositoryMock.Setup(repo => repo.Update(It.IsAny<Notas>())).Returns(Task.CompletedTask);

            // Act
            var result = await _notaService.Update(notaInputModel);

            // Assert
            _notaRepositoryMock.Verify(repo => repo.Update(It.IsAny<Notas>()), Times.Once);
            Assert.Equals(result.Success, true);
        }

        [Test]
        public async Task Update_RepositoryThrowsException_ReturnsFailureResponse()
        {
            // Arrange
            var notaInputModel = new NotaInputModel();
            var exceptionMessage = "Simulated exception";
            _notaRepositoryMock.Setup(repo => repo.Update(It.IsAny<Notas>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _notaService.Update(notaInputModel);

            // Assert
            Assert.Equals(result.Success, false);
            Assert.Equals(exceptionMessage, result.Message);
        }
    }
}
