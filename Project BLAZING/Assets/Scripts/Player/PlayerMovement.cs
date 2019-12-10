using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CoreFinder finder;
    Camera mainCam;
    Rigidbody rb;
    public bool isPlayer1;
    public enum mechType { Normal, Fast, Slow};
    public mechType type;
    //Movement Variables
    public float moveSpeed;
    float horizontalAxis;
    float verticalAxis;

    public float xMove; //Horizontal Input
    public float zMove; //Vertical Input
    public float hMove;
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
    public LayerMask wallLayer;
    //LockOn Variables
    GameObject lockedTarget;
    public bool isLocked;

    public bool inSpecialMovement;
    public bool targetable;

    //Dash Attack Variables
    bool inDashAttack;
    Vector3 dashAttackTarget;
    public float dashAttackSpeed;
    public int dashDirection;

    //Materials
    public Material speedCamoTest;
    Material baseMaterial;
    public Material damageMaterial;

    //Boost Variables
    bool inBoost;
    public float boostSpeed;
    public float boostChargeMax;
    float boostCharge;
    public float boostChargeCooldown;
    bool canBoost;
    public float boostChargeRate;
    public float boostDrainRate;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
        evasionCharge = maxEvasionCharge;
        //finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        playerWidth = GetComponent<BoxCollider>().size.z / 2.0f;
        targetable = true;
        if (type == null)
        {
            type = mechType.Normal;
        }
        //SetTypeStats(type);
        baseMaterial = mesh.material;
        canBoost = true;
        boostCharge = boostChargeMax;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameFunctions.isPaused)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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
                        evasionCharge += Time.deltaTime * 0.5f;
                        evasionCharge = Mathf.Clamp(evasionCharge, 0, maxEvasionCharge);
                        finder.mainUI.UpdateEvadeBar(evasionCharge, maxEvasionCharge);
                    }
                    HandleBoost();
                    if (inBoost)
                    {
                        Boost(xMove, zMove);
                    }
                    else
                    {
                        Move(xMove, zMove);
                    }
                    
                }
                Rotate(cameraAxis);
            }
            else
            {
                SpecialMovement();
            }


        }

    }

    public void RecieveAxisInput(float hor, float ver, float camX)
    {
        horizontalAxis = hor;
        verticalAxis = ver;
        cameraAxis = camX;
    }

    void GetInput()
    {
        //Get Movement Axis
        //horizontalAxis = Input.GetAxis("Horizontal");
        //verticalAxis = Input.GetAxis("Vertical");

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
        //cameraAxis = Input.GetAxis("CameraX");
        //if (cameraAxis != 0)
        //{
        //    GetComponent<LockOn>().EndLock();
        //}

        // Get Height Axis
        //float heightAxis = Input.GetAxis("HeightAxis");
        //hMove = heightAxis * moveSpeed;

        
    }


    void Move(float x, float z, float h)
    {
        Vector3 xVec = x * transform.right;
        Vector3 zVec = z * transform.forward;
        Vector3 hVec = h * transform.up;
        
        rb.velocity = (xVec + zVec + hVec);
    }

    void Move(float x, float z)
    {
        Vector3 xVec = x * transform.right;
        Vector3 zVec = z * transform.forward;
        

        rb.velocity = (xVec + zVec);
    }

    void Boost(float x, float z)
    {
        Vector3 xVec = x * transform.right * boostSpeed;
        Vector3 zVec = z * transform.forward * boostSpeed;


        rb.velocity = (xVec + zVec);
    }

    void Rotate(float axis)
    {
        if (isLocked)
        {
            //Vector3 toLookAt = lockedTarget.transform.position;
            //toLookAt.Set(toLookAt.x, transform.position.y, toLookAt.z);
            //transform.LookAt(toLookAt);
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
        //if (!inEvade && evasionCharge >= 1.0f)
        //{
        //    evasionCharge -= 1.0f;
        //    finder.playerHealth.canTakeDamage = false;
        //    float x = xMove;
        //    float z = zMove;
        //    inEvade = true;
        //    evadeParticles.Play();
        //    Invoke("EndEvade", evadeTime);
        //    if (x == 0 && z == 0)
        //    {
        //        z = -1 * moveSpeed;
        //    }
        //    evadeX = x;
        //    evadeZ = z;
        //    finder.mainUI.UpdateEvadeBar(evasionCharge, maxEvasionCharge);
        //    mesh.material = speedCamoTest;
        //}

        //Evade adjusted for boost movement
        if (!inEvade && boostCharge == boostChargeMax)
        {
            boostCharge = 0;
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
            mesh.material = speedCamoTest;
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
        ResetMaterial();
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
        DashAttack();
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

    public void DashPressed()
    {
        if (isLocked && evasionCharge == maxEvasionCharge)
        {
            inSpecialMovement = true;
            evasionCharge = 0;
            finder.mainUI.UpdateEvadeBar(evasionCharge, maxEvasionCharge);
            float dir = Input.GetAxis("Horizontal");            
            if (dir != 0)
            {
                dashDirection = (int)Mathf.Sign(dir);
            }
            else
            {
                dashDirection = 0;
            }
            evadeParticles.Play();
            dashAttackTarget = finder.lockOn.ReturnTargetHorizontalPoint(dashDirection);
        }
        
        
    }

    void DashAttack()
    {
        transform.LookAt(dashAttackTarget);
        dashAttackTarget = finder.lockOn.ReturnTargetHorizontalPoint(dashDirection);
        transform.position = Vector3.MoveTowards(transform.position, dashAttackTarget, dashAttackSpeed * Time.deltaTime);
        float dist = Vector3.Distance(transform.position, lockedTarget.transform.position);
        if (dist <= 3f)
        {
            StopDash();
        }
    }

    void StopDash()
    {
        inSpecialMovement = false;
        evadeParticles.Stop();
    }

    void SetPlayerTargetable()
    {
        targetable = true;
    }

    public void SetTypeStats(mechType t)
    {
        switch (t)
        {
            case mechType.Normal:
                moveSpeed = 20;
                maxEvasionCharge = 2;
                finder.playerHealth.SetMaxHealth(15);                
                break;
            case mechType.Fast:
                moveSpeed = 25;
                maxEvasionCharge = 3;
                finder.playerHealth.SetMaxHealth(10);
                break;
            case mechType.Slow:
                moveSpeed = 15;
                maxEvasionCharge = 1;
                finder.playerHealth.SetMaxHealth(20);
                break;
            default:
                break;
        }
        finder.playerGun.SetStats(t);
        finder.playerGun2.SetStats(t);
        finder.shoulderWeapon.SetStats(t);
        finder.mainUI.UpdateEvadeBar(evasionCharge, maxEvasionCharge);
        type = t;
    }

    public void SpeedCamoTest()
    {
        mesh.material = speedCamoTest;
    }

    public void CamoButtonPressed()
    {
        finder.playerHealth.canSpecialCharge = false;
        mesh.material = speedCamoTest;
        targetable = false;
        Invoke("RemoveCamo", 5f);
    }

    void RemoveCamo()
    {
        finder.playerHealth.canSpecialCharge = true;
        targetable = true;
        ResetMaterial();
    }

    public void ResetMaterial()
    {
        mesh.material = baseMaterial;
    }

    public void DamageMaterial()
    {
        mesh.material = damageMaterial;
    }

    public void BoostButtonPressed()
    {
        if (canBoost)
        {
            inBoost = true;
        }
    }

    public void BoostButtonReleased()
    {
        inBoost = false;
    }

    void HandleBoost()
    {
        if (inBoost)
        {
            evadeParticles.Play();
            boostCharge -= Time.deltaTime * boostDrainRate;
            if (boostCharge <= 0)
            {
                canBoost = false;
                inBoost = false;
                Invoke("SetBoostCooldown", boostChargeCooldown);
            }
        }
        else
        {
            boostCharge += Time.deltaTime * boostChargeRate;
            boostCharge = Mathf.Clamp(boostCharge, 0, boostChargeMax);
            evadeParticles.Stop();
        }
        finder.mainUI.UpdateEvadeBar(boostCharge, boostChargeMax);
    }

    void SetBoostCooldown()
    {
        canBoost = true;
    }

    public void SetFinder(CoreFinder f)
    {
        finder = f;
    }

    public void HideWalls()
    {
        Vector3 camLocation = finder.playerCam.transform.position;
        Vector3 rayDirection = camLocation - transform.position;
        float rayLength = Vector3.Distance(transform.position, camLocation);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayDirection, out hit, rayLength, wallLayer))
        {
            //Debug.Log("hit wall");
            //Destroy(gameObject);
            if (hit.collider.gameObject.tag == "Player" && !finder.playerMovement.isPlayer1)
            {
                Debug.Log("hit");
                hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(8);
            }
            else if (hit.collider.gameObject.tag == "Player2" && finder.playerMovement.isPlayer1)
            {
                Debug.Log("hit");
                hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(8);
            }
            else if (hit.collider.gameObject.tag == "Wall")
            {

            }

        }
    }

}
