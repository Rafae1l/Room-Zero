using UnityEngine;

public class PowerPuzzleManager : MonoBehaviour
{
    private int currentStep = 0;

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
            Debug.Log("POWER RESTORED!");
        }
        else
        {
            ResetPuzzle();
        }
    }

    private void ResetPuzzle()
    {
        currentStep = 0;
        Debug.Log("Wrong sequence - puzzle reset.");
    }
}