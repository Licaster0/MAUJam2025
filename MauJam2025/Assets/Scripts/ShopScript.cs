using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TextMeshProUGUI ShopInteractText;
    [SerializeField] private GameObject ShopUi;
    [SerializeField] private bool canShop = false;
    [SerializeField] private int lightCost;

    private void Update()
    {
        if (canShop)
        {
            if (ShopInteractText.enabled && Input.GetKeyDown(KeyCode.E))
            {
                ShopUi.SetActive(true);
                canShop = false;
            }
        }
    }

    public void BuyLight()
    {
        if(PlayerManager.instance.Coin >= lightCost)
        {
            Debug.Log("Iþýk Alýndý");
            PlayerManager.instance.playerMaxLight = Mathf.Min(4, PlayerManager.instance.playerMaxLight + 1);
            //PlayerManager.instance.playerLightCount++;
            PlayerManager.instance.Coin -= lightCost;
            ShopUi.SetActive(false);
        }
        else
        {
            GameManager.instance.ShowWarning("Altýn Yetersiz");
        }
    }
    public void CancelLight()
    {
        ShopUi.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShopInteractText.enabled = true;
            canShop = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShopInteractText.enabled = false;
            ShopUi.SetActive(false);
            canShop = false;
        }
    }
}
