using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public GameObject normalModel;
    public GameObject speedModel;
    public GameObject heavyModel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetModel(PlayerMovement.mechType t)
    {
        switch (t)
        {
            case PlayerMovement.mechType.Normal:
                normalModel.SetActive(true);
                speedModel.SetActive(false);
                heavyModel.SetActive(false);
                break;
            case PlayerMovement.mechType.Fast:
                normalModel.SetActive(false);
                speedModel.SetActive(true);
                heavyModel.SetActive(false);
                break;
            case PlayerMovement.mechType.Slow:
                normalModel.SetActive(false);
                speedModel.SetActive(false);
                heavyModel.SetActive(true);
                break;
            default:
                break;
        }
    }
}
