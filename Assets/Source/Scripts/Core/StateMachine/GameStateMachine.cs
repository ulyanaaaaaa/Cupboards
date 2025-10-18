using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IState> _states = new();
    private IState _activeState;

    public void Register<T>(T state) where T : IState => _states[typeof(T)] = state;

    public void Enter<T>() where T : IState
    {
        _activeState?.Exit();
        _activeState = _states[typeof(T)];
        _activeState.Enter();
    }

    public void Tick() => _activeState?.Tick();

    public T Get<T>() where T : class, IState => _states[typeof(T)] as T;
}