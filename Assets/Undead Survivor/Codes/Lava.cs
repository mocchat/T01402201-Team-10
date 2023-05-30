using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private Player player; // Player ��ü�� ���� ������ ������ ����

    private void Start()
    {

        player = FindObjectOfType<Player>(); // Player ��ü�� ã�Ƽ� �Ҵ�
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("enter");
            GameManager.instance.ApplyLavaEffectToPlayer();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("exit");
            GameManager.instance.RemoveLavaEffectFromPlayer();

        }
    }
}
