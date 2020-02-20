using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookieJarEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private float _rotSpeed = 0.1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(_rotSpeed, _rotSpeed, 0), Space.Self);
    }
}
