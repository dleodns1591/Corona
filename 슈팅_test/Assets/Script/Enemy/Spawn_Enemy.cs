using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    public GameObject Enemy_Prefab;

    void Start()
    {
        InvokeRepeating("Spawn", 3f, 2f);
    }

    void Update()
    {

    }

    private void Spawn()
    {
        Vector3 Player_Position = GameObject.FindWithTag("Character").transform.position;
        float Random_Z = Random.Range(500f, 629f);
        float Random_Y = Random.Range(70f, 130f);
        float Random_X = Random.Range(450f, 650f);
        GameObject Enemy = (GameObject)Instantiate(Enemy_Prefab, new Vector3(Random_X,Random_Y,Random_Z), Quaternion.identity);
    }
}
