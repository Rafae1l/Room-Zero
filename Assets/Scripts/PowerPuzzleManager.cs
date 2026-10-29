using UnityEngine;
using TMPro;
//using UnityEngine.InputSystem;

public class PowerPuzzleManager : MonoBehaviour
{
    private int currentStep = 0;

    public TMP_Text powerStatusText;

    //Drag CabinetDoorHinge into this field in the Inspector
    public Transform cabinetDoorHinge;

    //How far the cabinet door opens
    public float cabinetOpenAngle = 90f;

    public void PressBlue()
    {
        if (currentStep == 0)
        {
            currentStep = 1;
            Debug.Log("Blue correct!");
        }
        else
        {
            ResetPuzzle();
        }
    }

    public void PressRed()
    {
        if (currentStep == 1)
        {
            currentStep = 2;
            Debug.Log("Red correct!");
        }
        else
        {
            ResetPuzzle();
        }
    }

    public void PressGreen()
    {
        if (currentStep == 2)
        {
            currentStep = 3;

            powerStatusText.text = "POWER RESTORED";
            powerStatusText.color = Color.green;

            OpenCabinet();

            Debug.Log("POWER RESTORED!");
        }
        else
        {
            ResetPuzzle();
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

    /*
    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            PressBlue();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            PressRed();
        }

        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            PressGreen();
        }
    }
    */

    private void ResetPuzzle()
    {
        currentStep = 0;

        powerStatusText.text = "POWER OFF";
        powerStatusText.color = Color.red;

        Debug.Log("Wrong sequence - puzzle reset.");
    }
}