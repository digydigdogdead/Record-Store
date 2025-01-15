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

        [Test]
        public void PostAlbumReturnsModelEquivalent()
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
            Album emptyAlbum = new Album();
            
            modelMock.Setup(m => m.PostAlbum(newAlbum, out testFeedback)).Returns(newAlbum);

            var result = albumService.PostAlbum(newAlbum, out testFeedback);

            result.Should().BeEquivalentTo(newAlbum);
        }

        [Test]
        public void PostAlbumsReturnsEditedAlbumGenre() 
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

            Album expectedEditedAlbum = new Album
            {
                Id = 3,
                Artist = "Lady Gaga",
                Year = 2013,
                ParentGenre = ParentGenre.POP,
                Subgenre = "POP",
                Name = "ARTPOP"
            };
            
            modelMock.Setup(m => m.PostAlbum(newAlbum, out testFeedback)).Returns(expectedEditedAlbum);

            var result = albumService.PostAlbum(newAlbum, out testFeedback);

            result.Should().BeEquivalentTo(expectedEditedAlbum);
        }

        [Test]
        public void PostAlbumReturnsFalseWithBadAlbum()
        {
            string testFeedback = string.Empty;
            Album newAlbum = new Album();

            var result = albumService.PostAlbum(newAlbum, out testFeedback);

            result.Should().BeNull();
        }

        [Test]
        public void PostAlbumInvokesModelOnce()
        {
            string testFeedback = string.Empty;
            Album newAlbum = new Album
            {
                Id = 3,
                Artist = "Lady Gaga",
                Year = 2013,
                ParentGenre = ParentGenre.POP,
                Subgenre = "Hyperpop",
                Name = "ARTPOP"
            };

            modelMock.Setup(m => m.PostAlbum(newAlbum, out testFeedback)).Returns(newAlbum);

            albumService.PostAlbum(newAlbum, out testFeedback);

            modelMock.Verify(m => m.PostAlbum(newAlbum, out testFeedback), Times.Once);
        }

        [Test]
        public void UpdateAlbumInvokesModelOnce()
        {
            AlbumDTO updateTestData = new AlbumDTO { Id = 3, Artist = "Lady GaGa" };
            string testFeedback = string.Empty;
            Album updatedAlbum = new Album
            {
                Id = 3,
                Artist = "Lady GaGa",
                Year = 2013,
                ParentGenre = ParentGenre.POP,
                Subgenre = "Hyperpop",
                Name = "ARTPOP"
            };

            modelMock.Setup(m => m.UpdateAlbum(updateTestData, out testFeedback)).Returns(updatedAlbum);

            albumService.UpdateAlbum(updateTestData, out testFeedback);

            modelMock.Verify(m => m.UpdateAlbum(updateTestData, out testFeedback), Times.Once);
        }

        [Test]
        public void UpdateAlbumReturnsModelEquivalent()
        {
            AlbumDTO updateTestData = new AlbumDTO { Id = 3, Artist = "Lady GaGa" };
            string testFeedback = string.Empty;
            Album updatedAlbum = new Album
            {
                Id = 3,
                Artist = "Lady GaGa",
                Year = 2013,
                ParentGenre = ParentGenre.POP,
                Subgenre = "Hyperpop",
                Name = "ARTPOP"
            };

            modelMock.Setup(m => m.UpdateAlbum(updateTestData, out testFeedback)).Returns(updatedAlbum);

            var result = albumService.UpdateAlbum(updateTestData, out testFeedback);

            result.Should().BeEquivalentTo(updatedAlbum);
        }

        [Test]
        public void UpdateAlbumReturnsNull()
        {
            AlbumDTO updateTestData = new AlbumDTO { Id = 549, Artist = "Lady GaGa" };
            string testFeedback = string.Empty;
            

            modelMock.Setup(m => m.UpdateAlbum(updateTestData, out testFeedback));

            var result = albumService.UpdateAlbum(updateTestData, out testFeedback);

            result.Should().BeNull();
        }

        [Test]
        public void DeleteAlbumInvokesModelOnce()
        {
            string testFeedback = string.Empty;
            modelMock.Setup(m => m.TryDeleteAlbum(1, out testFeedback)).Returns(true);

            albumService.TryDeleteAlbum(1, out testFeedback);

            modelMock.Verify(m => m.TryDeleteAlbum(1, out testFeedback), Times.Once);

        }

        [Test]
        public void DeleteAlbumReturnsModelEquivalent()
        {
            string testFeedback = string.Empty;
            modelMock.Setup(m => m.TryDeleteAlbum(1, out testFeedback)).Returns(true);

            var result = albumService.TryDeleteAlbum(1, out testFeedback);

            result.Should().BeTrue();
        }

        [Test]
        public void GetAlbumsByArtistInvokesModelOnce()
        {
            modelMock.Setup(m => m.GetAlbumsByArtist("P!nk")).Returns(new List<Album>());

            albumService.GetAlbumsByArtist("P!nk");

            modelMock.Verify(m => m.GetAlbumsByArtist("P!nk"));
        }

        [Test]
        public void GetAlbumsByArtistReturnsModelEquivalent()
        {
            
            modelMock.Setup(m => m.GetAlbumsByArtist("P!nk")).Returns(testAlbums);

            var result = albumService.GetAlbumsByArtist("P!nk");

            result.Should().BeEquivalentTo(testAlbums);
        }
    }

}
