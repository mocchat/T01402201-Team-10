using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private Player player; // Player ��ü�� ���� ������ ������ ����
    public float player_speed;
    public float true_speed;

    private void Start()
    {
        player = FindObjectOfType<Player>(); // Player ��ü�� ã�Ƽ� �Ҵ�
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("enter");

            Debug.Log("Cold");
            GameManager.instance.TakeDamage(0.1f);

            true_speed = player_speed + 2f;
            GameManager.instance.player.speed = true_speed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("exit");
            true_speed = player_speed;
            GameManager.instance.player.speed = player_speed;
        }
    }
}
