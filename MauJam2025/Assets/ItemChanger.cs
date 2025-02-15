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

    public float shakeDuration = 0.5f; // Sarsýlma süresi
    public float fadeInDuration = 0.5f; // Alfa artýþ süresi
    public float stayDuration = 1.5f; // Ekranda kalma süresi
    public float fadeOutDuration = 1f; // Alfa azalýþ süresi

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
            ShowWarning("Zaten Bu Silahý Kullanýyorsunuz");
            Invoke(nameof(ClearWarningText), 1f);
            ItemUi.SetActive(false);
        }
        PlayerManager.instance.player.attackCounter = switchNumber;
        Debug.Log("Ýtem Deðiþtirildi");
        ItemUi.SetActive(false);
    }
    public void ShowWarning(string message)
    {
        ItemWarningText.text = message;
        ItemWarningText.color = new Color(1, 0, 0, 0); // Baþlangýçta görünmez ve kýrmýzý yap
        ItemWarningText.transform.localScale = Vector3.zero; // Küçük baþlasýn

        Sequence seq = DOTween.Sequence();

        seq.Append(ItemWarningText.DOFade(1, fadeInDuration)) // Alpha 0 - 1 (Görünür yap)
           .Join(ItemWarningText.transform.DOScale(1.2f, fadeInDuration).SetEase(Ease.OutBack)) // Büyüme efekti
           .Append(ItemWarningText.transform.DOShakePosition(shakeDuration, new Vector3(20f, 0, 0), 20, 90, false, true)) // Saða sola sallanma
           .AppendInterval(stayDuration) // Bir süre ekranda kal
           .Append(ItemWarningText.DOFade(0, fadeOutDuration)) // Yavaþça kaybol
           .Join(ItemWarningText.transform.DOScale(0, fadeOutDuration).SetEase(Ease.InBack)) // Küçülerek kaybol
           .OnComplete(() => ItemWarningText.text = ""); // Kaybolunca içeriði temizle
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
