using Example.CoarseGrainedUTest.Core;
using Example.CoarseGrainedUTest.DataAccess;
using Example.CoarseGrainedUTest.Messages;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Example.CoarseGrainedUTest.Tests
{
    public class MovieServiceTests
    {
        private const string Cinema1 = "Cinema1";
        private const string Cinema2 = "Cinema2";

        private const string Movie1 = "Movie1";
        private const string Movie2 = "Movie2";

        [Test]
        public async Task WhenCheckOccures_ThenMessagesAreSent()
        {
            Mock<IFileReader> fileReader = CreateFileReader();
            var queueClient = new Mock<IQueueClient>();

            var movieRepository = new MovieRepository(fileReader.Object);
            var movieService = new MovieService(movieRepository, queueClient.Object);

            await movieService.CheckForChangesAsync();

            queueClient.Verify(p => p.PublishMessageAsync(It.Is<MovieAddedMessage>(x => x.CinemaName == Cinema1 && x.MovieName == Movie1)));
            queueClient.Verify(p => p.PublishMessageAsync(It.Is<MovieAddedMessage>(x => x.CinemaName == Cinema2 && x.MovieName == Movie2)));
        }

        private static Mock<IFileReader> CreateFileReader()
        {
            const string ExpectedCinemasFolder = @"C:\Cineams\";
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(p => p.GetFileDataAsync($"{ExpectedCinemasFolder}{Cinema1}")).ReturnsAsync(new[] {
               $"1;Director1;{Movie1}"
            });
            fileReader.Setup(p => p.GetFileDataAsync($"{ExpectedCinemasFolder}{Cinema2}")).ReturnsAsync(new[] {
                $"2;Director2;{Movie2}"
            });
            return fileReader;
        }
    }
}
