using System;
using UnityEngine;


//Camera Control Scheme - Free Camera
//Controls given target GameObject
public class CameraFreeControlScheme : ICameraControlScheme
{
    private Transform _targetCamera;
    private Vector3 _currRot = new Vector3(0, 0, 0);
    private float _rotSpeed = 100f;
    public CameraFreeControlScheme(Transform target)
    {
        _targetCamera = target;
    }

    public void OnInvalidState()
    {
        throw new NotImplementedException();
    }

    public void UpdateControl()
    {
         //FREE CAMERA
        _targetCamera.Translate(Vector3.forward * Input.GetAxis("Forward"));
        _targetCamera.Translate(Vector3.up * Input.GetAxis("Up"));
        _targetCamera.Translate(Vector3.right * Input.GetAxis("Right"));

        Quaternion rot = Quaternion.identity;
        _currRot += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f) * Time.deltaTime * _rotSpeed;
        rot.eulerAngles = _currRot;
        _targetCamera.rotation = rot;
    }
}