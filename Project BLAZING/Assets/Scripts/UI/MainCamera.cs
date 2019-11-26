using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    public Vector3 positionBehindPlayer;
    public Vector3 offset;
    public float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        offset = positionBehindPlayer;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
        
    }
}
