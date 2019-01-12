using UnityEngine;
using System.Collections;

public class RandomRotate : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed = 1.0f;
    public float TurnSpeed
    {
        get
        {
            return turnSpeed;
        }

        set
        {
            turnSpeed = value;
        }
    }

    [SerializeField]
    [Range(-360.0f,360.0f)]
    private float minXAngle = 0.0f;
    public float MinXAngle
    {
        get
        {
            return minXAngle;
        }

        set
        {
            minXAngle = value;
        }
    }

    [SerializeField]
    [Range(-360.0f, 360.0f)]
    private float maxXAngle = 0.0f;
    public float MaxXAngle
    {
        get
        {
            return maxXAngle;
        }

        set
        {
            maxXAngle = value;
        }
    }

    [SerializeField]
    [Range(-360.0f, 360.0f)]
    private float minYAngle = 0.0f;
    public float MinYAngle
    {
        get
        {
            return minYAngle;
        }

        set
        {
            minYAngle = value;
        }
    }

    [SerializeField]
    [Range(-360.0f, 360.0f)]
    private float maxYAngle = 0.0f;
    public float MaxYAngle
    {
        get
        {
            return maxYAngle;
        }

        set
        {
            maxYAngle = value;
        }
    }
    
    [SerializeField]
    [Range(-360.0f, 360.0f)]
    private float minZAngle = 0.0f;
    public float MinZAngle
    {
        get
        {
            return minZAngle;
        }

        set
        {
            minZAngle = value;
        }
    }

    [SerializeField]
    [Range(-360.0f, 360.0f)]
    private float maxZAngle = 0.0f;
    public float MaxZAngle
    {
        get
        {
            return maxZAngle;
        }

        set
        {
            maxZAngle = value;
        }
    }

    private Vector3 targetDirection;
    private Vector3 TargetDirection
    {
        get
        {
            return targetDirection;
        }

        set
        {
            targetDirection = value;
        }
    }
    
    void Start ()
    {
        TargetDirection = transform.forward;
	}
	
	void Update ()
    {
        if (GameData.IsGamePaused) return;

        if (Vector3.Angle(TargetDirection, transform.forward) < 1.0f)
        {
            DetermineTarget();
        }

        Vector3 newDir = Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * Time.deltaTime, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void DetermineTarget()
    {
        var euler = Vector3.zero;
        euler.x = Random.Range(MinXAngle, MaxXAngle);
        euler.y = Random.Range(MinYAngle, MaxYAngle);
        euler.z = Random.Range(MinZAngle, MaxZAngle);

        TargetDirection = Quaternion.Euler(euler) * Vector3.forward;
    }
}
