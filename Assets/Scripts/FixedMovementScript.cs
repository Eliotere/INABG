using System.Data;
using UnityEngine;

public class FixedMovementScript : MonoBehaviour
{

    enum MovementMode { Sinusoidal, Linear };

    [SerializeField]
    float period;
    [SerializeField]
    Vector3 beginPosition;
    [SerializeField]
    Vector3 endPosition;
    [SerializeField]
    MovementMode movementMode = MovementMode.Sinusoidal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = beginPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            switch (movementMode)
            {
                case MovementMode.Sinusoidal:
                    rb.MovePosition(beginPosition * SinCoefficient(Time.time,period) + endPosition * (1 - SinCoefficient(Time.time,period)));
                    break;
                case MovementMode.Linear:
                    LinearCoefficient(Time.time, period);
                    //rb.MovePosition(beginPosition * 1 / period);
                    break;

            }
        }
        else
        {
            switch (movementMode)
            {
                case MovementMode.Sinusoidal:
                    transform.position = beginPosition * SinCoefficient(Time.time,period) + endPosition * (1 - SinCoefficient(Time.time, period));
                    break;
                case MovementMode.Linear:

                    transform.position = beginPosition * LinearCoefficient(Time.time, period) + endPosition * (1 - LinearCoefficient(Time.time, period));
                    break;

            }
        }
    }

    /// <summary>
    /// Returns a value that eases from 0 → 1 → 0 in a sinusoid,
    /// completing one full cycle every <paramref name="period"/> seconds.
    /// </summary>
    public static float SinCoefficient(float time, float period)
    {
        // 2π / period gives radians‑per‑second
        float phase = time * (Mathf.PI * 2f / period);

        // Mathf.Sin outputs –1…+1.  Shift & scale to 0…1.
        return (Mathf.Sin(phase) + 1f) * 0.5f;
    }

    // Ik this function is hidiousn but so am I, so it's ok (for now)
    public static float LinearCoefficient(float time, float period)
    {
        float phase = time % period / period; // Value between 0 and 1
        if (phase < 0.5)
        {
            return 1 - phase * 2;
        }
        else
        {
            return (phase - 0.5f) * 2;
        }
    }
}
