using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    [SerializeField] private float sensitivity;

    private void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 newAngles = transform.localEulerAngles;
        newAngles.x -= mouseY * sensitivity;
        transform.localEulerAngles = newAngles;
    }
}
