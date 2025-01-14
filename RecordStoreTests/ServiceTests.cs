using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Record_Store;
using Record_Store.Models;
using Moq;
using Record_Store.Services;
using FluentAssertions;

namespace RecordStoreTests
{
    internal class ServiceTests
    {
        private Mock<IAlbumModel> modelMock;
        private AlbumService albumService;
        private List<Album> testAlbums;

        [SetUp]
        public void Setup()
        {
            modelMock = new Mock<IAlbumModel>();
            albumService = new AlbumService(modelMock.Object);
            testAlbums = new List<Album>
            {
                new Album { Id = 1, Artist = "Sleep Token", Name = "Take Me Back To Eden", Year = 2023 },
                new Album { Id = 2, Artist = "Various Artists", Name = "Madagascar: Escape 2 Africa", Year = 2008 }
            };
        }

        [Test]
        public void GetAllAlbumsInvokesRepoOnce()
        {
            modelMock.Setup(m => m.GetAllAlbums());
            albumService.GetAllAlbums();

            modelMock.Verify(m => m.GetAllAlbums(), Times.Once);
        }

        [Test]
        public void GetAllAlbumsReturnsModelEquivalent()
        {
            modelMock.Setup(m => m.GetAllAlbums()).Returns(testAlbums);
            var result = albumService.GetAllAlbums();

            result.Should().BeEquivalentTo(testAlbums);
        }
    }
}
