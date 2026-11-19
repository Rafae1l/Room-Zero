
using UnityEngine;

public class KeycardReaderManager : MonoBehaviour
{
    [Header("Keycard")]
    public GameObject exitKeycard;

    [Header("Indicator")]
    public Renderer keycardIndicator;
    public Color lockedColor = Color.red;
    public Color unlockedColor = Color.green;

    [Header("Exit Door")]
    public Transform exitDoor;
    public float openAngle = 90f;

    private bool unlocked = false;
    private Quaternion closedRotation;

    private void Start()
    {
        //Remember the door's original rotation
        if (exitDoor != null)
        {
            closedRotation = exitDoor.localRotation;
        }

        //Start with the reader locked
        unlocked = false;
        UpdateIndicator(lockedColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (unlocked || exitKeycard == null)
            return;

        //Check whether the detected object is our keycard
        if (other.gameObject == exitKeycard ||
            other.transform.IsChildOf(exitKeycard.transform))
        {
            UnlockDoor();
        }
    }

    private void UnlockDoor()
    {
        unlocked = true;

        //Change indicator from red to green
        UpdateIndicator(unlockedColor);

        //Open the exit door
        if (exitDoor != null)
        {
            exitDoor.localRotation =
                closedRotation * Quaternion.Euler(0, openAngle, 0);
        }

        Debug.Log("Keycard accepted! Exit door unlocked.");
    }

    private void UpdateIndicator(Color color)
    {
        if (keycardIndicator != null)
        {
            keycardIndicator.material.color = color;
        }
    }
}
