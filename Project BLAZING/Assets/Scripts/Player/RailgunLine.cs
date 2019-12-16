using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RailgunLine : MonoBehaviour
{
    public float destroyTime;
    float destroyCounter;
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        destroyCounter += Time.deltaTime;
        if (destroyCounter >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetLocations(Vector3 l1, Vector3 l2)
    {
        line.SetPosition(0, l1);
        line.SetPosition(1, l2);
    }
}
