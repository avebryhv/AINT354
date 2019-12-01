using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLockOnSquares : MonoBehaviour
{
    public List<LockOnSquare> list;
    public GameObject lockOnSquarePrefab;
    public Camera cam;
    GameObject newSqu;
    LockOnSquare squScript;

    private void Awake()
    {
        list = new List<LockOnSquare>();
    }

    // Start is called before the first frame update
    void Start()
    {
        newSqu = Instantiate(lockOnSquarePrefab, transform);
        squScript = newSqu.GetComponent<LockOnSquare>();
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
        //newSqu = Instantiate(lockOnSquarePrefab, transform);
        squScript.SetTarget(obj, c);
        //AddToList(newSqu.GetComponent<LockOnSquare>());
    }

    public void ShowLockOnSquare()
    {
        squScript.Show();
    }

    public void RemoveLockOnSquare()
    {
        squScript.Hide();
        UpdateColour(false);
    }

    public void UpdateSquareSize(float size)
    {
        squScript.SetSquareSize(size);
    }

    public void UpdateColour(bool isLock)
    {
        squScript.SetLockedColour(isLock);
    }
}
