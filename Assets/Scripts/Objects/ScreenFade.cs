using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    
    public static ScreenFade Instance;

    public Image fadeImage;
    public RectTransform rect;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator TVOff()
    {
        rect.localScale = Vector3.one;
        fadeImage.color = Color.black;

        float t = 0f;

        // collapse vertically
        while (t < 1f)
        {
            t += Time.deltaTime * 3f;
            rect.localScale = new Vector3(1f, Mathf.Lerp(1f, 0.05f, t), 1f);
            yield return null;
        }

        // flash white line
        fadeImage.color = Color.white;
        yield return new WaitForSeconds(0.1f);

        fadeImage.color = Color.black;

        // Step 3: Collapse horizontally into a dot
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 3f;
            rect.localScale = new Vector3(Mathf.Lerp(1f, 0f, t), 0.05f, 1f);
            yield return null;
        }

        // Step 4: Full black
        fadeImage.color = Color.black;
    }
}
    
   

    //public float fadeSpeed = 5f;

  

   // public IEnumerator FadeOut()
   // {
    //    float t = 0;

     //   while (t < 1)
     //   {
      //      t += Time.deltaTime * fadeSpeed;
       //     fadeImage.color = new Color(0, 0, 0, t);
       //     yield return null;
       // }
        // Squash vertically before fade
       // fadeImage.rectTransform.localScale = new Vector3(1, 0.1f, 1);
   // }

   // public IEnumerator FadeIn()
    //{
    //    float t = 1;

      //  while (t > 0)
      //  {
      //      t -= Time.deltaTime * fadeSpeed;
      //      fadeImage.color = new Color(0, 0, 0, t);
      //      yield return null;
       // }
   // }



