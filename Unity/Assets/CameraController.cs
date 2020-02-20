using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform _target;
    public Transform target
    {
        get { return _target; }
        set
        {
            _target = value;

            if (_target == null) //if nothing was selected use manual controls
            {
                _currentControlScheme = new CameraFreeControlScheme(transform);
                Debug.Log("Free Control Scheme");
            }
            else // if something was selected use targeting control schemes
            {
                _currentControlScheme = new CameraOrbitControlScheme(transform, _target, HandleTargetNull);
                Debug.Log("Orbital control scheme");
            }

        }
    }
    public Vector3 offset;

    private Vector3 _previousMousePos;
    private ICameraControlScheme _currentControlScheme;

    // Start is called before the first frame update
    void Start()
    {
        _previousMousePos = Input.mousePosition;
        Cursor.lockState = CursorLockMode.Locked;
        _currentControlScheme = new CameraFreeControlScheme(transform);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("ToggleCursor"))
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        _currentControlScheme.UpdateControl();


        if (Input.GetButtonDown("BreakTarget"))
            target = null;

        if (Input.GetButtonDown("BestTarget"))
            target = ChooseBestTarget();

        //Check for selection
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100.0f, 1 << Agent.LayerOrder))
            {
                if (hit.collider.gameObject.layer == Agent.LayerOrder)
                {
                    target = hit.collider.transform;
                }
            }
        }
        
        _previousMousePos = Input.mousePosition;
    }


    private void HandleTargetNull()
    {
        //When target is being destroyed _target is being set to null (not the property!) so we need to
        //Trigger property change
        target = null;
    }


    private Transform ChooseBestTarget()
    {
        throw new NotImplementedException();
    }

}
