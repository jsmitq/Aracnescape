using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform spider = null;
    public Transform Spider
    {
        get
        {
            return spider;
        }

        set
        {
            spider = value;
        }
    }

    [SerializeField]
    private Transform humanHead = null;
    public Transform HumanHead
    {
        get
        {
            return humanHead;
        }

        set
        {
            humanHead = value;
        }
    }

    [SerializeField]
    private float windowPlane = 0.0f;
    public float WindowPlane
    {
        get
        {
            return windowPlane;
        }

        set
        {
            windowPlane = value;
        }
    }

    [SerializeField]
    private float humanFOV = 30.0f;
    public float HumanFOV
    {
        get
        {
            return humanFOV;
        }

        set
        {
            humanFOV = value;
        }
    }

    [SerializeField]
    private SceneFader fader = null;
    public SceneFader Fader
    {
        get
        {
            return fader;
        }

        set
        {
            fader = value;
        }
    }

    private Vector3 headToSpider = Vector3.zero;
    private Vector3 HeadToSpider
    {
        get
        {
            return headToSpider;
        }

        set
        {
            headToSpider = value;
        }
    }

    private RaycastHit rayHit;
    
    void Start()
    {
        GameData.StartGame();
    }
	
	void Update()
    {
        if (GameData.IsGamePaused) return;

	    if (Spider && Spider.position.x < windowPlane)
        {
            OnEscape();
        }

        HeadToSpider = Vector3.Normalize(Spider.position - HumanHead.position);
        var angle = Mathf.Abs(Vector3.Angle(HeadToSpider, HumanHead.forward));

        if (angle < HumanFOV)
        {
            CheckIfCaught();
        }
	}

    private void CheckIfCaught()
    {
        if (Physics.Raycast(HumanHead.position, HeadToSpider, out rayHit, 200.0f))
        {
            if (rayHit.transform == Spider)
            {
                OnCaught();
            }
        }
    }

    public void OnCaught()
    {
        GameData.IsCaught = true;

        OnGameOver();
    }

    private void OnEscape()
    {
        OnGameOver();
    }
    
    private void OnGameOver()
    {
        GameData.EndGame();

        if (Fader)
        {
            Fader.OnFadeOutToScene("GameOver");
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
