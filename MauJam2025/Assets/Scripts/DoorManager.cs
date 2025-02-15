using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager instance;
    public GameObject DoorUiPanel;
    private Doors currentDoor; // Hangi kapýnýn açýlacaðýný takip edeceðiz

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowDoorUI(Doors door)
    {
        if (DoorUiPanel.activeSelf) return; // UI zaten açýksa yeni UI açma

        currentDoor = door;
        DoorUiPanel.SetActive(true);
    }

    public void HideDoorUI()
    {
        DoorUiPanel.SetActive(false);
        currentDoor = null;
    }

    public void ConfirmOpen()
    {
        if (currentDoor != null)
            currentDoor.DoorOpen();
    }

    public void Cancel()
    {
        HideDoorUI();
    }
}
