using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;

    private Vector3 previousMousePos;
    private Vector3 currRot = new Vector3(0, 0, 0);
    private float _rotSpeed = 100f;
    private ICameraControlScheme[] _controlSchemes;
    private ICameraControlScheme _currentControlScheme;

    // Start is called before the first frame update
    void Start()
    {
        previousMousePos = Input.mousePosition;
        Cursor.lockState = CursorLockMode.Locked;
        _controlSchemes[0] = new CameraFreeControlScheme(gameObject);
        _controlSchemes[1] = new CameraOrbitControlScheme(gameObject);
        _currentControlScheme = _controlSchemes[0];
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

        if (target == null) //if nothing was selected use manual controls
        {
            //FREE CAMERA
            transform.Translate(Vector3.forward * Input.GetAxis("Forward"));
            transform.Translate(Vector3.up * Input.GetAxis("Up"));
            transform.Translate(Vector3.right * Input.GetAxis("Right"));

            Quaternion rot = Quaternion.identity;
            currRot += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f) * Time.deltaTime * _rotSpeed;
            rot.eulerAngles = currRot;
            transform.rotation = rot;
        }
        else // if something was selected use targeting control schemes
        {
            transform.position = target.position - offset;
            //ORBIT CAMERA
            Vector3 pivot = target.position;

            //AD -> go around the orbit
            //WS -> Change orbit radius
            //RF -> Change orbit inclination





        }





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


        previousMousePos = Input.mousePosition;
    }






    Transform ChooseBestTarget()
    {
        throw new NotImplementedException();
    }

}
