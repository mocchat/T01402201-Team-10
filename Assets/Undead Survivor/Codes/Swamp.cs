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
            collision.GetComponent<Player>().SetSpeed(true_speed);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            true_speed = player_speed;
            collision.GetComponent<Player>().SetSpeed(player_speed);
        }
    }
}
