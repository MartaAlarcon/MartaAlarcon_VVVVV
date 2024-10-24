using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner spawner;
    private float time = 2f;
    public GameObject Paint;
    public GameObject SpawnPoint;
    private Coroutine coroutine;
    public Stack<GameObject> stack;
    // Start is called before the first frame update
    void Start()
    {
        if (Spawner.spawner != null && Spawner.spawner != this)
        {
            Destroy(gameObject);
        }
        Spawner.spawner = this;
        stack = new Stack<GameObject>();
        coroutine = StartCoroutine(SpawnPaint());
    }

    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        stack.Push(obj);
    }
    public GameObject Pop()
    {
        GameObject obj = stack.Pop();
        obj.SetActive(true);
        obj.transform.position = SpawnPoint.transform.position;
        return obj;
    }
    public GameObject Peek()
    {
        return stack.Peek();
    }
    private IEnumerator SpawnPaint()
    {
        if (stack.Count !=0 )
        {
            Pop();
        }
        else
        {
            Instantiate(Paint, SpawnPoint.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(time);
        yield return SpawnPaint();
    }
}
