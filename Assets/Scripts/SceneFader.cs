using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    private CanvasGroup m_CGComponent = null;
    private CanvasGroup CGComponent
    {
        get
        {
            return m_CGComponent;
        }

        set
        {
            m_CGComponent = value;
        }
    }


    private AudioSource m_AudioSource = null;
    private AudioSource AudioSource
    {
        get
        {
            return m_AudioSource;
        }

        set
        {
            m_AudioSource = value;
        }
    }


    private int m_FadeDirection = 0;
    protected int FadeDirection
    {
        get
        {
            return m_FadeDirection;
        }

        set
        {
            m_FadeDirection = value;
        }
    }


    private float m_Alpha = 1.0f;
    protected float Alpha
    {
        get
        {
            return m_Alpha;
        }

        set
        {
            m_Alpha = value;
        }
    }
    

    [SerializeField]
    private float m_FadeTime = 1.0f;
    public float FadeTime
    {
        get
        {
            return m_FadeTime;
        }

        set
        {
            m_FadeTime = value;
        }
    }


    [SerializeField]
    private float m_FadeDelay = 0.0f;
    public float FadeDelay
    {
        get
        {
            return m_FadeDelay;
        }

        set
        {
            m_FadeDelay = value;
        }
    }


    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        CGComponent = GetComponent<CanvasGroup>();
        CGComponent.alpha = Alpha;
        FadeDirection = -1;
    }


    private void OnGUI()
    {
        if (FadeDirection != 0)
        {
            Alpha += FadeDirection * FadeTime * Time.deltaTime;
            Alpha = Mathf.Clamp01(Alpha);

            if (CGComponent) CGComponent.alpha = Alpha;

            if ( (FadeDirection == -1 && Alpha == 0.0f) ||
                 (FadeDirection == 1  && Alpha == 1.0f) )
            {
                FadeDirection = 0;
            }
        }
    }


    public void OnFadeOutToScene(string sceneName)
    {
        StartCoroutine(FadeOutToScene(sceneName, FadeDelay));
    }


    private IEnumerator FadeOutToScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (AudioSource)
            AudioSource.Play();

        FadeDirection = 1;

        yield return new WaitForSeconds(FadeTime);

        SceneManager.LoadScene(sceneName);
    }

    public void OnFadeOutToQuit()
    {
        StartCoroutine(FadeOutToQuit(FadeDelay));
    }

    private IEnumerator FadeOutToQuit(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (AudioSource)
            AudioSource.Play();

        FadeDirection = 1;

        yield return new WaitForSeconds(FadeTime);

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
