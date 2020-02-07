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
    private float rotSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        previousMousePos = Input.mousePosition;
        Cursor.lockState = CursorLockMode.Locked;
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


        if (Input.GetButtonDown("BreakTarget"))
            target = null;

        if (Input.GetButtonDown("BestTarget"))
            target = ChooseBestTarget();


        if (target == null)
        {
            transform.Translate(Vector3.forward * Input.GetAxis("Forward"));
            transform.Translate(Vector3.up * Input.GetAxis("Up"));
            transform.Translate(Vector3.right * Input.GetAxis("Right"));

            Quaternion rot = Quaternion.identity;
            currRot += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f) * Time.deltaTime * rotSpeed;
            rot.eulerAngles = currRot;
            transform.rotation = rot;
        }

        previousMousePos = Input.mousePosition;

        if (target != null)
            transform.position = target.position - offset;
        //Check for input

    }




    Transform ChooseBestTarget()
    {
        throw new NotImplementedException();
    }

}
