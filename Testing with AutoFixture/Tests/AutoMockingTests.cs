using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;
using Xunit;


namespace TestAutoFixture.Tests;

public class AutoMockingTests
{
    [Fact(DisplayName = "Test Do Something without AutoFixture")]
    public void Baseline()
    {
        //arrange
        var configurationMock = new Mock<IConfiguration>();
        var fooMock = new Mock<IFoo>();
        configurationMock
            .Setup(x => x[It.IsAny<string>()])
            .Returns("1");
        var service = new MyService(configurationMock.Object, fooMock.Object);

        //act
        var validationConfiguration = service.CheckValidationConfiguration();

        //assert
        Assert.True(validationConfiguration);
    }

    [Fact(DisplayName = "Test Do Something with Manually Created Fixture")]
    public void ManualAutoMoq()
    {
        //arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var configurationMock = fixture.Freeze<Mock<IConfiguration>>();
        configurationMock
            .Setup(x => x[It.IsAny<string>()])
            .Returns("1");
        var sut = fixture.Create<MyService>();
        
        //act
        var validationConfiguration = sut.CheckValidationConfiguration();

        Assert.True(validationConfiguration);
    }

    [Theory]
    [AutoMoqData]
    public void AutoMoqTest(
        [Frozen] Mock<IConfiguration> configurationMock,
        MyService service)
    {
        //arrange
        configurationMock
            .Setup(x => x[It.IsAny<string>()])
            .Returns("1");
        
        //act
        var validationConfiguration = service.CheckValidationConfiguration();
        //assert
        Assert.True(validationConfiguration);
    }
}