using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtManager : Singleton<ArtManager>
{
    public enum CursorToUse { NORMAL, HAMMER, ERROR, BUILD_AREA };
    public Dictionary<CursorToUse, Texture2D> cursorChange = new Dictionary<CursorToUse, Texture2D>();

    private void AddArtToDictionaries()
    {
        cursorChange.Add(CursorToUse.NORMAL, Resources.Load<Texture2D>("Cursors/NormalMouse"));
        cursorChange.Add(CursorToUse.HAMMER, Resources.Load<Texture2D>("Cursors/Hammer"));
        cursorChange.Add(CursorToUse.ERROR, Resources.Load<Texture2D>("Cursors/RedX"));
        cursorChange.Add(CursorToUse.BUILD_AREA, Resources.Load<Texture2D>("Cursors/BuildArea"));
    }
    // Start is called before the first frame update
    void Awake()
    {
        AddArtToDictionaries();
    }

    public void SwapCursor(CursorToUse cursor)
    {
        Vector2 cursorOffset = Vector2.zero;
        if (cursor == CursorToUse.BUILD_AREA)
        {
            cursorOffset = new Vector2(.5f, .5f);
        }
        else
        {
            cursorOffset = Vector2.zero;
        }

        Cursor.SetCursor(cursorChange[cursor], cursorOffset, CursorMode.Auto);
    }
}
