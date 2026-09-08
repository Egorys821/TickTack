namespace TicTakToe;

/// <summary>
/// A UI element that reports its interactions to a central <see cref="IMediator"/>
/// instead of talking to other components directly.
/// </summary>
public interface IComponent
{
    /// <summary>The mediator this component reports events to.</summary>
    public IMediator Mediator { get; }
}

/// <summary>
/// Coordinates interactions between <see cref="IComponent"/> instances so that they
/// do not need direct references to one another.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Informs the mediator that <paramref name="Component"/> raised <paramref name="Event"/>,
    /// so the mediator can react accordingly (e.g. by advancing the game).
    /// </summary>
    /// <param name="Component">The component that raised the event.</param>
    /// <param name="Event">A short, mediator-specific identifier for what happened (e.g. <c>"click"</c>).</param>
    public void Notify(IComponent Component, string Event);
}