using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSensitivity = 0.05f;
    public float ZoomSensitivity = 5f;
    public float RotateSensitivity = 2.5f;

    void Update()
    {
        Movment();
        Zoom();
        Rotate();
    }

    private void Movment()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * MovementSensitivity;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * MovementSensitivity;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * MovementSensitivity;
        }


        else if(Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * MovementSensitivity;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.up * MovementSensitivity;
        }

        else if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position -= transform.up * MovementSensitivity;
        }
    }

    void Zoom()
    {
        Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ZoomSensitivity;
    }

    void Rotate()
    {
        if (Input.GetMouseButton(2))
        {
            float horizontal = Input.GetAxis("Mouse X") * RotateSensitivity;
            float vertical = Input.GetAxis("Mouse Y") * RotateSensitivity;
            transform.localEulerAngles += new Vector3(vertical, horizontal, 0);
        }
    }
}