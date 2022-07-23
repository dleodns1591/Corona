using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public BoxCollider boxCollider;

    void Start()
    {
        
    }

    void Update()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("����ź"))
        {
            boxCollider.isTrigger = true;
        }
    }
}
