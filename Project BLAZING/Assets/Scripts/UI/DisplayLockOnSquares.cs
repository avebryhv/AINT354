using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLockOnSquares : MonoBehaviour
{
    public List<LockOnSquare> list;
    public GameObject lockOnSquarePrefab;
    public Camera cam;
    GameObject newSqu;

    private void Awake()
    {
        list = new List<LockOnSquare>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddToList(LockOnSquare newSqu)
    {
        list.Add(newSqu);

    }

    public void RemoveFromList(LockOnSquare toRemove)
    {
        list.Remove(toRemove);
        Destroy(toRemove);
    }

    public void CreateNewLockOn(GameObject obj, Camera c)
    {
        newSqu = Instantiate(lockOnSquarePrefab, transform);
        newSqu.GetComponent<LockOnSquare>().SetTarget(obj, c);
        AddToList(newSqu.GetComponent<LockOnSquare>());
    }

    public void RemoveLockOnSquare()
    {
        Destroy(newSqu);
    }
}
