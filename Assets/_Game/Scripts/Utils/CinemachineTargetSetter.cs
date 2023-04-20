using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineTargetSetter : MonoBehaviour
{
    private void Awake()
    {
        var cam = GetComponent<CinemachineVirtualCamera>();
        var target = ServiceLocator.Get<Player>();
        cam.Follow = target.transform;
    }
}
