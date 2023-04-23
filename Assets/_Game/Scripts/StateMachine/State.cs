using UnityEngine;

public class State<T>
{
    protected T Owner {get; private set;}

    public State(T owner)
    {
        Owner = owner;
    }

    // automatically gets called in the State machine. Allows you to delay flow if desired
    public virtual void Enter() { }
    // allows simulation of Update() method without a MonoBehaviour attached
    public virtual void Tick() { }
    // allows simulatin of FixedUpdate() method without a MonoBehaviour attached
    public virtual void FixedTick() { }
    // automatically gets called in the State machine. Allows you to delay flow if desired
    public virtual void Exit() { }
}