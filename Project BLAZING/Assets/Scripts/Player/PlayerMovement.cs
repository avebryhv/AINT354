using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CoreFinder finder;
    Camera mainCam;
    Rigidbody rb;
    //Movement Variables
    public float moveSpeed;
    public float xMove; //Horizontal Input
    public float zMove; //Vertical Input
    public float cameraAxis;
    public float cameraSpeed;
    public float playerWidth;
    //Evasion Variables
    public bool inEvade;
    public float evadeModifier; //Multiplier applied to speed during evade
    public float evadeTime; //Duration of evade
    float evadeX;
    float evadeZ;
    public float maxEvasionCharge;
    float evasionCharge;
    public ParticleSystem evadeParticles;
    public MeshRenderer mesh;
    //LockOn Variables
    GameObject lockedTarget;
    public bool isLocked;

    public bool inSpecialMovement;
    public bool targetable;

    //Dash Attack Variables
    bool inDashAttack;
    Vector3 dashAttackTarget;
    public float dashAttackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
        evasionCharge = maxEvasionCharge;
        finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        playerWidth = GetComponent<BoxCollider>().size.z / 2.0f;
        targetable = true;
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
                if (evasionCharge <= maxEvasionCharge)
                {
                    evasionCharge += Time.deltaTime;
                    evasionCharge = Mathf.Clamp(evasionCharge, 0, maxEvasionCharge);
                    finder.mainUI.UpdateEvadeBar(evasionCharge, maxEvasionCharge);
                }
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

    public void EvadePressed()
    {
        if (!inEvade && evasionCharge >= 1.0f)
        {
            evasionCharge -= 1.0f;
            finder.playerHealth.canTakeDamage = false;
            float x = xMove;
            float z = zMove;
            inEvade = true;
            evadeParticles.Play();
            Invoke("EndEvade", evadeTime);
            if (x == 0 && z == 0)
            {
                z = -1 * moveSpeed;
            }
            evadeX = x;
            evadeZ = z;
            finder.mainUI.UpdateEvadeBar(evasionCharge, maxEvasionCharge);
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
        evadeParticles.Stop();
        finder.playerHealth.canTakeDamage = true;
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
        rb.velocity = new Vector3();
    }

    public void TeleportBehindPressed()
    {
        if (isLocked)
        {            
            inSpecialMovement = true;
            mesh.enabled = false;
            targetable = false;
            Invoke("TeleportBehind", 1f);
        }
        
    }

    public void TeleportBehind()
    {
        mesh.enabled = true;
        Vector3 location = finder.lockOn.ReturnTargetBehind();
        transform.position = location;
        inSpecialMovement = false;
        Invoke("SetPlayerTargetable", 1f);
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

    void SetPlayerTargetable()
    {
        targetable = true;
    }
}
