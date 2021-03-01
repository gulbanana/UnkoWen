using UnityEngine;

// works only with old or both old/new input backends - see https://issuetracker.unity3d.com/issues/cursor-displays-loading-icon-until-game-window-loses-and-regains-focus-when-building-without-splash-screen
public class CustomCursor: MonoBehaviour
{
    public Texture2D cursor;
    public CursorMode mode;

    private void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, mode);
    }
}
