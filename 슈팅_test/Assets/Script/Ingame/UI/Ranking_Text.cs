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
            if (textLine == "")
            {
                textlist[i] = "NONE,0";
            }
            else
            textlist[i] = (textLine == null) ? "NONE,0" : textLine;
        }
        for (int k = 0; k < 5; k++)
        {
            string name;
            string score;
            string[] txt = textlist[k].Split(',');
            name = txt[0];
            score = txt[1];

            rankbord.transform.GetChild(k).GetComponent<TextMeshProUGUI>().text = ($"{k + 1} Rank");
            rankbord.transform.GetChild(k).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
            rankbord.transform.GetChild(k).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = score;
        }
    }
        
    void Update()
    {
       
    }
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    
    
}
