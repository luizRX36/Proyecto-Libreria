using UnityEngine;

public class InteractableBook : MonoBehaviour, IInteractable
{
    public string bookTitle = "Libro antiguo";

    public void Interact()
    {
        Debug.Log($"Has interactuado con {bookTitle}");
        // Aqu� abrir�as la UI de copiado, por ejemplo
    }

    public string GetDescription()
    {
        return $"Presiona E para leer \"{bookTitle}\"";
    }
}