using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBullet : MonoBehaviour
{
    public int dmg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Borderbullet")
        {
            gameObject.SetActive(false);
        }
    }
}