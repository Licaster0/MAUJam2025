using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeZoneDoorScript : MonoBehaviour
{
    public List<GameObject> levelPrefabs; // Olasý seviyeleri içeren prefab listesi
    public Transform spawnPoint; // Seviye spawn noktasý
    public GameObject uiPanel; // UI penceresi (Evet / Hayýr butonlarý için)

    private bool isPlayerInZone = false;
    private GameObject spawnedLevel;

    private void Start()
    {
        uiPanel.SetActive(false); // UI baþlangýçta kapalý
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            uiPanel.SetActive(true); // Uyarýyý aç
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            uiPanel.SetActive(false); // Oyuncu uzaklaþýnca kapat
        }
    }

    public void StartGame()
    {
        if (spawnedLevel != null)
        {
            Destroy(spawnedLevel); // Önceki leveli temizle
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
