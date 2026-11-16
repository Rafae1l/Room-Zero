
using UnityEngine;
using TMPro;

public class SecurityKeypadManager : MonoBehaviour
{
    [Header("Keypad Settings")]
    public string correctCode = "4729";
    public TMP_Text keypadDisplayText;

    [Header("Cabinet")]
    public Transform cabinetDoorHinge;
    public float cabinetOpenAngle = 90f;

    private string enteredCode = "";
    private bool powerRestored = false;
    private bool unlocked = false;

    private void Start()
    {
        //Keep the keypad locked until power is restored
        powerRestored = false;
        unlocked = false;
        enteredCode = "";

        UpdateDisplay("POWER OFF");
    }

    public void ActivateKeypad()
    {
        if (powerRestored)
            return;

        powerRestored = true;
        enteredCode = "";

        UpdateDisplay("ENTER CODE");
        Debug.Log("Security keypad activated!");
    }

    public void PressDigit(string digit)
    {
        if (!powerRestored || unlocked)
            return;

        if (digit.Length != 1 || !char.IsDigit(digit[0]))
            return;

        if (enteredCode.Length >= 4)
            return;

        enteredCode += digit;

        //Show entered digits
        UpdateDisplay(enteredCode);

        Debug.Log("Keypad input: " + enteredCode);
    }

    public void ClearCode()
    {
        if (!powerRestored || unlocked)
            return;

        enteredCode = "";
        UpdateDisplay("ENTER CODE");

        Debug.Log("Keypad cleared.");
    }

    public void SubmitCode()
    {
        if (!powerRestored || unlocked)
            return;

        if (enteredCode == correctCode)
        {
            unlocked = true;

            UpdateDisplay("ACCESS GRANTED");
            OpenCabinet();

            Debug.Log("Correct code! Cabinet unlocked.");
        }
        else
        {
            UpdateDisplay("INVALID CODE");
            enteredCode = "";

            Debug.Log("Incorrect security code.");
        }
    }

    private void OpenCabinet()
    {
        if (cabinetDoorHinge != null)
        {
            cabinetDoorHinge.localRotation =
                Quaternion.Euler(0, cabinetOpenAngle, 0);
        }
    }

    private void UpdateDisplay(string message)
    {
        if (keypadDisplayText != null)
        {
            keypadDisplayText.text = message;
        }
    }
}
