using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public string itemName = "Objeto";
    public InteractableType itemType = InteractableType.Tool;

    public void Interact()
    {

    }

    public string GetDescription()
    {
        return $"Presiona E para recoger {itemName}";
    }

    public virtual void OnPickup()
    {
        Debug.Log($"Has recogido: {itemName}");
    }

    public virtual void OnDrop()
    {
        Debug.Log($"Has soltado: {itemName}");
    }

    public InteractableType GetInteractableType()
    {
        return itemType;
    }
}
