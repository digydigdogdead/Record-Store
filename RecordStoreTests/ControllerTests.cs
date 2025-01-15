using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Record_Store.Services;
using Record_Store.Controllers;
using Record_Store;
using Record_Store.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using FluentAssertions;
using System.Runtime.Intrinsics.X86;

namespace RecordStoreTests
{
    internal class ControllerTests
    {
        private Mock<IAlbumService> serviceMock;
        private AlbumController controller;
        private List<Album> testAlbums;
        private Album testAlbum;
        [SetUp]
        public void Setup()
        {
            serviceMock = new Mock<IAlbumService>();
            controller = new AlbumController(serviceMock.Object);
            testAlbums = new List<Album>
            {
                new Album { Id = 1, Artist = "Sleep Token", Name = "Take Me Back To Eden", Year = 2023, ParentGenre = ParentGenre.METAL },
                new Album { Id = 2, Artist = "Various Artists", Name = "Madagascar: Escape 2 Africa", Year = 2008, ParentGenre = ParentGenre.METAL }
            };
            testAlbum = new Album { Id = 1, Artist = "Sleep Token", Name = "Take Me Back To Eden", Year = 2023, ParentGenre = ParentGenre.METAL };
        }

        [TearDown]
        public void TearDown()
        {
            controller.Dispose();
        }

        [Test]
        public void GetAllAlbumsInvokesServiceOnce()
        {
            serviceMock.Setup(s => s.GetAllAlbums()).Returns(testAlbums);

            controller.GetAllAlbums();

            serviceMock.Verify(s => s.GetAllAlbums(), Times.Once);
        }

