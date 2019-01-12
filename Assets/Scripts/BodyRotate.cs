using UnityEngine;
using System.Collections;

public class BodyRotate : MonoBehaviour
{
    [SerializeField]
    private Transform body;
    public Transform Body
    {
        get
        {
            return body;
        }

        set
        {
            body = value;
        }
    }

    [SerializeField]
    private Transform head;
    public Transform Head
    {
        get
        {
            return head;
        }

        set
        {
            head = value;
        }
    }



    void Start()
    {
	
	}
	
	void Update()
    {
	    if (Body && Head)
        {
            var eulerAngles = new Vector3(0.0f, Head.eulerAngles.y, 0.0f);
            Body.eulerAngles = eulerAngles;
        }
	}
}
