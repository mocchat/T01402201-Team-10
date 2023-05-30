using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private Player player; // Player 객체에 대한 참조를 저장할 변수

    private void Start()
    {

        player = FindObjectOfType<Player>(); // Player 객체를 찾아서 할당
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
