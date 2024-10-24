using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con im�genes de UI

public class SignController : MonoBehaviour
{
    public Image[] messages; // Array de im�genes a mostrar
    public float messageDuration = 2f; // Tiempo entre mensajes
    private bool isPlayerInRange = false; // Para saber si el personaje est� dentro del �rea del cartel
    private int currentMessageIndex = 0;  // �ndice del mensaje actual

    private void Start()
    {
        // Asegurarse de que todas las im�genes est�n ocultas al principio
        foreach (Image message in messages)
        {
            message.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Aseg�rate de que el personaje tiene el tag "Player"
        {
            isPlayerInRange = true;
            StartCoroutine(ShowMessages());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            StopAllCoroutines(); // Deja de mostrar los mensajes si el personaje sale del �rea
            HideAllMessages();   // Oculta todas las im�genes cuando el personaje sale del �rea
        }
    }

    IEnumerator ShowMessages()
    {
        // Reiniciar el �ndice de los mensajes
        currentMessageIndex = 0;

        // Mostrar cada mensaje uno a uno
        while (isPlayerInRange && currentMessageIndex < messages.Length)
        {
            // Mostrar la imagen actual
            messages[currentMessageIndex].gameObject.SetActive(true);

            // Esperar la duraci�n antes de mostrar el siguiente mensaje
            yield return new WaitForSeconds(messageDuration);

            // Ocultar la imagen actual antes de pasar a la siguiente
            messages[currentMessageIndex].gameObject.SetActive(false);

            // Pasar al siguiente mensaje
            currentMessageIndex++;
        }

        // Al terminar, asegurarse de que no quede ninguna imagen visible
        HideAllMessages();
    }

    // M�todo para ocultar todas las im�genes
    void HideAllMessages()
    {
        foreach (Image message in messages)
        {
            message.gameObject.SetActive(false);
        }
    }
}
