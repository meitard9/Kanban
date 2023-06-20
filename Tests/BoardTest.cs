using NUnit.Framework;
using Moq;
using IntroSE.Kanban.Backend.BusinessLayer.BoardPackage;


namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Board b = new Board("yarinBoard", "yarinperetz@gmail.com");

        }

        [Test]
        public void GetColumn_Existing_Column_Success()
        {

            Assert.Pass();
        }
    }
}