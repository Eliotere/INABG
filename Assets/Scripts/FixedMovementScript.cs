using UnityEngine;

public class FixedMovementScript : MonoBehaviour
{

    [SerializeField]
    float period;
    [SerializeField]
    Vector3 beginPosition;
    [SerializeField]
    Vector3 endPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = beginPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = beginPosition * SinCoefficient(period) + endPosition * (1 - SinCoefficient(period));
    }
    
    /// <summary>
    /// Returns a value that eases from 0 → 1 → 0 in a sinusoid,
    /// completing one full cycle every <paramref name="period"/> seconds.
    /// </summary>
    public static float SinCoefficient(float period)
    {
        // 2π / period gives radians‑per‑second
        float phase = Time.time * (Mathf.PI * 2f / period);

        // Mathf.Sin outputs –1…+1.  Shift & scale to 0…1.
        return (Mathf.Sin(phase) + 1f) * 0.5f;
    }
}
