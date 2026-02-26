namespace MyApplication;

public record Person(string Name, int Age)
{
    public override string ToString() => $"My name is {Name} and I am {Age}";
}