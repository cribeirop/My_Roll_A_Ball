using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp_2 : MonoBehaviour
{
    public float rotationAmplitude = 75f; 
    public float rotationFrequency = 0.75f; 
    public float rotationSpeedMultiplier = 10f; 
    private float initialRotationZ; 
    private float elapsedTime; 

    void Start()
    {
        initialRotationZ = transform.rotation.eulerAngles.z; 
    }

    void Update()
    {
        float rotationOffset = rotationAmplitude * Mathf.Sin(2 * Mathf.PI * rotationFrequency * elapsedTime);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, initialRotationZ + rotationOffset);

        elapsedTime += Time.deltaTime;
    }
}
