using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    Animator anim;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim = collision.gameObject.GetComponent<Animator>();

            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).gameObject.name == "Wall" // flyeye와 bat은 벽 넘기 불가
                    && (anim.runtimeAnimatorController.name == "AcEnemy_flyeye" || anim.runtimeAnimatorController.name == "AcEnemy_bat"))
                    continue;
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.transform.GetChild(i).GetComponent<Collider2D>());
            }
        }
    }
}
