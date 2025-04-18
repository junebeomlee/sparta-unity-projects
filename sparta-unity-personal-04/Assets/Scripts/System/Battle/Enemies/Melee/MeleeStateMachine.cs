using System.Collections.Generic;

public class MeleeEnemyStateMachine : StateMachine
{
    public override List<State> states { get; } = new() { State.Idle, State.Attacking };

    void Start()
    { 
        states[1].ToString();
    }
}