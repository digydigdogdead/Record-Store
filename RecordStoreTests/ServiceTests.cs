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
        private Album testAlbum;

        [SetUp]
        public void Setup()
        {
            modelMock = new Mock<IAlbumModel>();
            albumService = new AlbumService(modelMock.Object);
            testAlbums = new List<Album>
            {
                new Album { Id = 1, Artist = "Sleep Token", Name = "Take Me Back To Eden", Year = 2023, ParentGenre = ParentGenre.METAL },
                new Album { Id = 2, Artist = "Various Artists", Name = "Madagascar: Escape 2 Africa", Year = 2008, ParentGenre = ParentGenre.CLASSICAL }
            };
            testAlbum = new Album { Id = 1, Artist = "Sleep Token", Name = "Take Me Back To Eden", Year = 2023, ParentGenre = ParentGenre.METAL };
        }

        [Test]
        public void GetAllAlbumsInvokesModelOnce()
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

        [Test]
        public void GetAlbumByIdInvokesModelOnce()
        {
            modelMock.Setup(m => m.GetAlbumById(1)).Returns(testAlbum);

            albumService.GetAlbumById(1);

            modelMock.Verify(m => m.GetAlbumById(1), Times.Once);
        }

        [Test]
        public void GetAlbumByIdReturnsModelEquivalent()
        {
            modelMock.Setup(m => m.GetAlbumById(1)).Returns(testAlbum);
            var result = albumService.GetAlbumById(1);

            result.Should().BeEquivalentTo(testAlbum);
        }
    }
}
