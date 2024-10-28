using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float time;
    public float speed = 2f; 

    private GameObject place;
     void Start()
    {
        UpdatePlace();
        StartCoroutine(Patrol());
    }

    private void UpdatePlace()
    {
        if (place == null)
        {
            place = new GameObject("ObjPlace");
            place.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1,1, 1);    
            return;
        }
        if (place.transform.position.x == minX)
        {
            place.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1,1, 1);
        }
        else if (place.transform.position.x == maxX)
        {
            place.transform.position = new Vector2 (minX, transform.position.y);
            transform.localScale = new Vector3 (-1,1, 1);
        }
    }
    private IEnumerator Patrol()
    {
        while (Vector2.Distance(transform.position, place.transform.position) > 0.05f)
        {
            Vector2 direction = place.transform.position - transform.position;
            float xDirection = direction.x;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
            yield return null;
        }
        transform.position = new Vector2(place.transform.position.x,transform.position.y);
        yield return new WaitForSeconds(time);
        UpdatePlace();
        StartCoroutine(Patrol());
    }
}
