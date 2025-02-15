using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("References")]
    public Player player;
    public int playerLightCount;
    public TextMeshProUGUI lightText;
    public TextMeshProUGUI maxLightText;
    public int playerMaxLight;
    public int Coin;
    public TextMeshProUGUI coinText;

    public Image[] lightImages; // UI'de kullandığınız ışıklar (Image array)
    public Sprite fullLight; // Dolu ışık sprite'
    public Sprite emptyLight; // Boş ışık sprite'

    private void Start()
    {
        playerLightCount = 1;
        playerMaxLight = playerLightCount;
        UpdateGui(); // İlk GUI güncellemesi
    }

    private void Update()
    {
        UpdateGui(); // UI her frame güncellenir
    }

    // GUI'yi güncelle
    public void UpdateGui()
    {
        coinText.text = Coin.ToString();

        // İşığın durumunu UI'ye yansıt
        for (int i = 0; i < lightImages.Length; i++)
        {
            // Işıklar arasında sadece gerekli olanları güncelle
            if (i < playerMaxLight)
            {
                lightImages[i].gameObject.SetActive(true); // Işığı aktif et
                lightImages[i].sprite = (i < playerLightCount) ? fullLight : emptyLight; // Dolu ya da boş
            }
            else
            {
                lightImages[i].gameObject.SetActive(false); // Gereksiz ışıkları gizle
            }
        }

        // Light Text güncellemesi
        lightText.text = playerLightCount.ToString() + "/" + playerMaxLight.ToString();
    }

    // Işık sayısını arttırmak için fonksiyon
    public void IncreaseMaxLight(int amount)
    {
        playerMaxLight = Mathf.Max(1, playerMaxLight + amount); // En az 1 ışık olmalı
        MaxLightUpdate();
    }

    // Işık sayısını yenile
    public void MaxLightUpdate()
    {
        playerLightCount = playerMaxLight;
        playerLightCount = Mathf.Min(playerMaxLight, playerLightCount); // Mevcut ışık, max ışığı geçmesin
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Singleton
        }
    }
}
