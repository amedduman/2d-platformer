using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpStartState : State<Player>
{
    public PlayerJumpStartState(Player sm) : base(sm) {
    }

    float _minJumpDuration = .15f;
    float _currentJumpDuration = 0;
    bool _canExit = false;
    public override void Enter()
    {
        _currentJumpDuration = 0;
        _canExit = false;
        
        Owner.PlayAnimation("jump_start");
        Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, Owner.JumpSpeed);
        // Owner.Rb.AddForce(new Vector2(0, Owner.JumpSpeed* 2), ForceMode2D.Impulse);
    }

    public override void Tick()
    {
        if (_currentJumpDuration > _minJumpDuration)
        {
            _canExit = true;
        }

        _currentJumpDuration += Time.deltaTime;
    }

    public override void FixedTick()
    {
        // don't let player press jump button really quick and end up with a really small jump.
        if (_canExit == false)
        {
            // return;
        }
        
        if (Owner.CheckGround() == false)
        {
            Owner.MovementStateMachine.ChangeState(Owner.JumpLaunchingState);
        }
    }
    
    // bool CheckGround()
    // {
    //     if (Physics2D.OverlapBox(Owner.GroundCheckRayOrigin.position,
    //             new Vector2(.1f, 4), 0, Owner.JumpableLayers) != null)
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }
}
