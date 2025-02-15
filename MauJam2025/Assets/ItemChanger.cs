using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemChanger : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TextMeshProUGUI ItemInteractText;
    [SerializeField] private GameObject ItemUi;
    [SerializeField] private bool canChange = false;

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
        PlayerManager.instance.player.attackCounter = switchNumber;
        Debug.Log("Ýtem Deðiþtirildi");
        ItemUi.SetActive(false);
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
