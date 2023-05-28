using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : MonoBehaviour
{
    public float player_speed;
    public float true_speed;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            true_speed = player_speed - 1.5f;
            GameManager.instance.player.speed = true_speed;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            true_speed = player_speed;
            GameManager.instance.player.speed = player_speed;
        }
    }
}
