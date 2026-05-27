using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;

/// <summary>
/// This script handles the "Android Corruption" mechanic. 
/// It monitors the DarknessSystem and applies signal lag, movement drift, 
/// and input glitches to the ThirdPersonController.
/// </summary>
[RequireComponent(typeof(ThirdPersonController))]
public class DarknessCorruption : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Reference to the Starter Assets Third Person Controller")]
    public ThirdPersonController controller;
    [Tooltip("Reference to the input system component")]
    public StarterAssetsInputs inputs;

    [Header("Darkness Thresholds")]
    [Tooltip("Darkness level (0-100) where corruption starts being felt")]
    public float corruptionStart = 30f;
    [Tooltip("Darkness level where movement becomes severely impaired")]
    public float heavyCorruption = 70f;
    [Tooltip("Darkness level where the system starts failing (misfires, glitches)")]
    public float criticalCorruption = 85f;

    [Header("Movement Settings")]
    [Tooltip("Maximum percentage reduction in player's move and sprint speed (0-1)")]
    [Range(0f, 1f)]
    public float maxSpeedReduction = 0.7f; // e.g., 0.7 means 70% speed reduction
    [Tooltip("Amount of random rotation twitching")]
    public float maxTurnJitter = 35f;
    [Tooltip("Chance per second for movement input to momentarily stutter (0-1)")]
    [Range(0f, 1f)]
    public float signalStutterChance = 0.1f; // 10% chance per second to stutter
    [Tooltip("Duration of a single signal stutter in seconds")]
    public float signalStutterDuration = 0.05f;
    
    [Header("Jump Corruption")]
    [Tooltip("Maximum delay for jump execution")]
    public float maxJumpDelay = 0.4f;
    [Tooltip("Chance (0-1) of a jump command failing at critical darkness")]
    public float jumpMisfireChance = 0.2f;

    // Internal state for signal lag (Buffer)
    private bool isStuttering = false;
    private bool isJumpQueued = false;
    private float originalMoveSpeed;
    private float originalSprintSpeed;
    private Vector2 rawMoveInput; // Stores the player's actual input before corruption
    private bool rawJumpInput; // Stores the player's actual jump input before corruption

    void Start()
    {
        // Auto-assign references if they are missing
        if (controller == null) controller = GetComponent<ThirdPersonController>();
        if (inputs == null) inputs = GetComponent<StarterAssetsInputs>();

        // Store original speeds to revert when darkness decreases
        if (controller != null)
        {
            originalMoveSpeed = controller.MoveSpeed;
            originalSprintSpeed = controller.SprintSpeed;
        }
    }

    void Update()
    {
        // Store raw player inputs at the beginning of the frame.
        // This ensures we always have the player's true intention, even if inputs.move/jump are modified later.
        rawMoveInput = inputs.move;
        rawJumpInput = inputs.jump;

        // Exit early if the darkness system is missing
        if (DarknessSystem.Instance == null) return;

        float darkness = DarknessSystem.Instance.darknessLevel;

        // Check if LightOrb is on. If so, reset all corruption effects immediately.
        if (LightOrb.Instance != null && LightOrb.Instance.IsLightOn)
        {
            ResetCorruptionEffects();
            return; // Skip all corruption logic if light is on
        }

        // Only process if darkness is above the initial threshold
        if (darkness >= corruptionStart)
        {
            float normalizedCorruption = Mathf.InverseLerp(corruptionStart, 100f, darkness);

            HandleSpeedDegradation(normalizedCorruption);
            HandleSignalStutter(normalizedCorruption);
            HandleRotationJitter(normalizedCorruption);
            HandleJumpCorruption(darkness, normalizedCorruption);

            // Critical glitches (UI/Visual feedback can be triggered here too)
            if (darkness >= criticalCorruption)
            {
                ApplyCriticalGlitches(normalizedCorruption);
            }
        }
        else // Revert to normal if darkness is below corruption start and light is off
        {
            ResetCorruptionEffects();
        }
    }

    /// <summary>
    /// Resets all corruption effects to their normal state.
    /// Called when the light orb is on or darkness level is below corruption start.
    /// </summary>
    void ResetCorruptionEffects()
    {
        // Restore original speeds to the ThirdPersonController
        if (controller != null)
        {
            controller.MoveSpeed = originalMoveSpeed;
            controller.SprintSpeed = originalSprintSpeed;
        }

        // Stop any active coroutines that might be modifying inputs
        if (isStuttering) StopCoroutine("SignalStutterRoutine");
        isStuttering = false;
        if (isJumpQueued) StopCoroutine("DelayedJumpRoutine");
        isJumpQueued = false;

        // Restore inputs to the player's actual raw input, ensuring immediate control
        inputs.move = rawMoveInput;
        inputs.jump = rawJumpInput;
    }

    /// <summary>
    /// Reduces the player's movement speed based on darkness level.
    /// This makes the android feel heavy and sluggish.
    /// </summary>
    void HandleSpeedDegradation(float normalized)
    {
        if (controller == null) return;

        // Calculate the current speed reduction based on normalized corruption
        float currentSpeedReduction = Mathf.Lerp(0f, maxSpeedReduction, normalized);

        // Apply the speed reduction to the controller's movement speeds
        controller.MoveSpeed = originalMoveSpeed * (1f - currentSpeedReduction);
        controller.SprintSpeed = originalSprintSpeed * (1f - currentSpeedReduction);
    }

    /// <summary>
    /// Randomly zeroes out movement input for a short duration to simulate signal stutter.
    /// This creates a "glitched" movement feel without removing player control.
    /// </summary>
    void HandleSignalStutter(float normalized)
    {
        // Only apply stutter if the player is actively providing movement input and not already stuttering
        if (rawMoveInput.magnitude > 0.1f && !isStuttering)
        {
            // Increase stutter chance with higher darkness
            float currentStutterChance = signalStutterChance * normalized;

            // Check if a stutter should occur this frame (Time.deltaTime makes it frame-rate independent)
            if (Random.value < currentStutterChance * Time.deltaTime)
            {
                StartCoroutine(SignalStutterRoutine(signalStutterDuration));
            }
        }
    }

    IEnumerator SignalStutterRoutine(float duration)
    {
        isStuttering = true;
        // Temporarily zero out the movement input to create a stutter effect
        // The ThirdPersonController will then use this zeroed input for the duration.
        inputs.move = Vector2.zero; // Zero out input during stutter

        yield return new WaitForSeconds(duration);

        // Restore the player's original input after the stutter duration
        // This ensures control is immediately given back to the player.
        inputs.move = rawMoveInput; // Restore input after stutter
        isStuttering = false;
    }

    /// <summary>
    /// Adds random rotation twitching to simulate a failing gyroscope.
    /// </summary>
    void HandleRotationJitter(float normalized)
    {
        // Only jitter if we are actually trying to move
        if (rawMoveInput.magnitude > 0.1f)
        {
            float jitter = Mathf.Lerp(0f, maxTurnJitter, normalized);
            float randomTwitch = Random.Range(-jitter, jitter);

            // Directly apply a small rotation offset to the transform
            transform.Rotate(0f, randomTwitch * Time.deltaTime, 0f);
        }
    }

    /// <summary>
    /// Delays or cancels jump inputs based on corruption levels.
    /// </summary>
    void HandleJumpCorruption(float darkness, float normalized)
    {
        if (darkness < heavyCorruption) return;

        // If the player presses jump and we haven't already queued one
        if (rawJumpInput && !isJumpQueued)
        {
            // At critical levels, there's a chance the signal just fails
            if (darkness >= criticalCorruption && Random.value < jumpMisfireChance)
            {
                Debug.Log("Jump signal corrupted - Misfire!");
                inputs.jump = false;
                return;
            }

            float delay = Mathf.Lerp(0f, maxJumpDelay, normalized);
            StartCoroutine(DelayedJumpRoutine(delay));
        }

        // Block the immediate jump signal so the coroutine can handle it
        inputs.jump = false;
    }

    IEnumerator DelayedJumpRoutine(float delay)
    {
        isJumpQueued = true;

        // Wait for the simulated lag
        yield return new WaitForSeconds(delay);

        // Inject the jump signal back into the input system for one frame
        inputs.jump = true;
        yield return null;
        inputs.jump = false;

        isJumpQueued = false;
    }

    /// <summary>
    /// Randomly scrambles or zeros out inputs for a split second.
    /// </summary>
    void ApplyCriticalGlitches(float normalized)
    {
        // Rare chance every frame to have a "Signal Dropout"
        if (Random.value < 0.01f * normalized)
        {
            // Zero out movement for a frame
            inputs.move = Vector2.zero;
        }
    }
}  


