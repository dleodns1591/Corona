using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;

public class NickName : MonoBehaviour
{
    public InputField PlayerName_Input;
    public float _Score;
    public string names;
    public void InputName()
    {
        _Score = AddScore.instance.Stage_AddPoint;
        names = PlayerName_Input.GetComponent<InputField>().text;
        StartCoroutine("addRank");
    }
    private IEnumerator addRank()
    {
        var stringReader = new StringReader(Resources.Load<TextAsset>("RanKing").text);
        string[] textlist = new string[5];
        for (int i = 0; i < 5; i++)
        {
            string textLine = stringReader.ReadLine();
            if (textLine == "")
            {
                textlist[i] = "NONE,0";
            }
            else
            textlist[i] = (textLine == null) ? "NONE,0" : textLine;
        }
        for (int i = 0; i < 5; i++)
        {
            string[] txtscore = textlist[i].Split(',');
            Debug.Log(txtscore[0]);
            if (int.Parse(txtscore[1]) < _Score)
            {
                for (int j = 3; j >= i; j--)
                {
                    textlist[j + 1] = textlist[j];
                }
                textlist[i] = names + "," + _Score;
                break;
            }
        }
        StreamWriter sw = new StreamWriter("Assets/Resources/RanKing.txt", false);
        for (int k = 0; k < 5; k++)
        {
            sw.WriteLine(textlist[k]);
        }
        sw.Close();
        SceneManager.LoadScene("Title");
        yield return null;
    }
}
