using System;
using UnityEngine;


public class CameraFreeControlScheme : ICameraControlScheme
{
    private GameObject _target;

    public CameraFreeControlScheme(GameObject target)
    {
        _target = target;
    }

    
    public void UpdateControl()
    {
        throw new System.NotImplementedException();
    }
}