using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerRumbleInterval : MonoBehaviour
{
    [Range(0f,1f)] public float lowFrequency = 0.2f;
    [Range(0f,1f)] public float highFrequency = 0.06f;

    public float rumbleDuration = 0.2f;
    public float interval = 3f;

    private Gamepad pad;
    private float rumbleTimer = 0f;
    private float intervalTimer = 0f;

    void Update()
    {
        pad = Gamepad.current;

        if (pad == null)
            return;

        intervalTimer += Time.deltaTime;

        // Trigger rumble every 3 seconds
        if (intervalTimer >= interval)
        {
            rumbleTimer = rumbleDuration;
            intervalTimer = 0f;
        }

        if (rumbleTimer > 0f)
        {
            rumbleTimer -= Time.deltaTime;

            float t = rumbleTimer / rumbleDuration;
            float fade = t * t; // starts and ends with a fade

            pad.SetMotorSpeeds(lowFrequency * fade, highFrequency * fade);
        }
        else
        {
            pad.SetMotorSpeeds(0f, 0f);
        }
    }

    void OnDisable()
    {
        if (pad != null)
            pad.SetMotorSpeeds(0f, 0f);
    }
}