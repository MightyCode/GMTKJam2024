using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame

    public Vector3 rotationForce;
    void Update()
    {
        transform.Rotate(rotationForce * Time.deltaTime);
    }
}
