using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject mainCamera; 
    public GameObject secondCamera; 
    private GameObject activeCamera;
    void Start()
    {
        activeCamera = mainCamera;
        activeCamera.SetActive(true);
        secondCamera.SetActive(false);
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeCamera();
        }
    }
  
    void ChangeCamera()
    {
        activeCamera.SetActive(false);
        activeCamera = activeCamera == mainCamera ? secondCamera : mainCamera;
        activeCamera.SetActive(true);
    }  
}
