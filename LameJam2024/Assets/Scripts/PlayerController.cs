using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    private Vector3 speed;

    private void Update()
    {
        // Moving
        speed += transform.forward * acceleration * 
    }
}
