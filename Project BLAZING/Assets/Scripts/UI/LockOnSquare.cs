using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnSquare : MonoBehaviour
{
    public GameObject target;
    Canvas canvas;
    public float deltaRotate;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 screenPos = cam.ScreenToWorldPoint(target.transform.position);
            transform.position = worldToUISpace(canvas, target.transform.position);
            transform.Rotate(new Vector3(0, 0, deltaRotate));
        }
        
    }

    public void SetTarget(GameObject t, Camera c)
    {
        cam = c;
        target = t;
    }

    public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = cam.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }
    
}
