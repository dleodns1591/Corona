using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    public Text Score_Text;
    public float Score_Point;

    public Text Enemy_Die_Text;
    public float EnemyDie_Point;

    public Text Stage01_Score;
    public Text Stage01_EnemyDie;

    void Start()
    {

    }

    void Update()
    {
        BossDie();
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

    public void Stage01_Clear_UI()
    {
        Stage01_Score.text = "Score : " + Score_Point.ToString();
        Stage01_EnemyDie.text = "Enemy Die : " + EnemyDie_Point.ToString();
    }

    public void EnemyDie(int Enemy)
    {
        EnemyDie_Point += Enemy;
        Enemy_Die_Text.text = "Enemy Die : " + EnemyDie_Point.ToString();
    }

    public void BossDie()
    {
        if (Boss.instance.Cur_Hp <= 0)
        {
            transform.GetChild(7).gameObject.SetActive(true);
            Stage01_Clear_UI();
        }
        else
        {
            transform.GetChild(7).gameObject.SetActive(false);
        }
    }

    public void Stage01_Clear_Button()
    {
        Debug.Log("Å¬¸¯");
        SceneManager.LoadScene("Stage_02");
    }
}
