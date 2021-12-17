﻿namespace NAPS2.Threading;

/// <summary>
/// An interface for synchronized access to the UI thread.
/// </summary>
public interface IInvoker
{
    void Invoke(Action action);

    void SafeInvoke(Action action);

    T InvokeGet<T>(Func<T> func);
}