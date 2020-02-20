using System;
using UnityEngine;


public class CameraOrbitControlScheme : ICameraControlScheme
{
    private GameObject _target;

    public CameraOrbitControlScheme(GameObject target)
    {
        _target = target;
    }

    
    public void UpdateControl()
    {
        throw new System.NotImplementedException();
    }
}