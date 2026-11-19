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
    public int chemicalsInserted = 0;
    public int requiredChemicals = 3;
    public bool isStabilized = false;

    [Header("Events")]
    public UnityEvent onPowerRestored;
    public UnityEvent onSpecimenStabilized;

    private Material ringMaterial;

    private void Start()
    {
        if (ringLightRenderer != null)
        {
            ringMaterial = ringLightRenderer.material;
            ringMaterial.EnableKeyword("_EMISSION");
            UpdateRingColor(unpoweredColor);
        }
    }

    // Called by Room 1 (Circuit Puzzle)
    public void RestorePower()
    {
        if (hasPower) return;

        hasPower = true;
        UpdateRingColor(poweredColor);
        onPowerRestored?.Invoke();
        Debug.Log("Specimen power restored!");
        CheckStabilization();
    }

    // Called each time one of the 3 chemicals snaps into a tube
    public void AddChemical()
    {
        chemicalsInserted++;
        Debug.Log($"Chemical added! ({chemicalsInserted}/{requiredChemicals})");
        CheckStabilization();
    }

    // Optional: Call this in WhenUnselect() if the player pulls a vial back out
    public void RemoveChemical()
    {
        chemicalsInserted = Mathf.Max(0, chemicalsInserted - 1);
        Debug.Log($"Chemical removed! ({chemicalsInserted}/{requiredChemicals})");
    }

    private void CheckStabilization()
    {
        if (isStabilized) return;

        if (hasPower && chemicalsInserted >= requiredChemicals)
        {
            isStabilized = true;
            UpdateRingColor(stabilizedColor);
            onSpecimenStabilized?.Invoke();
            Debug.Log("Specimen stabilized! Escape door unlocked!");
        }
    }

    private void UpdateRingColor(Color targetColor)
    {
        if (ringMaterial != null)
        {
            ringMaterial.color = targetColor;
            ringMaterial.SetColor("_EmissionColor", targetColor * 2f);
        }
    }

    // --- TESTING SHORTCUTS ---
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) RestorePower();
        if (Input.GetKeyDown(KeyCode.Alpha2)) AddChemical();
    }
}
