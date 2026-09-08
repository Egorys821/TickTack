using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Receives change notifications from an <see cref="ISubject"/> it has been attached to.
/// </summary>
public interface IObserver
{
    /// <summary>
    /// Called by <paramref name="subject"/> when its state has changed, so the observer
    /// can refresh itself (e.g. redraw UI to match the new game state).
    /// </summary>
    /// <param name="subject">The subject that changed.</param>
    void Update(ISubject subject);
}

/// <summary>
/// An object that keeps a list of <see cref="IObserver"/>s and notifies them whenever
/// its own state changes, without needing to know what those observers do.
/// </summary>
public interface ISubject
{
    /// <summary>Registers <paramref name="observer"/> so it starts receiving <see cref="Notify"/> updates.</summary>
    /// <param name="observer">The observer to attach.</param>
    void Attach(IObserver observer);

    /// <summary>Unregisters <paramref name="observer"/> so it stops receiving <see cref="Notify"/> updates.</summary>
    /// <param name="observer">The observer to detach.</param>
    void Detach(IObserver observer);

    /// <summary>Calls <see cref="IObserver.Update"/> on every currently attached observer.</summary>
    void Notify();
}


