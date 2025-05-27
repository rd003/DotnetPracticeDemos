namespace MiddleWareDemo;

public interface ISomeService
{
    void SomeMethod();
}

public class SomeService : ISomeService
{
    public void SomeMethod()
    {
        Console.WriteLine("Some method of SomeService");
    }
}