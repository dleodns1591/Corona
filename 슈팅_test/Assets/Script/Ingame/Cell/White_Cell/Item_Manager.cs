using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Manager : MonoBehaviour
{
    [Header("ÀÌµ¿ ¼Óµµ")]
    [SerializeField]
    public float Move_Speed = 0.0f;

    [Header("ÀÌµ¿ ¹æÇâ")]
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
                int Random_Item = Random.Range(1,7);
                if (Random_Item == 1)
                {
                    Debug.Log("1¹ø »ÌÈû");
                    this.gameObject.tag = "Item_01";
                    transform.GetChild(1).gameObject.SetActive(true);
                }

                else if (Random_Item == 2)
                {
                    Debug.Log("2¹ø »ÌÈû");
                    this.gameObject.tag = "Item_02";
                    transform.GetChild(2).gameObject.SetActive(true);
                }

                else if (Random_Item == 3)
                {
                    this.gameObject.tag = "Item_03";
                    Debug.Log("3¹ø »ÌÈû");
                    transform.GetChild(3).gameObject.SetActive(true);
                }

                else if (Random_Item == 4)
                {
                    this.gameObject.tag = "Item_04";
                    Debug.Log("4¹ø »ÌÈû");
                    transform.GetChild(4).gameObject.SetActive(true);
                }

                else if (Random_Item == 5)
                {
                    this.gameObject.tag = "Item_05";
                    Debug.Log("5¹ø »ÌÈû");
                    transform.GetChild(5).gameObject.SetActive(true);
                }

                else if (Random_Item == 6)
                {
                    this.gameObject.tag = "Item_06";
                    Debug.Log("6¹ø »ÌÈû");
                    transform.GetChild(6).gameObject.SetActive(true);
                }
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
