using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

    [Header("Sayilar")]
    public int doorCount;

    [Header("Referances")]
    public GameObject DoorUiPanel;

    private void Awake()
    {
        DoorUiPanel = GameObject.Find("DoorUiPanel").GetComponent<GameObject>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameManager.instance.playerLightCount -= doorCount;
            DoorUiPanel.SetActive(true); // Uyarýyý aç
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            DoorUiPanel.SetActive(false); // Oyuncu uzaklaþýnca kapat
        }
    }
    public void DoorOpen()
    {
        DoorUiPanel.SetActive(false); // UI paneli kapat
        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        DoorUiPanel.SetActive(false); // UI paneli kapat
    }
}
