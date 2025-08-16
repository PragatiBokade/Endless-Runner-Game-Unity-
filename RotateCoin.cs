using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    public float rotationSpeed = 200f; // Adjust speed as needed

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0); // Rotates around Y-axis
    }
}
