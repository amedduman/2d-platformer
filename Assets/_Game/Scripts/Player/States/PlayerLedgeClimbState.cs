using UnityEngine;

public class PlayerLedgeClimbState : State<Player>
{
    public PlayerLedgeClimbState(Player owner) : base(owner) {
    }

    public override void Enter()
    {
        Debug.Log("entered ledge State");
    }
}