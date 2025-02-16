using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BonfireScript : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] GameObject Player;
    [SerializeField] UI_InGame game;
    [SerializeField] Transform spawnTransform;
    [SerializeField] private TextMeshProUGUI BonfireInteractText;
    [SerializeField] private GameObject BonfireUi;
    [SerializeField] private bool canBonfire = false;
    [SerializeField] CanvasGroup blackCanvasGroup;
    public SafeZoneDoorScript safeZoneDoorScript;
    public bool loading = false;
    public Light2D GlobalLight;
    
    private void Update()
    {
        if (canBonfire)
        {
            if (BonfireInteractText.enabled && Input.GetKeyDown(KeyCode.E))
            {
                BonfireUi.SetActive(true);
                canBonfire = false;
            }
        }
    }
    private void loadingFalse()
    {
        loading = false;
        Player.transform.position = spawnTransform.transform.position;
    }
    public void BonfireLit()
    {
        loading = true;
        Invoke(nameof(loadingFalse), 2.9f);
        PlayerManager.instance.player.stats.currentHealth = PlayerManager.instance.player.stats.GetMaxHealthValue();
        game.UpdateHealthUI();
        Debug.Log("BonfireLit");
        BonfireUi.SetActive(false);
        FadeLight(1, 0, 2f); // 2 saniyede 0.11f'e düşsün
        BlackAlpha(0, 1, 2);
        DOVirtual.DelayedCall(2.8f, () => BlackAlpha(1f, 0f, 2f));
        DOVirtual.DelayedCall(3.2f, () => FadeLight(0f, 1f, 2f));
        GameObject.Find("SafeZoneDoor").GetComponent<SafeZoneDoorScript>().DeleteLevel();
        PlayerManager.instance.MaxLightUpdate();
        safeZoneDoorScript.levelLoaded = false;
    }
    public void FadeLight(float startIntensity, float targetIntensity, float duration)
    {
        GlobalLight.intensity = startIntensity; // Başlangıç değerini hemen ayarla

        DOTween.To(() => GlobalLight.intensity,
                   x => GlobalLight.intensity = x,
                   targetIntensity,
                   duration)
               .SetEase(Ease.InOutSine); // Daha akıcı bir geçiş için
    }

    public void BlackAlpha(float startAlpha, float targetAlpha, float duration)
    {
        blackCanvasGroup.alpha = startAlpha; // Başlangıç değerini ayarla
        blackCanvasGroup.DOFade(targetAlpha, duration).SetEase(Ease.InOutSine); // Yumuşak geçiş yap
    }
    public void CancelBonfire()
    {
        BonfireUi.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BonfireInteractText.enabled = true;
            canBonfire = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BonfireInteractText.enabled = false;
            BonfireUi.SetActive(false);
            canBonfire = false;
        }
    }
}
