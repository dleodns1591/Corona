using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawn_Enemy : MonoBehaviour
{
    [Header("¹ÚÅ×¸®¾Æ")]
    public GameObject Enemy_Prefab_01;

    [Header("¼¼±Õ")]
    public GameObject Enemy_Prefab_02;

    [Header("¹ÙÀÌ·¯½º")]
    public GameObject Enemy_Prefab_03;

    [Header("¾Ï¼¼Æ÷")]
    public GameObject Enemy_Prefab_04;

    [Header("¹éÇ÷±¸")]
    public GameObject White_Blood;

    [Header("ÀûÇ÷±¸")]
    public GameObject Red_Blood;

    void Start()
    {
        InvokeRepeating("Probability_White", 10f, 10f);
        InvokeRepeating("Probability_Red", 15f, 25f);
        InvokeRepeating("Enemy01_Spawn", 3f, 1.5f);
        InvokeRepeating("Enemy02_Spawn", 10f, 4f);
        InvokeRepeating("Enemy03_Spawn", 3f, 3.5f);
        InvokeRepeating("Enemy04_Spawn", 10f, 4f);
    }

    void Update()
    {

    }

    private void Enemy01_Spawn()
    {
        if (SceneManager.GetActiveScene().name == "Stage_01")
        {
            if (UI_Manager.instance.EnemyDie_Point <= 30 && Boss.instance.All_Enemy_Check == true)
            {
                float Random_Z = Random.Range(500f, 620f);
                float Random_Y = Random.Range(70f, 100f);
                float Random_X = Random.Range(500f, 620f);
                GameObject Enemy = Instantiate(Enemy_Prefab_01, new Vector3(Random_X, Random_Y, Random_Z), Quaternion.identity);
            }

            else if (Boss.instance.BossHP_Check == false)
            {
                float Random_X = Random.Range(500f, 620f);
                GameObject Enemy = Instantiate(Enemy_Prefab_01, new Vector3(Random_X, 80f, 500f), Quaternion.identity);
            }
        }

        if (SceneManager.GetActiveScene().name == "Stage_02")
        {
            if (Boss.instance.BossHP_Check == false && Boss.instance.Cur_Hp >= 1)
            {
                float Random_X = Random.Range(500f, 620f);
                GameObject Enemy = Instantiate(Enemy_Prefab_01, new Vector3(Random_X, 80f, 500f), Quaternion.identity);
            }
        }
    }

    private void Enemy02_Spawn()
    {
        if (SceneManager.GetActiveScene().name == "Stage_01")
        {
            if (UI_Manager.instance.EnemyDie_Point <= 30 && Boss.instance.All_Enemy_Check == true)
            {
                float Random_Z = Random.Range(500f, 620f);
                float Random_X = Random.Range(500f, 620f);
                GameObject Enemy = Instantiate(Enemy_Prefab_02, new Vector3(Random_X, 80f, Random_Z), Quaternion.identity);
            }
        }
    }

    private void Enemy03_Spawn()
    {
        if (SceneManager.GetActiveScene().name == "Stage_02")
        {
            if (UI_Manager.instance.EnemyDie_Point <= 30 && Boss.instance.All_Enemy_Check == true)
            {
                float Random_X = Random.Range(451.8f, 620.7f);
                GameObject Enemy = Instantiate(Enemy_Prefab_03, new Vector3(Random_X, 67.8f, 1418), Quaternion.identity);
            }
        }
    }

    private void Enemy04_Spawn()
    {
        if (SceneManager.GetActiveScene().name == "Stage_02")
        {
            if (UI_Manager.instance.EnemyDie_Point <= 30 && Boss.instance.All_Enemy_Check == true)
            {
                float Random_X = Random.Range(451.8f, 620.7f);
                GameObject Enemy = Instantiate(Enemy_Prefab_04, new Vector3(Random_X, 67.8f, 1418), Quaternion.identity);
            }
        }
    }

    public void White_Cell()
    {
        float Random_Z = Random.Range(500f, 620f);
        float Random_X = Random.Range(460f, 630f);
        GameObject Enemy = Instantiate(White_Blood, new Vector3(Random_X, 56f, Random_Z), Quaternion.identity);
    }

    public void Red_Cell()
    {
        if (UI_Manager.instance.EnemyDie_Point <= 30 && Boss.instance.All_Enemy_Check == true)
        {
            float Random_Z = Random.Range(500f, 620f);
            float Random_X = Random.Range(460f, 630f);
            GameObject Enemy = Instantiate(Red_Blood, new Vector3(Random_X, 56f, Random_Z), Quaternion.identity);
        }
    }

    public void Probability_White()
    {
        int Probability_White = Random.Range(1, 101);
        //print(Probability_White);

        // 30% È®·ü·Î ¹éÇ÷±¸ »ý¼º
        if (Probability_White <= 30)
        {
            White_Cell();
        }
    }

    public void Probability_Red()
    {
        int Probability_Red = Random.Range(1, 101);
        //print(Probability_Red);

        // 15% È®·ü·Î ÀûÇ÷±¸ »ý¼º
        if (Probability_Red <= 15)
        {
            Red_Cell();
        }
    }
}
