using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<GameObject> list;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToList(GameObject toAdd)
    {
        list.Add(toAdd);
    }

    public void RemoveFromList(GameObject toRemove)
    {
        list.Remove(toRemove);
    }

    public GameObject ReturnClosest(Vector3 pos)
    {
        float greatest = 999;
        GameObject toReturn = null;
        for (int i = 0; i < list.Count; i++)
        {
            float dist = GetDistance(pos, list[i].transform.position);
            if (dist < greatest)
            {
                greatest = dist;
                toReturn = list[i];
            }
        }
        return toReturn;
    }

    float GetDistance(Vector3 first, Vector3 second)
    {
        float dist;
        dist = Mathf.Abs((second - first).magnitude);
        return dist;
    }
}
