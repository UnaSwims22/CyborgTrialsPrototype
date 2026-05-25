using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BadgeUI : MonoBehaviour
{
    public Image badgeIcon;
    public TMP_Text badgeName;
    //public Image badgeGlow;
    public Transform visualRoot;

    public void SetupBadge(
        Sprite icon,
        string title,
        Color glowColor)
    {
        badgeIcon.sprite = icon;

        badgeName.text = title;

       //badgeGlow.color = glowColor;
    }

    private void OnEnable()
    {
        StartCoroutine(AnimateBadge());
    }

    IEnumerator AnimateBadge()
    {
        visualRoot.localScale = Vector3.zero;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 5f;

            visualRoot.localScale =
                Vector3.Lerp(
                    Vector3.zero,
                    Vector3.one,
                    t
                );

            yield return null;
        }
    }
}
