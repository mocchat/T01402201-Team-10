using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private Player player; // Player 객체에 대한 참조를 저장할 변수
    public float player_speed;
    public float true_speed;

    private void Start()
    {
        player = FindObjectOfType<Player>(); // Player 객체를 찾아서 할당
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("enter");

            Debug.Log("Cold");
            GameManager.instance.TakeDamage(2f);

            true_speed = player_speed + 5f;
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
