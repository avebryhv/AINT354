using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Camera mainCam;
    Rigidbody rb;
    //Movement Variables
    public float moveSpeed;
    public float xMove; //Horizontal Input
    public float zMove; //Vertical Input
    public float cameraAxis;
    public float cameraSpeed;
    //Evasion Variables
    public bool inEvade;
    public float evadeModifier; //Multiplier applied to speed during evade
    public float evadeTime; //Duration of evade
    float evadeX;
    float evadeZ;
    //LockOn Variables
    GameObject lockedTarget;
    public bool isLocked;

    bool inSpecialMovement;

    //Dash Attack Variables
    bool inDashAttack;
    Vector3 dashAttackTarget;
    public float dashAttackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        GetInput();
        if (!inSpecialMovement)
        {
            if (inEvade)
            {
                Evade(evadeX, evadeZ);
            }
            else
            {
                Move(xMove, zMove);
            }
            Rotate(cameraAxis);
        }
        else
        {
            SpecialMovement();
        }
        

    }

    void GetInput()
    {
        //Get Movement Axis
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        xMove = horizontalAxis * moveSpeed;
        zMove = verticalAxis * moveSpeed;

        //Check diagonal fasts
        Vector3 diagonal = new Vector3(horizontalAxis, 0, verticalAxis);
        if (diagonal.magnitude > 1)
        {
            zMove = zMove / diagonal.magnitude;
            xMove = xMove / diagonal.magnitude;
        }

        //Get Rotation Axis
        cameraAxis = Input.GetAxis("CameraX");
        if (cameraAxis != 0)
        {
            GetComponent<LockOn>().EndLock();
        }

        if (Input.GetButtonDown("Evade"))
        {
            EvadePressed(xMove, zMove);
        }
    }


    void Move(float x, float z)
    {
        Vector3 xVec = x * transform.right;
        Vector3 zVec = z * transform.forward;
        
        rb.velocity = (xVec + zVec);
    }

    void Rotate(float axis)
    {
        if (isLocked)
        {
            transform.LookAt(lockedTarget.transform);
        }
        else if (!inEvade)
        {
            Vector3 rotateAngle = new Vector3(0, axis * cameraSpeed, 0);
            Quaternion deltaRotate = Quaternion.Euler(rotateAngle * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotate);
        }
        
    }

    void EvadePressed(float x, float z)
    {
        if (!inEvade)
        {
            inEvade = true;
            Invoke("EndEvade", evadeTime);
            if (x == 0 && z == 0)
            {
                z = -1 * moveSpeed;
            }
            evadeX = x;
            evadeZ = z;
        }
    }

    void Evade(float x, float z)
    {       

        Vector3 xVec = x * transform.right;
        Vector3 zVec = z * transform.forward;

        rb.velocity = (xVec + zVec) * 2;

    }

    void EndEvade()
    {
        inEvade = false;
    }

    public void SetLockOn(GameObject target)
    {
        isLocked = true;
        lockedTarget = target;
    }

    public void RemoveLock()
    {
        isLocked = false;
        lockedTarget = null;
    }

    void SpecialMovement()
    {

    }

    public void StartDash(Vector3 target)
    {
        //dashAttackTarget = target;
        //inDashAttack = true;
        //transform.LookAt(dashAttackTarget);
        //moveDistance = Vector3.Distance
    }

    void DashAttack()
    {
        transform.LookAt(dashAttackTarget);
    }
}
