using UnityEngine;
using System.Collections;

public class SpiderController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
        }
    }

    [SerializeField]
    private float rotateSpeed = 1.0f;
    public float RotateSpeed
    {
        get
        {
            return rotateSpeed;
        }

        set
        {
            rotateSpeed = value;
        }
    }

    private Rigidbody rigidBody = null;
    private Rigidbody RigidBody
    {
        get
        {
            return rigidBody;
        }

        set
        {
            rigidBody = value;
        }
    }

    private Animator spiderAnimator = null;
    private Animator SpiderAnimator
    {
        get
        {
            return spiderAnimator;
        }

        set
        {
            spiderAnimator = value;
        }
    }
    
    private Vector3 currentNormal = Vector3.zero;
    private Vector3 CurrentNormal
    {
        get
        {
            return currentNormal;
        }

        set
        {
            currentNormal = value;
        }
    }

    private Vector3 surfaceNormal = Vector3.zero;
    private Vector3 SurfaceNormal
    {
        get
        {
            return surfaceNormal;
        }

        set
        {
            surfaceNormal = value;
        }
    }

    private Quaternion tiltToNormal;
    private Quaternion TiltToNormal
    {
        get
        {
            return tiltToNormal;
        }

        set
        {
            tiltToNormal = value;
        }
    }

    private RaycastHit rayHit;
    
    private float rotateAmount = 0.0f;
    private float RotateAmount
    {
        get
        {
            return rotateAmount;
        }

        set
        {
            rotateAmount = value;
        }
    }
    
    private bool isWalking = false;
    private bool IsWalking
    {
        get
        {
            return isWalking;
        }

        set
        {
            isWalking = value;
        }
    }


    void Start ()
    {
        RigidBody = GetComponent<Rigidbody>();
        SpiderAnimator = GetComponent<Animator>();

        GameData.IsCaught = false;
    }
	
	void Update ()
    {
        if (RigidBody)
        {
            MoveRigidBody();
        }
    }

    private void MoveRigidBody()
    {
        IsWalking = false;

        Climb();

        if (!GameData.IsGamePaused)
        {
            // translate spider
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                IsWalking = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.forward * MoveSpeed * Time.deltaTime;
                IsWalking = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * MoveSpeed * Time.deltaTime;
                IsWalking = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.position -= transform.right * MoveSpeed * Time.deltaTime;
                IsWalking = true;
            }

            // rotate spider
            RotateAmount = Input.GetAxis("Mouse X") * RotateSpeed * Time.deltaTime;
            transform.RotateAround(transform.position, transform.up, RotateAmount);

            IsWalking = RotateAmount != 0.0f ? true : IsWalking;
        }

        // make sure spider sticks to surface
        RigidBody.velocity = -transform.up * RigidBody.mass;

        // animate
        if (SpiderAnimator)
        {
            SpiderAnimator.SetBool("IsWalking", IsWalking);
        }
    }

    private void Climb()
    {
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, 1.0f)) // Check in front of spider
        {
            Debug.DrawRay(transform.position, transform.forward, Color.blue, 2f);
            SurfaceNormal = rayHit.normal;
            CurrentNormal = Vector3.Lerp(CurrentNormal, SurfaceNormal, 6.0f * Time.deltaTime);
            TiltToNormal = Quaternion.FromToRotation(transform.up, CurrentNormal) * transform.rotation;
            transform.rotation = TiltToNormal;
        }
        else if (Physics.Raycast(transform.position, -transform.up, out rayHit, 1.0f)) // Check below spider
        {
            Debug.DrawRay(transform.position, -transform.up, Color.green, 2f);
            SurfaceNormal = rayHit.normal;
            CurrentNormal = Vector3.Lerp(CurrentNormal, SurfaceNormal, 6.0f * Time.deltaTime);
            TiltToNormal = Quaternion.FromToRotation(transform.up, CurrentNormal) * transform.rotation;
            transform.rotation = TiltToNormal;
        }
        else if (Physics.Raycast(transform.position, -transform.up - transform.forward, out rayHit, 1.5f)) // Check behind spider
        {
            Debug.DrawRay(transform.position, -transform.up - transform.forward, Color.magenta, 2f);
            SurfaceNormal = rayHit.normal;
            CurrentNormal = Vector3.Lerp(CurrentNormal, SurfaceNormal, 6.0f * Time.deltaTime);
            TiltToNormal = Quaternion.FromToRotation(transform.up, CurrentNormal) * transform.rotation;
            transform.rotation = TiltToNormal;
        }
        else
        {
            CurrentNormal = Vector3.Lerp(CurrentNormal, Vector3.up, 6.0f * Time.deltaTime);
            TiltToNormal = Quaternion.FromToRotation(transform.up, CurrentNormal) * transform.rotation;
            transform.rotation = TiltToNormal;
        }
    }
}
