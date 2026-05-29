using UnityEngine;
using System.Collections;

public class DynamicTeleportationEffect : MonoBehaviour
{
    [Header("Portal")]
    public ParticleSystem portalParticles; // Assign your TeleportationPortal_PS here

    [Header("Portal Colours")]
    public Color idleColor = new Color(0f, 0f, 0.588f, 1f); // Dark Blue (R:0, G:0, B:150)
    public Color playerInZoneColor = new Color(0f, 1f, 1f, 1f); // Cyan
    public Color activationColor = new Color(1f, 0.647f, 0f, 1f); // Orange

    [Header("Portal Systems")]
    public float transitionSpeed = 2f; // How fast colors transition

    private Coroutine colorChangeCoroutine;
    private Coroutine colorRoutine;

    void Start()
    {

        SetColor(idleColor);
        // if (portalParticleSystem == null)
        // {
        //   Debug.LogError("Portal Particle System not assigned to DynamicTeleportationEffect.");
        //  enabled = false; // Disable script if no particle system
        // return;
        //}
        //SetParticleSystemColor(defaultColor);
    }

    // Call this when the player enters the trigger zone
    public void OnPlayerEnterZone()
    {
        StartColorTransition(playerInZoneColor);

        SetEmission(80);
        // if (colorChangeCoroutine != null) StopCoroutine(colorChangeCoroutine);
        //colorChangeCoroutine = StartCoroutine(TransitionColor(playerInZoneColor));
    }

    // Call this when the player exits the trigger zone
    public void OnPlayerExitZone()
    {
        StartColorTransition(idleColor);

        SetEmission(40);

        //if (colorChangeCoroutine != null) StopCoroutine(colorChangeCoroutine);
        //colorChangeCoroutine = StartCoroutine(TransitionColor(defaultColor));
    }

    public void OnTeleportActivated()
    {
        StartColorTransition(activationColor);

        SetEmission(200);
    }

    private void StartColorTransition(Color target)
    {
        if (colorRoutine != null)
        {
            StopCoroutine(colorRoutine);
        }

        colorRoutine = StartCoroutine(ColorTransition(target));
    }

    private IEnumerator ColorTransition(Color target)
    {
        var main = portalParticles.main;

        Color startColor = main.startColor.color;

        float timer = 0f;

        while (timer < 1f)
        {
            Color current = Color.Lerp(startColor, target, timer);

            main.startColor = current;

            timer += Time.deltaTime * transitionSpeed;

            yield return null;
        }

        main.startColor = target;
    }

    private void SetColor(Color color)
    {
        var main = portalParticles.main;

        main.startColor = color;
    }

    private void SetEmission(float amount)
    {
        var emission = portalParticles.emission;

        emission.rateOverTime = amount;
    }

    // Call this when the player presses the activation key
    public void OnActivationKeyPressed()
    {
        if (colorChangeCoroutine != null) StopCoroutine(colorChangeCoroutine);
        colorChangeCoroutine = StartCoroutine(ColorTransition(activationColor));
    }

    //private void SetParticleSystemColor(Color targetColor)
    //{
     //   var main = portalParticleSystem.main;
      //  main.startColor = targetColor;
    //}

   // private IEnumerator TransitionColor(Color targetColor)
    //{
    //    var main = portalParticleSystem.main;
    //    Color currentColor = main.startColor.color;
      //  float timer = 0f;

      //  while (timer < colorChangeDuration)
      //  {
       //     main.startColor = Color.Lerp(currentColor, targetColor, timer / colorChangeDuration);
       //     timer += Time.deltaTime;
        //    yield return null;
        //}
        //main.startColor = targetColor; // Ensure final color is set precisely
   // }
}
