using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField] private float sensitivity;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 newAngles = transform.localEulerAngles;
        newAngles.y += mouseX * sensitivity;
        transform.localEulerAngles = newAngles;
    }
}
