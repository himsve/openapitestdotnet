using NUnit.Framework;
using OpenApi;

namespace openapinunit
{
    public class OpenApiTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {  
            Assert.AreEqual(1, 2);         
        }

        [Test]
        public void StressTest1()
        {
            Assert.Pass();
        }
    }
}