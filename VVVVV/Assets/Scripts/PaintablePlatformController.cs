using UnityEngine;

public class PaintablePlatformController : MonoBehaviour
{
    public string platformID;  // ID único para cada plataforma pintable
    private SpriteRenderer spriteRenderer;


 
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Si no tiene un ID, generamos uno automáticamente.
        if (string.IsNullOrEmpty(platformID))
        {
            platformID = System.Guid.NewGuid().ToString();
        }

        // Restaurar el color guardado (si existe).
        Color savedColor;
        if (PlatformStateController.TryGetPlatformColor(platformID, out savedColor))
        {
            spriteRenderer.color = savedColor;
        }
    }
   
    public Color GetPlatformColor()
    {
        return spriteRenderer.color;
    }

    public void SetPlatformColor(Color color)
    {
        spriteRenderer.color = color;

        // Guardar el estado de la plataforma pintable.
        PlatformStateController.SavePlatformState(platformID, color);
    }
}
