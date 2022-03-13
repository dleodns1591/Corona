using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float Speed = 5;
    public float Gravity = -9.81f;
    public float Jump_Power = 10;
    private float Y_Velocity;

    CharacterController CC;

    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    void Update()
    {
        Y_Velocity += Gravity * Time.deltaTime;
        if (Input.GetButton("Jump"))
        {
            Y_Velocity = Jump_Power;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();
        dir.y = Y_Velocity;

        CC.Move(dir * Speed * Time.deltaTime);
    }
}
