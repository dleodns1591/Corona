using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    public Text Score_Text;
    public float Score_Point;

    public Text Enemy_Die_Text;
    public float EnemyDie_Point;

    void Start()
    {

    }

    void Update()
    {

    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Score(int Score)
    {
        Score_Point += Score;
        Score_Text.text = "Score ; " + Score_Point.ToString();
    }

    public void EnemyDie(int Enemy)
    {
        EnemyDie_Point += Enemy;
        Enemy_Die_Text.text = "Enemy Die : " + EnemyDie_Point.ToString();
    }
}
