using UnityEngine;

public class LiquidFloat : MonoBehaviour
{
    [Header("Vertical Bobbing")]
    [Tooltip("How high and low the body moves in meters.")]
    public float bobAmplitude = 0.05f;
    [Tooltip("Speed of the up-and-down floating cycle.")]
    public float bobFrequency = 0.8f;

    [Header("Gentle Sway / Tilt")]
    [Tooltip("Maximum tilt angle in degrees.")]
    public float swayAngle = 2.5f;
    [Tooltip("Speed of the rotational sway.")]
    public float swayFrequency = 0.5f;

    private Vector3 startLocalPos;
    private Quaternion startLocalRot;
    private float timeOffset;

    void Start()
    {
        // Store the initial position and rotation inside the pod
        startLocalPos = transform.localPosition;
        startLocalRot = transform.localRotation;

        // Randomize phase slightly so multiple pods don't move identically
        timeOffset = Random.Range(0f, 10f);
    }

    void Update()
    {
        float t = Time.time + timeOffset;

        // Smooth up-and-down sine wave
        float newY = startLocalPos.y + Mathf.Sin(t * bobFrequency * Mathf.PI * 2f) * bobAmplitude;
        transform.localPosition = new Vector3(startLocalPos.x, newY, startLocalPos.z);

        // Subtle multi-axis tilt to mimic water currents
        float tiltX = Mathf.Sin(t * swayFrequency * Mathf.PI * 2f) * swayAngle;
        float tiltZ = Mathf.Cos(t * (swayFrequency * 0.8f) * Mathf.PI * 2f) * swayAngle;
        transform.localRotation = startLocalRot * Quaternion.Euler(tiltX, 0f, tiltZ);
    }
}
