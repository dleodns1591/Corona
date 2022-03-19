using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 Position = transform.position;

        Position.x += Horizontal * Time.deltaTime * Speed;
        Position.z += Vertical * Time.deltaTime * Speed;

        transform.position = Position;
    }
}
