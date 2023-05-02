using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerJumpPeakPauseState : State<Player>
{
    public PlayerJumpPeakPauseState(Player owner) : base(owner) {
    }

    float _originalGravityY;
    Tween _jumpPeakTimer;

    public override void Enter()
    {
        _originalGravityY = Physics2D.gravity.y;

        Physics2D.gravity = new Vector2(Physics.gravity.x, 0);
        _jumpPeakTimer = DOVirtual.DelayedCall(.12f, () =>
        {
            Physics2D.gravity = new Vector2(Physics.gravity.x, _originalGravityY);
            
            Owner.MovementStateMachine.ChangeState(Owner.FallState);
        });
    }

    public override void Tick()
    {
        if (Owner.IsJumpButtonPressed())
        {
            Owner.MovementStateMachine.ChangeState(Owner.DoubleJumpState);
            return;
        }
    }

    public override void FixedTick()
    {
        Owner.MoveHorizontally();
    }

    public override void Exit()
    {
        _jumpPeakTimer.Kill();
        Physics2D.gravity = new Vector2(Physics.gravity.x, _originalGravityY);
    }
}
