using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SafeZoneDoorScript : MonoBehaviour
{
    [Header ("Referances")]
    [SerializeField] private GameObject safeZoneCollider;
    public List<GameObject> levelPrefabs; // Olasý seviyeleri içeren prefab listesi
    public Transform spawnPoint; // Seviye spawn noktasý
    public GameObject uiPanel; // UI penceresi (Evet / Hayýr butonlarý için)
    public bool levelLoaded = false;
    public Light2D GlobalLight;

    [Space]
    private GameObject spawnedLevel;

    private void Start()
    {
        uiPanel.SetActive(false); // UI baþlangýçta kapalý
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& !levelLoaded)
        {
            uiPanel.SetActive(true); // Uyarýyý aç
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(false); // Oyuncu uzaklaþýnca kapat
        }
    }

    public void StartGame()
    {
        if (spawnedLevel != null)
        {
            DeleteLevel();
        }
        levelLoaded = true;
        safeZoneCollider.SetActive(false);
        int randomIndex = Random.Range(0, levelPrefabs.Count);
        spawnedLevel = Instantiate(levelPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

        uiPanel.SetActive(false); // UI paneli kapat
        FadeLight(0.08f, 2f); // 2 saniyede 0.11f'e düþsün
        if (gameObject.TryGetComponent<TilemapRenderer>(out var tileMapRenderer))
        {
            tileMapRenderer.enabled = false;
        }
        if (gameObject.TryGetComponent<SafeZoneDoorScript>(out var doorScript))
        {
            doorScript.enabled = false;
        }

    }
    public void DeleteLevel()
    {
        Destroy(spawnedLevel); // Önceki leveli temizle
        safeZoneCollider.SetActive(true);
        if (gameObject.TryGetComponent<TilemapRenderer>(out var tileMapRenderer))
        {
            tileMapRenderer.enabled = true;
        }
        if (gameObject.TryGetComponent<SafeZoneDoorScript>(out var doorScript))
        {
            doorScript.enabled = true;
        }
    }
    public void FadeLight(float targetIntensity, float duration)
    {
        DOTween.To(() => GlobalLight.intensity, x => GlobalLight.intensity = x, targetIntensity, duration)
               .SetEase(Ease.InOutSine); // Daha akýcý geçiþ
    }
    public void Cancel()
    {
        uiPanel.SetActive(false); // UI paneli kapat
    }
}
