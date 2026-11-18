using UnityEngine;
using UnityEngine.Events;

public class StasisPodController : MonoBehaviour
{
    public enum PodState { Unpowered, PoweredUnstable, Stabilized }

    [Header("Current Status")]
    public PodState currentState = PodState.Unpowered;

    [Header("Pod References")]
    public Renderer powerIndicatorRenderer;
    public Light podLight;
    public LiquidFloat specimenFloat;
    public ParticleSystem auraParticles;

    [Header("State Colors")]
    public Color unpoweredColor = Color.red;
    public Color unstableColor = new Color(1f, 0.5f, 0f); // Amber/Orange
    public Color stabilizedColor = Color.cyan;

    [Header("Events")]
    public UnityEvent onPowerRestored;
    public UnityEvent onSpecimenStabilized;

    private Material indicatorMat;

    void Start()
    {
        // Create a unique material instance so we don't edit the project asset
        if (powerIndicatorRenderer != null)
        {
            indicatorMat = powerIndicatorRenderer.material;
            indicatorMat.EnableKeyword("_EMISSION");
        }

        UpdatePodVisuals();
    }

    // Call this from your Power Lever, Button, or Fuse Socket
    public void RestorePower()
    {
        if (currentState != PodState.Unpowered) return;

        currentState = PodState.PoweredUnstable;
        UpdatePodVisuals();
        onPowerRestored?.Invoke();
        Debug.Log("Pod Power Restored! Specimen is unstable.");
    }

    // Call this when the Chemical Vial enters the Pod's Tube/Port
    public void InjectStabilizer()
    {
        if (currentState == PodState.Unpowered)
        {
            Debug.Log("Cannot inject chemical: Restore pod power first!");
            return;
        }

        if (currentState == PodState.Stabilized) return;

        currentState = PodState.Stabilized;
        UpdatePodVisuals();
        onSpecimenStabilized?.Invoke();
        Debug.Log("Specimen Stabilized!");
    }

    private void UpdatePodVisuals()
    {
        switch (currentState)
        {
            case PodState.Unpowered:
                SetIndicator(unpoweredColor, 0.5f);
                if (podLight != null) podLight.enabled = false;
                if (auraParticles != null) auraParticles.Stop();
                if (specimenFloat != null)
                {
                    specimenFloat.bobFrequency = 0.2f; // Nearly still
                    specimenFloat.swayAngle = 0.5f;
                }
                break;

            case PodState.PoweredUnstable:
                SetIndicator(unstableColor, 2.0f);
                if (podLight != null)
                {
                    podLight.enabled = true;
                    podLight.color = unstableColor;
                }
                if (auraParticles != null) auraParticles.Play();
                if (specimenFloat != null)
                {
                    // Fast, agitated movement while unstable
                    specimenFloat.bobFrequency = 2.2f;
                    specimenFloat.swayAngle = 7.0f;
                }
                break;

            case PodState.Stabilized:
                SetIndicator(stabilizedColor, 2.5f);
                if (podLight != null)
                {
                    podLight.enabled = true;
                    podLight.color = stabilizedColor;
                }
                if (specimenFloat != null)
                {
                    // Calm, steady floating once stabilized
                    specimenFloat.bobFrequency = 0.6f;
                    specimenFloat.swayAngle = 2.0f;
                }
                break;
        }
    }

    private void SetIndicator(Color color, float emissionIntensity)
    {
        if (indicatorMat == null) return;
        indicatorMat.color = color;
        indicatorMat.SetColor("_EmissionColor", color * emissionIntensity);
    }
}
