using UnityEngine;

public class WritingDesk : MonoBehaviour, IInteractable
{
    public GameObject writingUI; // el objeto con el minijuego
    public string deskName = "Escritorio";

    public string GetDescription()
    {
        return $"Presiona E para escribir en el {deskName}";
    }

    public void Interact()
    {
        Debug.Log("Entrando al minijuego de escritura...");
        writingUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Aquí puedes pausar el movimiento del jugador si quieres
        Time.timeScale = 0f;
    }

    public InteractableType GetInteractableType()
    {
        Debug.Log($"Estas interactuando con: {deskName}");
        return InteractableType.Book;
    }
}