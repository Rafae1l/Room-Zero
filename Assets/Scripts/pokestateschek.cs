
using UnityEngine;

public class PokeStateChecker : MonoBehaviour
{
    public Behaviour pokeInteractable;

    private void Update()
    {
        if (pokeInteractable != null && !pokeInteractable.enabled)
        {
            Debug.LogWarning(
                gameObject.name + " Poke Interactable is DISABLED!"
            );
        }
    }
}
