using System;
using System.Collections.Generic;

public class StateMachine<TState>
{
    private TState _currentState;
    private readonly Dictionary<TState, Action> _stateEnterActions = new();
    private readonly Dictionary<TState, Action> _stateExitActions = new();
    private readonly Dictionary<(TState, TState), Func<bool>> _transitions = new();

    public TState CurrentState => _currentState;

    public void AddState(TState state, Action onEnter = null, Action onExit = null)
    {
        if (!_stateEnterActions.ContainsKey(state))
        {
            _stateEnterActions[state] = onEnter;
            _stateExitActions[state] = onExit;
        }
    }

    public void AddTransition(TState from, TState to, Func<bool> condition)
    {
        _transitions[(from, to)] = condition;
    }

    public void SetState(TState newState)
    {
        if (_currentState.Equals(newState)) return;

        if (_stateExitActions.TryGetValue(_currentState, out var onExit))
            onExit?.Invoke();

        _currentState = newState;

        if (_stateEnterActions.TryGetValue(newState, out var onEnter))
            onEnter?.Invoke();
    }

    public void Update()
    {
        foreach (var transition in _transitions)
        {
            if (transition.Key.Item1.Equals(_currentState) && transition.Value.Invoke())
            {
                SetState(transition.Key.Item2);
                break;
            }
        }
    }
}