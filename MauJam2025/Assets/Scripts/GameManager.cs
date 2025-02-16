using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI WarningText;

    public GameObject DeathScreen;

    public float shakeDuration = 0.5f;
    public float fadeInDuration = 0.5f;
    public float stayDuration = 1.5f;
    public float fadeOutDuration = 1f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        DeathScreen.SetActive(false);
    }

    public void ShowWarning(string message)
    {
        WarningText.text = message;
        WarningText.color = new Color(1, 0, 0, 0); // Ba�lang��ta g�r�nmez ve k�rm�z� yap
        WarningText.transform.localScale = Vector3.zero; // K���k ba�las�n

        Sequence seq = DOTween.Sequence();

        seq.Append(WarningText.DOFade(1, fadeInDuration)) // Alpha 0 - 1 (G�r�n�r yap)
           .Join(WarningText.transform.DOScale(1.2f, fadeInDuration).SetEase(Ease.OutBack)) // B�y�me efekti
           .Append(WarningText.transform.DOShakePosition(shakeDuration, new Vector3(20f, 0, 0), 20, 90, false, true)) // Sa�a sola sallanma
           .AppendInterval(stayDuration) // Bir s�re ekranda kal
           .Append(WarningText.DOFade(0, fadeOutDuration)) // Yava��a kaybol
           .Join(WarningText.transform.DOScale(0, fadeOutDuration).SetEase(Ease.InBack)) // K���lerek kaybol
           .OnComplete(() => WarningText.text = ""); // Kaybolunca i�eri�i temizle
    }

}
