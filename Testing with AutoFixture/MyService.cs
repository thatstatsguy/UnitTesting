namespace TestAutoFixture;

public interface IFoo
{
    public void Bar();
}

public class MyService
{
    private readonly IConfiguration _configuration;
    private readonly IFoo _foo;

    public MyService(IConfiguration configuration, IFoo foo)
    {
        _configuration = configuration;
        _foo = foo;
    }

    //checks if our configuration is correctly setup
    public bool CheckValidationConfiguration()
    {
        var configValue =
            _configuration["ConfigElement"] ?? throw new Exception("Config element not specified!");
        
        return int.TryParse(configValue, out _);;
    }

    //used to represent a second method which requires a dependency on a different interface
    public void TestMethod2()
    {
        _foo.Bar();
    }
}