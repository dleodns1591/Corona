using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ������ ���� ��ũ��Ʈ
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
        // Enemy_01 (���׸���) �� ����� ���
        // Enemy_02 (����) �� ����� ���
        // Enemy_03 (���̷���) �� ����� ���
        // Enemy_04 (�ϼ���) �� ����� ���
        Enemy_Move EM = other.GetComponent<Enemy_Move>();
        if (other.CompareTag("Enemy_01") || other.CompareTag("Enemy_02") || other.CompareTag("Enemy_03") || other.CompareTag("Enemy_04"))
        {
            Invoke("Handle_Pain", 0.01f);
            Cur_Pain += EM.Damage / 2;
            Pain_Text.text = "Pain : " + Cur_Pain + "/ 100";
        }
    }
}
