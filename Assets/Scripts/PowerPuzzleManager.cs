
using UnityEngine;
using TMPro;
//using UnityEngine.InputSystem;

public class PowerPuzzleManager : MonoBehaviour
{
    private int currentStep = 0;

    public TMP_Text powerStatusText;

    //Cabinet door hinge
    public Transform cabinetDoorHinge;
    public float cabinetOpenAngle = 90f;

    //Main laboratory lights
    public GameObject mainLights;

    private void Start()
    {
        //Start with the power off
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

        //Make sure the cabinet starts closed
        if (cabinetDoorHinge != null)
        {
            cabinetDoorHinge.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void PressGreen()
    {
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
        if (currentStep == 2)
        {
            currentStep = 3;

            RestorePower();

            Debug.Log("POWER RESTORED!");
        }
        else
        {
            ResetPuzzle();
        }
    }

    private void RestorePower()
    {
        //Update the power status display
        if (powerStatusText != null)
        {
            powerStatusText.text = "POWER RESTORED";
            powerStatusText.color = Color.green;
        }

        //Open the cabinet
        if (cabinetDoorHinge != null)
        {
            cabinetDoorHinge.localRotation =
                Quaternion.Euler(0, cabinetOpenAngle, 0);
        }

        //Turn on the laboratory lights
        if (mainLights != null)
        {
            mainLights.SetActive(true);
        }
    }

    /*//Temporary keyboard testing - remove after testing
    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            PressGreen();
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            PressBlue();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            PressRed();
        }
    }*/

    private void ResetPuzzle()
    {
        //Keep the puzzle solved once power is restored
        if (currentStep == 3)
            return;

        currentStep = 0;

        if (powerStatusText != null)
        {
            powerStatusText.text = "POWER OFF";
            powerStatusText.color = Color.red;
        }

        Debug.Log("Wrong sequence - puzzle reset.");
    }
}
