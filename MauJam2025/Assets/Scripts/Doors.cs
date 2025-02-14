using UnityEngine;

public class Doors : MonoBehaviour
{
    [Header("Sayilar")]
    public int doorCount;
    private bool isOpened = false; // Kap�n�n a��l�p a��lmad���n� kontrol et

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isOpened)
        {
            DoorManager.instance.ShowDoorUI(this); // UI panelini a� ve kap�y� sakla
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
        if (isOpened) return; // E�er kap� zaten a��ld�ysa tekrar a�ma

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager bulunamad�!");
            return;
        }

        if (GameManager.instance.playerLightCount >= doorCount)
        {
            isOpened = true; // Kap� a��ld� olarak i�aretle
            DoorManager.instance.HideDoorUI();
            GameManager.instance.playerLightCount -= doorCount;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Yetersiz ���k say�s�!");
        }
    }
}