        [Test]
        public void GetAllAlbumsReturnsOk()
        {
            serviceMock.Setup(s => s.GetAllAlbums()).Returns(testAlbums);
            var expected = controller.Ok(testAlbums);

            var result = controller.GetAllAlbums();

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetAllAlbumsReturnsNoContent()
        {
            var emptyList = new List<Album>();
            serviceMock.Setup(s => s.GetAllAlbums()).Returns(emptyList);
            var expected = controller.NoContent();

            var result = controller.GetAllAlbums();

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetAlbumByIdInvokesServiceOnce()
        {
            serviceMock.Setup(s => s.GetAlbumById(1)).Returns(testAlbum);
            controller.GetAlbumById(1);

            serviceMock.Verify(s => s.GetAlbumById(1), Times.Once);
        }

        [Test]
        public void GetAlbumByIdReturnsOk()
        {
            serviceMock.Setup(s => s.GetAlbumById(1)).Returns(testAlbum);
            var expected = controller.Ok(testAlbum);

            var result = controller.GetAlbumById(1);
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetAlbumByIdReturnsNotFound()
        {
            serviceMock.Setup(s => s.GetAlbumById(40));
            var expected = controller.NotFound();

            var result = controller.GetAlbumById(40);
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void PostAlbumReturnsBadRequest()
        {
            string testFeedback = string.Empty;
            Album newAlbum = new Album();

            serviceMock.Setup(s => s.PostAlbum(newAlbum, out testFeedback));
            var expected = controller.BadRequest();

            var result = controller.PostAlbum(newAlbum);

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void PostAlbumReturnsOk()
        {
            string testFeedback = string.Empty;
            Album newAlbum = new Album
            {
                Id = 3,
                Artist = "Lady Gaga",
                Year = 2013,
                ParentGenre = ParentGenre.POP,
                Name = "ARTPOP"
            };

            serviceMock.Setup(s => s.PostAlbum(newAlbum, out testFeedback)).Returns(newAlbum);
            var expected = controller.Ok(newAlbum);

            var result = controller.PostAlbum(newAlbum);
        }

        [Test]
        public void PostAlbumInvokesServiceOnce()
        {
            string testFeedback = string.Empty;
            Album newAlbum = new Album
            {
                Id = 3,
                Artist = "Lady Gaga",
                Year = 2013,
                ParentGenre = ParentGenre.POP,
                Name = "ARTPOP"
            };

            serviceMock.Setup(s => s.PostAlbum(newAlbum, out testFeedback)).Returns(newAlbum);

            controller.PostAlbum(newAlbum);

            serviceMock.Verify(s => s.PostAlbum(newAlbum, out testFeedback), Times.Once);
        }

        [Test]
        public void PatchAlbumInvokesServiceOnce()
        {
            AlbumDTO testDTO = new AlbumDTO { Id = 1, Subgenre = "Pop-Metal" };
            string testFeedback = string.Empty;

            serviceMock.Setup(s => s.UpdateAlbum(testDTO, out testFeedback));

            controller.PatchAlbum(1, testDTO);

            serviceMock.Verify(s => s.UpdateAlbum(testDTO, out testFeedback), Times.Once);
        }

        [Test]
        public void PatchAlbumReturnsNotFound()
        {
            AlbumDTO testDTO = new AlbumDTO { Id = 1000, Subgenre = "Pop-Metal" };
            string testFeedback = "Album not found.";

            var expected = controller.NotFound();
            serviceMock.Setup(s => s.UpdateAlbum(testDTO, out testFeedback));
            
            var result = controller.PatchAlbum(1, testDTO);

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void PatchAlbumReturnsOkWithUpdatedAlbum()
        {
            AlbumDTO testDTO = new AlbumDTO { Id = 1, Subgenre = "Pop-Metal" };
            Album updatedAlbum = new Album
            {
                Id = 1,
                Artist = "Sleep Token",
                Name = "Take Me Back To Eden",
                Year = 2023,
                ParentGenre = ParentGenre.METAL,
                Subgenre = "Pop-Metal"
            };
            string testFeedback = string.Empty;

            serviceMock.Setup(s => s.UpdateAlbum(testDTO, out testFeedback)).Returns(updatedAlbum);
            var expected = controller.Ok(updatedAlbum);

            var result = controller.PatchAlbum(1, testDTO);

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void PatchAlbumReturnsBadRequest()
        {
            AlbumDTO testDTO = new AlbumDTO { Id = 1, Subgenre = "Pop-Metal" };
            string testFeedback = "jdfisj";

            serviceMock.Setup(s => s.UpdateAlbum(testDTO, out testFeedback));
            var expected = controller.BadRequest(testFeedback);

            var result = controller.PatchAlbum(1, testDTO);

            result.Should().BeEquivalentTo(expected);

        }

        [Test]
        public void DeleteAlbumInvokesServiceOnce()
        {
            string testFeedback = string.Empty;

            serviceMock.Setup(s => s.TryDeleteAlbum(1, out testFeedback)).Returns(false);

            controller.DeleteAlbum(1);

            serviceMock.Verify(s => s.TryDeleteAlbum(1, out testFeedback), Times.Once);
        }

        [Test]
        public void DeleteAlbumReturnsBadRequest() 
        {
            string testFeedback = string.Empty;

            serviceMock.Setup(s => s.TryDeleteAlbum(1, out testFeedback)).Returns(false);
            var expected = controller.BadRequest(testFeedback);

            var result = controller.DeleteAlbum(1);

            result.Should().BeEquivalentTo(expected);

        }

        [Test]
        public void DeleteAlbumReturnsNotFound()
        {
            string testFeedback = "Album not found.";
            
            serviceMock.Setup(s => s.TryDeleteAlbum(1, out testFeedback)).Returns(false);
            var expected = controller.NotFound();

            var result = controller.DeleteAlbum(1);

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void DeleteAlbumReturnsNoContent()
        {
            string testFeedback = "Album not found.";

            serviceMock.Setup(s => s.TryDeleteAlbum(1, out testFeedback)).Returns(true);
            var expected = controller.NoContent();

            var result = controller.DeleteAlbum(1);

            result.Should().BeEquivalentTo(expected);

        }
    }
}
