using UnityEngine;
using System.Collections;

public class GameData
{
    private static bool isCaught = false;
    public static bool IsCaught
    {
        get
        {
            return isCaught;
        }

        set
        {
            isCaught = value;
        }
    }

    private static bool isGamePaused = false;
    public static bool IsGamePaused
    {
        get
        {
            return isGamePaused;
        }

        set
        {
            isGamePaused = value;
        }
    }

    public static void Reset()
    {
        IsCaught = false;
        IsGamePaused = false;
    }

    public static void StartGame()
    {
        Reset();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void EndGame()
    {
        IsGamePaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
