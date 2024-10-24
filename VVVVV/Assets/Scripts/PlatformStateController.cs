using System.Collections.Generic;
using UnityEngine;

public class PlatformStateController : MonoBehaviour
{
    // Diccionario para almacenar el estado de las plataformas pintables.
    private static Dictionary<string, Color> platformStates = new Dictionary<string, Color>();

    // Guardar el color de una plataforma pintable.
    public static void SavePlatformState(string platformID, Color color)
    {
        if (platformStates.ContainsKey(platformID))
        {
            platformStates[platformID] = color;
        }
        else
        {
            platformStates.Add(platformID, color);
        }
    }

    // Obtener el color guardado de una plataforma pintable.
    public static bool TryGetPlatformColor(string platformID, out Color color)
    {
        return platformStates.TryGetValue(platformID, out color);
    }

    // Limpiar todos los estados (si es necesario reiniciar).
    public static void ClearPlatformStates()
    {
        platformStates.Clear();
    }
}
