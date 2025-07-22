public interface IInteractable
{
    void Interact();
    string GetDescription(); 
    InteractableType GetInteractableType();
}

public enum InteractableType
{
    Book,
    Tool,
    Key,
    Artifact,
    Manuscript
}