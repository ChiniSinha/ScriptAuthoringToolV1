using System.Collections.Generic;

public class Script
{
    private State _currentState;

    private string _nextStateName;
    public List<State> States;

    public State GetStartingState()
    {
        return States[0];
    }

    public State GetState(string stateName)
    {
        foreach (State state in States)
        {
            if (state.StateName == stateName)
            {
                return state;
            }
        }
        return null;
    }

    public State GetCurrentState()
    {
        return _currentState;
    }

    public State GetNextState()
    {
        return GetState(_nextStateName);
    }

    public void SetNextState(string nextStateName)
    {
        _nextStateName = nextStateName;
    }
}