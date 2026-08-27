namespace TicTakToe;

public interface IComponent
{
    public IMediator Mediator { get; }
}

public interface IMediator
{
    public void Notify(IComponent Component, string Event);
}