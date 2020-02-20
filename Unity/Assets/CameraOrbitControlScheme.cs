using System;
using UnityEngine;



//Camera Control Scheme - Orbit Camera
//Controls given target GameObject, and makes it rotate around some pivot
public class CameraOrbitControlScheme : ICameraControlScheme
{
    private Transform _targetCamera;
    private Transform _pivotObject;
    //Orbit size = orbit radius
    private float _orbitSize = 1f;
    //Orbit position, in radians
    private float _orbitPosition = 0f;
    private Vector3 _pivotOffset = Vector3.zero;
    private float _cameraSpeed = 0.1f;
    private Action _invalidStateAction; 

    public CameraOrbitControlScheme(Transform target, Transform pivotObject, Action invalidStateAction)
    {
        _targetCamera = target;
        _pivotObject = pivotObject;
        _invalidStateAction = invalidStateAction;
    }

    public void OnInvalidState()
    {
        _invalidStateAction();
    }

    public void UpdateControl()
    {
        if(_targetCamera == null || _pivotObject == null)
            OnInvalidState();
        //AD -> Moves left/right around the circle
        //WS -> Changes orbit radius
        //RF -> Changes camera height
        _orbitSize += Input.GetAxis("Forward");
        _orbitPosition += Input.GetAxis("Right");
        _pivotOffset.z += Input.GetAxis("Up");

        //Calculate new position
        Vector3 pos = new Vector3(Mathf.Cos(_orbitPosition) * _orbitSize, Mathf.Sin(_orbitPosition) * _orbitSize, 0)
         + _pivotOffset + _pivotObject.position;

        //Make camera look at pivot object
        _targetCamera.LookAt(_pivotObject);
        
    }
}