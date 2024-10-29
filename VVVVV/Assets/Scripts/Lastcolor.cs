using UnityEngine;
using UnityEngine.SceneManagement;

public class Lastcolor : MonoBehaviour
{

    public float colorTolerance = 0.1f;

    void Update()
    {
        
        GameObject[] paintableObjects = GameObject.FindGameObjectsWithTag("Paintable");

        if (paintableObjects.Length == 0)
        {
            SceneManager.LoadScene("FinalScene");
        }
    }

   
}
