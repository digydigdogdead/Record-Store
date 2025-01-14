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

namespace RecordStoreTests
{
    internal class ControllerTests
    {
        private Mock<IAlbumService> serviceMock;
        private AlbumController controller;
        private List<Album> testAlbums;
        [SetUp]
        public void Setup()
        {
            serviceMock = new Mock<IAlbumService>();
            controller = new AlbumController(serviceMock.Object);
            testAlbums = new List<Album>
            {
                new Album { Id = 1, Artist = "Sleep Token", Name = "Take Me Back To Eden", Year = 2023 },
                new Album { Id = 2, Artist = "Various Artists", Name = "Madagascar: Escape 2 Africa", Year = 2008 }
            };
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
    }
}
