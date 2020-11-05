using AuthorizationAPI.Model;
using AuthorizationAPI.Provider;
using AuthorizationAPI.Repository;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AuthTest
{
    public class Tests
    {
        List<PensionCredentials> user = new List<PensionCredentials>();
        IQueryable<PensionCredentials> pensionerdata;
        Mock<List<PensionCredentials>> mockSet;
        Mock<IPensionProvider> pensionprovidermock;
        Mock<IPensionRepo> pensionrepomock;
        IPensionProvider provider = new PensionProvider();
        [SetUp]
        public void Setup()
        {
            user = new List<PensionCredentials>()
            {
                new PensionCredentials{Username = "user1",Password = "user1"}
            };
            pensionerdata = user.AsQueryable();
            mockSet = new Mock<List<PensionCredentials>>();
            mockSet.As<IQueryable<PensionCredentials>>().Setup(m => m.Provider).Returns(pensionerdata.Provider);
            mockSet.As<IQueryable<PensionCredentials>>().Setup(m => m.Expression).Returns(pensionerdata.Expression);
            mockSet.As<IQueryable<PensionCredentials>>().Setup(m => m.ElementType).Returns(pensionerdata.ElementType);
            mockSet.As<IQueryable<PensionCredentials>>().Setup(m => m.GetEnumerator()).Returns(pensionerdata.GetEnumerator());

            //pensionprovidermock = new Mock<IPensionProvider>();
            //pensionprovidermock.Setup(x => x.list).Returns(mockSet.Object);
           // pensionrepomock = new Mock<IPensionRepo>(provider);
        }

        [Test]
        public void GetCredentialsProvider()
        {
            PensionCredentials cred = new PensionCredentials { Username = "user1", Password = "user1" };
            var pensionProvider = new PensionProvider();
            var penCred = pensionProvider.GetPensioner(cred);

            Assert.IsNotNull(penCred);
        }

        [Test]
        public void GetCredentialsProviderFail()
        {

            PensionCredentials cred = new PensionCredentials { Username = "1", Password = "abc1" };
            var pensionProvider = new PensionProvider();
            var penCred = pensionProvider.GetPensioner(cred);

            Assert.IsNull(penCred);

        }
    }
}