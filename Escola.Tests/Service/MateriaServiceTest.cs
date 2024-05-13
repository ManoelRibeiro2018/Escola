using Escola.Application.Service;
using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Dtos.ViewModels;
using Escola.Domain.Interface.Repository;
using Escola.Domain.Models;
using Moq;
using NUnit.Framework;

namespace Escola.Tests.Service
{
    public class MateriaServiceTest
    {
        private MateriaService _materiaService;
        private Mock<IMateriaRepository> _materiaRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _materiaRepositoryMock = new();
            _materiaService = new MateriaService(_materiaRepositoryMock.Object);
        }

        [Test]
        public async Task Create_MateriaSuccessfully_CallsRepositoryCreate()
        {
            // Arrange
            var materiaInputModel = new MateriaInputModel();
            _materiaRepositoryMock.Setup(repo => repo.Create(It.IsAny<Materia>())).Returns(Task.CompletedTask);

            // Act
            var result = await _materiaService.Create(materiaInputModel);

            // Assert
            _materiaRepositoryMock.Verify(repo => repo.Create(It.IsAny<Materia>()), Times.Once);
            Assert.Equals(result.Success, true);
        }

        [Test]
        public async Task Create_RepositoryThrowsException_ReturnsFailureResponse()
        {
            // Arrange
            var materiaInputModel = new MateriaInputModel();
            var exceptionMessage = "Simulated exception";
            _materiaRepositoryMock.Setup(repo => repo.Create(It.IsAny<Materia>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _materiaService.Create(materiaInputModel);

            // Assert
            Assert.Equals(result.Success, false);
            Assert.Equals(exceptionMessage, result.Message);
        }

        [Test]
        public async Task Delete_MateriaSuccessfully_CallsRepositoryDelete()
        {
            // Arrange
            var id = 1;
            _materiaRepositoryMock.Setup(repo => repo.Delete(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _materiaService.Delete(id);

            // Assert
            _materiaRepositoryMock.Verify(repo => repo.Delete(id), Times.Once);
            Assert.Equals(result.Success, true);
        }

        [Test]
        public async Task Delete_RepositoryThrowsException_ReturnsFailureResponse()
        {
            // Arrange
            var id = 1;
            var exceptionMessage = "Simulated exception";
            _materiaRepositoryMock.Setup(repo => repo.Delete(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _materiaService.Delete(id);

            // Assert
            Assert.Equals(result.Success, false);
            Assert.Equals(exceptionMessage, result.Message);
        }

        [Test]
        public async Task Get_MateriaExists_ReturnsMateriaViewModel()
        {
            // Arrange
            var id = 1;
            var materia = new Materia();
            var expectedViewModel = MateriaViewModel.Map(materia);
            _materiaRepositoryMock.Setup(repo => repo.Get(id)).ReturnsAsync(materia);

            // Act
            var result = await _materiaService.Get(id);

            // Assert
            Assert.Equals(expectedViewModel, result);
        }

        [Test]
        public async Task Get_MateriaDoesNotExist_ReturnsNull()
        {
            // Arrange
            var id = 1;
            _materiaRepositoryMock.Setup(repo => repo.Get(id)).ReturnsAsync((Materia)null);

            // Act
            var result = await _materiaService.Get(id);

            // Assert
            Assert.Equals(result, null);
        }

        [Test]
        public async Task GetAll_ReturnsListOfMateriaViewModel()
        {
            // Arrange
            var materias = new List<Materia> { new Materia(), new Materia() };
            var expectedViewModels = new List<MateriaViewModel> { new MateriaViewModel(), new MateriaViewModel() };
            _materiaRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(materias);
            foreach (var materia in materias)
            {
                MateriaViewModel.Map(materia);
            }

            // Act
            var result = await _materiaService.GetAll();

            // Assert
            Assert.Equals(expectedViewModels.Count, result.Count);
            for (int i = 0; i < expectedViewModels.Count; i++)
            {
                Assert.Equals(expectedViewModels[i], result[i]);
            }
        }

    }
}
