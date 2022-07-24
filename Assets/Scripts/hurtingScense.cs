using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtingScense : MonoBehaviour
{

    private static hurtingScense instance;
    public static hurtingScense Instance
    {
        get{
            if (instance == null)
                instance = Transform.FindObjectOfType<hurtingScense>();
            return instance;
        }
    }

    private bool isShake;

    public void CameraShake(float duration, float strength)
    {
        if (!isShake)
        {
            StartCoroutine(Shake(duration, strength));
        }
    }

    IEnumerator Shake(float duration,float strength)
    {
        isShake = true;
        Transform camera = transform;
        Vector3 startPosition = camera.position;
        while (duration > 0)
        {
            GetComponent<Renderer>().enabled = ! GetComponent<Renderer>().enabled;
            camera.position = Random.insideUnitSphere * strength + startPosition;
            duration -= Time.deltaTime;
            yield return null;
        }
        GetComponent<Renderer>().enabled = false;
        camera.position = startPosition;
        isShake = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.GetComponent<Transform>().position;
    }
}
