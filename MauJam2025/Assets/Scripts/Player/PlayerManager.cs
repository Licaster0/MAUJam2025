using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("Referances")]
    public Player player;
    public int playerLightCount;
    public TextMeshProUGUI lightText;
    public int playerMaxLight;
    public int Coin;
    public TextMeshProUGUI coinText;

    private void Start()
    {
        playerLightCount = 1;
        playerMaxLight = playerLightCount;
    }
    private void FixedUpdate()
    {
        UpdateGui();
    }
    public void MaxLightUpdate()
    {
        playerLightCount = playerMaxLight;
    }
    public void UpdateGui()
    {
        coinText.text = Coin.ToString();
        lightText.text = playerLightCount.ToString();

    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
