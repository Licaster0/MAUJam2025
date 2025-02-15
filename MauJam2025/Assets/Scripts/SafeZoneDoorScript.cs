using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SafeZoneDoorScript : MonoBehaviour
{
    [Header ("Referances")]
    [SerializeField] private GameObject safeZoneCollider;
    public List<GameObject> levelPrefabs; // Olas� seviyeleri i�eren prefab listesi
    public Transform spawnPoint; // Seviye spawn noktas�
    public GameObject uiPanel; // UI penceresi (Evet / Hay�r butonlar� i�in)
    public Light2D GlobalLight;

    [Space]
    private GameObject spawnedLevel;

    private void Start()
    {
        uiPanel.SetActive(false); // UI ba�lang��ta kapal�
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(true); // Uyar�y� a�
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(false); // Oyuncu uzakla��nca kapat
        }
    }

    public void StartGame()
    {
        if (spawnedLevel != null)
        {
            DeleteLevel();
        }
        safeZoneCollider.SetActive(false);
        int randomIndex = Random.Range(0, levelPrefabs.Count);
        spawnedLevel = Instantiate(levelPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

        uiPanel.SetActive(false); // UI paneli kapat
        FadeLight(0.08f, 2f); // 2 saniyede 0.11f'e d��s�n
        gameObject.SetActive(false);

    }
    public void DeleteLevel()
    {
        Destroy(spawnedLevel); // �nceki leveli temizle
    }
    public void FadeLight(float targetIntensity, float duration)
    {
        DOTween.To(() => GlobalLight.intensity, x => GlobalLight.intensity = x, targetIntensity, duration)
               .SetEase(Ease.InOutSine); // Daha ak�c� ge�i�
    }
    public void Cancel()
    {
        uiPanel.SetActive(false); // UI paneli kapat
    }
}
