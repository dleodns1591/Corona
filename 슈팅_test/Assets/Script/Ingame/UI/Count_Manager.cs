using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count_Manager : MonoBehaviour
{
    public Text Score_Text;
    public static float Score;

    void Start()
    {
        
    }

    void Update()
    {
        Score_Text.text = "Score : " + Score;
    }
}
