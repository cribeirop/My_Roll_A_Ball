using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp_1 : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(new Vector3(0, 20f, 0) * Time.deltaTime);
    }
}
