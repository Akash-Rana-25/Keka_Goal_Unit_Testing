using AutoFixture;
using AutoFixture.AutoMoq;

namespace xUnitTest
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
