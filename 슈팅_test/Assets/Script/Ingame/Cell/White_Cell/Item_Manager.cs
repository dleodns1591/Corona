using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Manager : MonoBehaviour
{
    [Header("이동 속도")]
    [SerializeField]
    public float Move_Speed = 0.0f;

    [Header("이동 방향")]
    [SerializeField]
    public Vector3 Move_Direction = Vector3.zero;

    public bool RI = true;

    void Start()
    {

    }

    void Update()
    {
        transform.position += Move_Direction * Move_Speed * Time.deltaTime;
    }

    void Awake()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            if (RI)
            {
                RI = false;
                int Random_Item = Random.Range(2,2);
                Debug.Log($"{Random_Item}번 뽑힘");
                this.gameObject.tag = $"Item_0{Random_Item}";
                transform.GetChild(Random_Item).gameObject.SetActive(true);
            }


        }

        if (other.CompareTag("Destroy_Wall") || other.CompareTag("Player"))
        {
            if (tag == "Item_01" || tag == "Item_02" || tag == "Item_03" || tag == "Item_05" || tag == "Item_06")
            {
                Destroy(this.gameObject);
            }
            else if (tag == "Item_04")
            {
                Move_Speed = 300;
            }
        }


    }
}
