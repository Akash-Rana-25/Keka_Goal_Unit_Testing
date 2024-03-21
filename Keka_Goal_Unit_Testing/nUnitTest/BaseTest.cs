using AutoFixture;
using AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nUnitTest
{
    public class BaseTest
    {
        protected readonly IFixture _fixture;

        public BaseTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }
    }
}
