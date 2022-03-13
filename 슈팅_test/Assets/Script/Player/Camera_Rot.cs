using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rot : MonoBehaviour
{
    private float rx;
    private float ry;
    public float Rot_Speed = 200;

    void Start()
    {
        
    }

    void Update()
    {
        float mx = Input.GetAxis("Mouse Y");
        float my = Input.GetAxis("Mouse X");

        rx += Rot_Speed * mx * Time.deltaTime;
        ry += Rot_Speed * my * Time.deltaTime;

        rx = Mathf.Clamp(rx, -80, 80);

        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
