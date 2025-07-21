using UnityEngine;

public class InteractableBook : MonoBehaviour, IInteractable
{
    public string bookTitle = "Libro antiguo";

    public void Interact()
    {
        Debug.Log($"Has interactuado con {bookTitle}");
        // Aquí abrirías la UI de copiado, por ejemplo
    }

    public string GetDescription()
    {
        return $"Presiona E para leer \"{bookTitle}\"";
    }
}