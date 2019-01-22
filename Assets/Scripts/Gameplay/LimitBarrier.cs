 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitBarrier : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Pers"))
        {
            if (!collision.GetComponent<PersBehaviour>().isOnFollow)
            {
                Destroy(collision.gameObject);
            }
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
