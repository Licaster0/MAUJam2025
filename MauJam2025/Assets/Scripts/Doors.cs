using UnityEngine;

public class Doors : MonoBehaviour
{
    [Header("Sayilar")]
    public int doorCount;
    private bool isOpened = false; // Kapýnýn açýlýp açýlmadýðýný kontrol et

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isOpened)
        {
            DoorManager.instance.ShowDoorUI(this); // UI panelini aç ve kapýyý sakla
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            DoorManager.instance.HideDoorUI();
        }
    }

    public void DoorOpen()
    {
        if (isOpened) return; // Eðer kapý zaten açýldýysa tekrar açma

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager bulunamadý!");
            return;
        }

        if (PlayerManager.instance.playerLightCount >= doorCount)
        {
            isOpened = true; // Kapý açýldý olarak iþaretle
            DoorManager.instance.HideDoorUI();
            PlayerManager.instance.playerLightCount = Mathf.Max(0, PlayerManager.instance.playerLightCount - doorCount);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Yetersiz ýþýk sayýsý!");
            GameManager.instance.ShowWarning("Yetersiz ýþýk sayýsý!");
        }
    }
}
