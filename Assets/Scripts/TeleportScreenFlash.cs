using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeleportScreenFlash : MonoBehaviour
{
    public Image flashImage;

    public float flashSpeed = 2f;

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        Color color = flashImage.color;

        color.a = 1f;

        flashImage.color = color;

        while (flashImage.color.a > 0)
        {
            color.a -= Time.deltaTime * flashSpeed;

            flashImage.color = color;

            yield return null;
        }
    }
}
