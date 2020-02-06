using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;

    private Vector3 mouseDelta = new Vector2(0f, 0f);
    private Vector3 previousMousePos;

    // Start is called before the first frame update
    void Start()
    {
        previousMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("BreakTarget"))
            target = null;

        if (Input.GetButton("BestTarget"))
            target = ChooseBestTarget();


        if (target == null)
        {
            transform.Translate(Vector3.forward * Input.GetAxis("Forward"));
            transform.Translate(Vector3.up * Input.GetAxis("Up"));
            transform.Translate(Vector3.right * Input.GetAxis("Right"));
            mouseDelta = Input.mousePosition - previousMousePos;

            transform.rotation *= Quaternion.AngleAxis(mouseDelta.x, Vector3.up);
            transform.rotation *= Quaternion.AngleAxis(mouseDelta.y, Vector3.forward);            
            
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
