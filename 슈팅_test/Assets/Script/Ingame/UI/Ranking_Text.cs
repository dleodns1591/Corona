using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Ranking_Text : MonoBehaviour
{
    public static Ranking_Text instance;

    
    public GameObject rankbord;
    void Start()
    {
        var s = new StreamReader("Assets/Resources/RanKing.txt");
        string[] textlist = new string[5];
        for (int i = 0; i < 5; i++)
        {
            string textLine = s.ReadLine();

            textlist[i] = (textLine == null || textLine == "") ? "NONE,0" : textLine;
        }
        for (int k = 0; k < 5; k++)
        {
            GameObject RankBord = rankbord.transform.GetChild(k).gameObject;
            RankBord.GetComponent<Text>().text = $"{k + 1} Rank";
            RankBord.transform.transform.GetChild(0).GetComponent<Text>().text = $"{textlist[k].Split(',')[0]}";
            RankBord.transform.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{textlist[k].Split(',')[1]}";
        }
    }
        
    void Update()
    {
        Destroy(GameObject.Find("Stage_01_Score_Point"));
        Destroy(GameObject.Find("Player"));
    }
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // 데이터 처리
    //var s = new StreamReader("Assets/Resources/RanKing.txt");
    //string[] textlist = new string[5];
    //string textLine = s.ReadLine();
    //string[] txt = textlist[k].Split(',');
    //PlayerHp,100

    //    Hp = txt[1];


}
