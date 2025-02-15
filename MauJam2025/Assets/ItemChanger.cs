using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class ItemChanger : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TextMeshProUGUI ItemInteractText;
    [SerializeField] private TextMeshProUGUI ItemWarningText;
    [SerializeField] private GameObject ItemUi;
    [SerializeField] private bool canChange = false;

    public float shakeDuration = 0.5f;
    public float fadeInDuration = 0.5f;
    public float stayDuration = 1.5f;
    public float fadeOutDuration = 1f;

    private void Update()
    {
        if (canChange)
        {
            if (ItemInteractText.enabled && Input.GetKeyDown(KeyCode.E))
            {
                ItemUi.SetActive(true);
                canChange = false;
            }
        }
    }

    public void ChangeItem(int switchNumber)
    {
        if (PlayerManager.instance.player.attackCounter == switchNumber)
        {
            ShowWarning("Zaten Bu Silahı Kullanıyorsunuz");
            Invoke(nameof(ClearWarningText), 1f);
            ItemUi.SetActive(false);
        }
        PlayerManager.instance.player.attackCounter = switchNumber;
        Debug.Log("İtem Değiştirildi");
        ItemUi.SetActive(false);
    }
    public void ShowWarning(string message)
    {
        ItemWarningText.text = message;
        ItemWarningText.color = new Color(1, 0, 0, 0); // Ba�lang��ta g�r�nmez ve k�rm�z� yap
        ItemWarningText.transform.localScale = Vector3.zero; // K���k ba�las�n

        Sequence seq = DOTween.Sequence();

        seq.Append(ItemWarningText.DOFade(1, fadeInDuration)) // Alpha 0 - 1 (G�r�n�r yap)
           .Join(ItemWarningText.transform.DOScale(1.2f, fadeInDuration).SetEase(Ease.OutBack)) // B�y�me efekti
           .Append(ItemWarningText.transform.DOShakePosition(shakeDuration, new Vector3(20f, 0, 0), 20, 90, false, true)) // Sa�a sola sallanma
           .AppendInterval(stayDuration) // Bir s�re ekranda kal
           .Append(ItemWarningText.DOFade(0, fadeOutDuration)) // Yava��a kaybol
           .Join(ItemWarningText.transform.DOScale(0, fadeOutDuration).SetEase(Ease.InBack)) // K���lerek kaybol
           .OnComplete(() => ItemWarningText.text = ""); // Kaybolunca i�eri�i temizle
    }
    private void ClearWarningText()
    {

    }
    public void CancelWeapon()
    {
        ItemUi.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ItemInteractText.enabled = true;
            canChange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ItemInteractText.enabled = false;
            ItemUi.SetActive(false);
            canChange = false;
        }
    }
}
