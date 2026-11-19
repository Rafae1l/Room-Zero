using UnityEngine;
using UnityEngine.Events;

public class SpecimenController : MonoBehaviour
{
    [Header("Ring Light Visuals")]
    [SerializeField] private Renderer ringLightRenderer;
    [SerializeField] private Color unpoweredColor = Color.red;
    [SerializeField] private Color poweredColor = Color.yellow;
    [SerializeField] private Color stabilizedColor = Color.green;

    [Header("Puzzle States")]
    public bool hasPower = false;
    public bool isStabilized = false;

    [Header("Events")]
    public UnityEvent onPowerRestored;
    public UnityEvent onSpecimenStabilized;

    private Material ringMaterial;

    private void Start()
    {
        // Create a material instance so we don't overwrite the project asset
        if (ringLightRenderer != null)
        {
            ringMaterial = ringLightRenderer.material;
            ringMaterial.EnableKeyword("_EMISSION");
            UpdateRingColor(unpoweredColor);
        }
    }

    // Call this from Room 1 when the circuit puzzle is solved
    public void RestorePower()
    {
        if (hasPower) return;

        hasPower = true;
        UpdateRingColor(poweredColor);
        onPowerRestored?.Invoke();
        Debug.Log("Specimen power restored!");
    }

    // Call this when the mixed chemical beaker snaps into the specimen tube
    public void InsertStabilizer()
    {
        // Optional: Require power before stabilizing
        if (!hasPower)
        {
            Debug.Log("Cannot stabilize: Specimen has no power!");
            return;
        }

        if (isStabilized) return;

        isStabilized = true;
        UpdateRingColor(stabilizedColor);
        onSpecimenStabilized?.Invoke();
        Debug.Log("Specimen stabilized! Escape door unlocked!");
    }

    private void UpdateRingColor(Color targetColor)
    {
        if (ringMaterial != null)
        {
            ringMaterial.color = targetColor;
            ringMaterial.SetColor("_EmissionColor", targetColor * 2f); // * 2f gives a nice VR glow
        }
    }

    // --- TESTING SHORTCUTS (Remove before final build) ---
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) RestorePower();
        if (Input.GetKeyDown(KeyCode.Alpha2)) InsertStabilizer();
    }
}
