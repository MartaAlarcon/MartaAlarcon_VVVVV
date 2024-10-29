using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlatform : MonoBehaviour
{
    public float delayTime = 2f; 

    void Start()
    {
        gameObject.SetActive(false);

        Invoke("EnablePlatform", delayTime);
    }

    void EnablePlatform()
    {
        gameObject.SetActive(true);
    }
}
