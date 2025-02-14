using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeZoneDoorScript : MonoBehaviour
{
    public List<GameObject> levelPrefabs; // Olas� seviyeleri i�eren prefab listesi
    public Transform spawnPoint; // Seviye spawn noktas�
    public GameObject uiPanel; // UI penceresi (Evet / Hay�r butonlar� i�in)

    private bool isPlayerInZone = false;
    private GameObject spawnedLevel;

    private void Start()
    {
        uiPanel.SetActive(false); // UI ba�lang��ta kapal�
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            uiPanel.SetActive(true); // Uyar�y� a�
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            uiPanel.SetActive(false); // Oyuncu uzakla��nca kapat
        }
    }

    public void StartGame()
    {
        if (spawnedLevel != null)
        {
            Destroy(spawnedLevel); // �nceki leveli temizle
        }

        int randomIndex = Random.Range(0, levelPrefabs.Count);
        spawnedLevel = Instantiate(levelPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

        uiPanel.SetActive(false); // UI paneli kapat
    }

    public void Cancel()
    {
        uiPanel.SetActive(false); // UI paneli kapat
    }
}
