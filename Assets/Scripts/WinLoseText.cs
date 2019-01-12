using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLoseText : MonoBehaviour
{
    [SerializeField]
    private string winText = "";
    public string WinText
    {
        get
        {
            return winText;
        }

        set
        {
            winText = value;
        }
    }

    [SerializeField]
    private Color winColor = Color.white;
    public Color WinColor
    {
        get
        {
            return winColor;
        }

        set
        {
            winColor = value;
        }
    }

    [SerializeField]
    private string loseText = "";
    public string LoseText
    {
        get
        {
            return loseText;
        }

        set
        {
            loseText = value;
        }
    }

    [SerializeField]
    private Color loseColor = Color.white;
    public Color LoseColor
    {
        get
        {
            return loseColor;
        }

        set
        {
            loseColor = value;
        }
    }

    private Text displayText = null;
    public Text DisplayText
    {
        get
        {
            return displayText;
        }

        set
        {
            displayText = value;
        }
    }
    
    void Start()
    {
        DisplayText = GetComponent<Text>();

	    if (DisplayText && GameData.IsCaught)
        {
            DisplayText.text = LoseText;
            DisplayText.color = LoseColor;
        }
        else
        {
            DisplayText.text = WinText;
            DisplayText.color = WinColor;
        }
	}
}
