
using UnityEngine;
using TMPro;
//using UnityEngine.InputSystem;

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
        //Initialize the power puzzle
        currentStep = 0;

        if (powerStatusText != null)
        {
            powerStatusText.text = "POWER OFF";
            powerStatusText.color = Color.red;
        }

        //Keep main laboratory lights off initially
        if (mainLights != null)
        {
            mainLights.SetActive(false);
        }
    }

    public void PressGreen()
    {
        if (currentStep == 3)
            return;

        if (currentStep == 0)
        {
            currentStep = 1;
            Debug.Log("Green correct!");
        }
        else
        {
            ResetPuzzle();
        }
    }

    public void PressBlue()
    {
        if (currentStep == 3)
            return;

        if (currentStep == 1)
        {
            currentStep = 2;
            Debug.Log("Blue correct!");
        }
        else
        {
            ResetPuzzle();
        }
    }

    public void PressRed()
    {
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
        //Update the power monitor
        if (powerStatusText != null)
        {
            powerStatusText.text = "POWER RESTORED";
            powerStatusText.color = Color.green;
        }

        //Turn on the main laboratory lights
        if (mainLights != null)
        {
            mainLights.SetActive(true);
        }

        //Activate Puzzle 2
        if (securityKeypad != null)
        {
            securityKeypad.ActivateKeypad();
        }

        Debug.Log("POWER RESTORED! Security keypad activated.");
    }

    private void ResetPuzzle()
    {
        currentStep = 0;

        if (powerStatusText != null)
        {
            powerStatusText.text = "POWER OFF";
            powerStatusText.color = Color.red;
        }

        Debug.Log("Wrong sequence - puzzle reset.");
    }
}
