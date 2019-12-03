using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    CoreFinder finder;
    public LayerMask crosshairLocationMask;
    public Image hitMarker;
    public float baseDistance;

    // Start is called before the first frame update
    void Start()
    {
        hitMarker.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(finder.player.transform);

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(finder.player.transform.position, finder.player.transform.forward, out hit, Mathf.Infinity, crosshairLocationMask))
        {
            SetPosition(hit.point);
            Debug.DrawRay(finder.player.transform.position, finder.player.transform.forward, Color.red, hit.distance);
            //transform.LookAt(hit.normal);
            //if (hit.distance <= 4)
            //{
            //    transform.position = transform.position + transform.forward * (hit.distance - 1f);
            //}
            //else
            //{
            transform.position = transform.position + transform.forward * 2f;
            //}
            Resize(hit.distance);

        }
    }

    public void ShowHitMarker()
    {
        hitMarker.color = new Color(1, 0, 0, 1);
        Invoke("FadeHitMarker", 0.5f);
    }

    public void FadeHitMarker()
    {
        hitMarker.color = new Color(0, 0, 0, 0);
    }

    public void SetFinder(CoreFinder f)
    {
        finder = f;
    }

    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void Resize(float dist)
    {
        if (dist <= 20)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            float scale = 1 + ( (dist - 20) * (1.5f / 100.0f));
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

}
