using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HpBar : MonoBehaviour
{
    [SerializeField]
    GameObject HpBar_Prefab = null;

    List<Transform> Enemy_List = new List<Transform>();
    List<GameObject> HpBar_List = new List<GameObject>();
    Camera Cam = null;

    void Start()
    {
        Cam = Camera.main;

        GameObject[] Enemy = GameObject.FindGameObjectsWithTag("Enemy_01");
        for (int i = 0; i < Enemy.Length; i++)
        {
            Enemy_List.Add(Enemy[i].transform);
            GameObject HpBar = Instantiate(HpBar_Prefab, Enemy[i].transform.position, Quaternion.identity, transform);
            HpBar_List.Add(HpBar);
        }
    }

    void Update()
    {
        for (int i = 0; i < Enemy_List.Count; i++)
        {
            HpBar_List[i].transform.position = Cam.WorldToScreenPoint(Enemy_List[i].position + new Vector3(0, 1.15f, 0));
        }
    }
}
