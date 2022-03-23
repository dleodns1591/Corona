using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 고통 게이지 관리 스크립트
public class Pain_Bar : MonoBehaviour
{
    [SerializeField]
    private Slider Pain_Hader;

    private float Max_Pain = 100;
    private float Cur_Pain = 10;
    public Text Pain_Text;

    void Start()
    {
        Pain_Hader.value = (float)Cur_Pain / (float)Max_Pain;
    }

    void Update()
    {

    }

    private void Handle_Pain()
    {
        Pain_Hader.value = (float)Cur_Pain / (float)Max_Pain;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Enemy_01 (박테리아) 와 닿았을 경우
        // Enemy_02 (세균) 과 닿았을 경우
        // Enemy_03 (바이러스) 와 닿았을 경우
        // Enemy_04 (암세포) 와 닿았을 경우
        Enemy_Move EM = other.GetComponent<Enemy_Move>();
        if (other.CompareTag("Enemy_01") || other.CompareTag("Enemy_02") || other.CompareTag("Enemy_03") || other.CompareTag("Enemy_04"))
        {
            Invoke("Handle_Pain", 0.01f);
            Cur_Pain += EM.Damage / 2;
            Pain_Text.text = "Pain : " + Cur_Pain + "/ 100";
        }
    }
}
