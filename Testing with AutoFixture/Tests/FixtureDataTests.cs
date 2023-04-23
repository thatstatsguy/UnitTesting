using AutoFixture;
using AutoFixture.Xunit2;
using Xunit;

namespace TestAutoFixture.Tests;

public class FixtureDataTests
{
    private int Add(int a, int b, string validationError)
    {
        if (a < 0 || b < 0)
        {
            throw new Exception(validationError);
        }
        else
        {
            return a + b;
        }
    }

    [Fact(DisplayName = "Test Addition without AutoFixture")]
    public void TestAddition1()
    {
        //arrange
        var input1 = 1;
        var input2 = 2;
        var expectedResult = input1 + input2;
        //act
        var actual = Add(input1, input2, "");
        //assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact(DisplayName = "Test Addition with AutoFixture")]
    public void TestAddition2()
    {
        //arrange
        var input1 = 1;
        var input2 = 2;
        var expectedResult = input1 + input2;
        var fixture = new Fixture();

        //act
        var actual = Add(input1, input2, fixture.Create<string>());
        //assert
        Assert.Equal(expectedResult, actual);
    }

    [Theory(DisplayName = "Test Addition with AutoFixture AutoData Part 1")]
    [AutoData]
    public void TestAddition3(string validationError)
    {
        //arrange
        var input1 = 1;
        var input2 = 2;
        var expectedResult = input1 + input2;

        //act
        var actual = Add(input1, input2, validationError);
        //assert
        Assert.Equal(expectedResult, actual);
    }

    [Theory(DisplayName = "Test Addition with AutoFixture AutoData Part 2")]
    [AutoData]
    public void TestAddition4(int input1, int input2, string validationError)
    {
        //arrange
        var expectedResult = input1 + input2;

        //act
        var actual = Add(input1, input2, validationError);
        //assert
        Assert.Equal(expectedResult, actual);
    }
}