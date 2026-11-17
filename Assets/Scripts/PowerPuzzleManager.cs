
using UnityEngine;
using TMPro;

public class PowerPuzzleManager : MonoBehaviour
{
    private int currentStep = 0;

    [Header("Power Status")]
    public TMP_Text powerStatusText;

    [Header("Lighting")]
    public GameObject mainLights;

    [Header("Security Keypad")]
    public SecurityKeypadManager securityKeypad;

    private void Start()
    {
        currentStep = 0;

        if (powerStatusText != null)
        {
            powerStatusText.text = "POWER OFF";
            powerStatusText.color = Color.red;
        }

        if (mainLights != null)
        {
            mainLights.SetActive(false);
        }
    }

    // STEP 1: Green
    public void PressGreen()
    {
        Debug.Log("green button pressed");
        if (currentStep == 3)
            return;

        if (currentStep == 0)
        {
            currentStep = 1;
            Debug.Log("Green correct! Now press Blue.");
        }
        else
        {
            ResetPuzzle();
        }
    }

    // STEP 2: Blue
    public void PressBlue()
    {
        Debug.Log("blue button pressed");
        if (currentStep == 3)
            return;

        if (currentStep == 1)
        {
            currentStep = 2;
            Debug.Log("Blue correct! Now press Red.");
        }
        else
        {
            ResetPuzzle();
        }
    }

    // STEP 3: Red
    public void PressRed()
    {
        Debug.Log("red button pressed");
        if (currentStep == 3)
            return;

        if (currentStep == 2)
        {
            currentStep = 3;
            RestorePower();
        }
        else
        {
            ResetPuzzle();
        }
    }

    private void RestorePower()
    {
        if (powerStatusText != null)
        {
            powerStatusText.text = "POWER ON";
            powerStatusText.color = Color.green;
        }

        if (mainLights != null)
        {
            mainLights.SetActive(true);
        }

        if (securityKeypad != null)
        {
            securityKeypad.ActivateKeypad();
        }

        Debug.Log("POWER RESTORED! Security keypad activated.");
    }

    private void ResetPuzzle()
    {
        currentStep = 0;
        Debug.Log("Wrong sequence! Start again with Green.");
    }
}
