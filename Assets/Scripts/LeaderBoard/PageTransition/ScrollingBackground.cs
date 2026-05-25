using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public RawImage image;

    public Vector2 speed;

    void Update()
    {
        image.uvRect =
            new Rect(
                image.uvRect.position +
                speed * Time.deltaTime,
                image.uvRect.size
            );
    }
}
