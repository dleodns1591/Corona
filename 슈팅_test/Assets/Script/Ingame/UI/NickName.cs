using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;

public class NickName : MonoBehaviour
{
    public InputField PlayerName_Input;
    public float _Score;
    public string names;
    public GameObject rankbord;
    string[] textlist = new string[5];

    public AudioSource audioSource;

    void Start()
    {
        
        var s = new StreamReader("Assets/Resources/RanKing.txt");
        for (int i = 0; i < 5; i++)
        {
            string textLine = s.ReadLine();

            textlist[i] = (textLine == null || textLine == "") ? "NONE,0" : textLine;
        }
        if (rankbord != null)
        { 
            for (int k = 0; k < 5; k++)
            {
                GameObject RankBord = rankbord.transform.GetChild(k).gameObject;
                RankBord.GetComponent<TextMeshProUGUI>().text = $"{k + 1} Rank";
                RankBord.transform.transform.GetChild(0).GetComponent<Text>().text = $"{textlist[k].Split(',')[0]}";
                RankBord.transform.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{textlist[k].Split(',')[1]}";
            }
        }
    }

    public void InputName()
    {
        // ¹öÆ°
        _Score = AddScore.instance.Stage_AddPoint;
        names = PlayerName_Input.GetComponent<InputField>().text;
        StartCoroutine("addRank");
    }
    private IEnumerator addRank()
    {
        StreamWriter sw = new StreamWriter("Assets/Resources/RanKing.txt");
        for (int i = 0; i < 5; i++)
        {
            string[] txtscore = textlist[i].Split(',');
            if (int.Parse(txtscore[1]) < _Score)
            {
                for (int j = 3; j >= i; j--)
                {
                    textlist[j + 1] = textlist[j];
                }
                textlist[i] = names + "," + _Score;
                _Score = 0;
            }
            sw.WriteLine(textlist[i]);
        }
        sw.Close();
        SceneManager.LoadScene("Title");
        yield return null;
    }
}
