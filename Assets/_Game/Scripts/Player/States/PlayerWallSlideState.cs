using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : State
{
    Player _player;

    public PlayerWallSlideState(Player player)
    {
        _player = player;
    }

    bool CheckWall()
    {
        if(Physics2D.Raycast(_player.WallCheckRayOrigin.position, _player.transform.right, _player.WallCheckRayLegth, _player.WallLayer))
        {
            return true;
        }

        return false;
    }
}
