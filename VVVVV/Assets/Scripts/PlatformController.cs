using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found!");
        }
    }

    public Color GetPlatformColor()
    {
        return spriteRenderer.color;
    }
}
