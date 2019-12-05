using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWarning : MonoBehaviour
{
    public float targetY;
    public bool isRising;
    Vector3 currentPos;
    public ArenaFireBox fire;
    public float riseTime;
    float counter;
    MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
        counter = 0;
        isRising = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameFunctions.isPaused)
        {
            if (isRising)
            {
                //currentPos = transform.position;
                //currentPos = Vector3.Lerp(currentPos, new Vector3(currentPos.x, currentPos.y, currentPos.z), Time.deltaTime);

                counter += Time.deltaTime;
                if (counter >= riseTime)
                {
                    fire.Activate();
                    mesh.enabled = false;
                    isRising = false;
                }
            }
        }
        
    }

    public void SetRising()
    {
        isRising = true;
        mesh.enabled = true;
    }
}
