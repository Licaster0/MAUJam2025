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
    public int Coin;
    public TextMeshProUGUI coinText;

    private void Start()
    {
        playerLightCount = 1;
    }
    private void FixedUpdate()
    {
        UpdateCoinGui();
    }
    public void UpdateCoinGui()
    {
        coinText.text = Coin.ToString();
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
