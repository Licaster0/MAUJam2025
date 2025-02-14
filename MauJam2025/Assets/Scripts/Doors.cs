using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

    [Header("Sayilar")]
    public int doorCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.playerLightCount -= doorCount;
            gameObject.SetActive(false);
        }
    }

}
