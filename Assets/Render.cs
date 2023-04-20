using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

public class Render : MonoBehaviour
{
    public Texture2D texture;
    public float timer;
    public Vector2Int PlayerPos;
    public Color CursorColor;

    Object[] Object;
    Vector2Int[] Pos;

    Vector2Int Resolution = new Vector2Int(288, 162);

    int p;

    bool WindowOpen;

    int selected=999;
    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D(Resolution.x, Resolution.y);
        texture.name = "Screen";
        texture.filterMode = FilterMode.Point;
        FindObjectOfType<RenderReplacement>().replacement = texture;

        CursorColor = Color.black;
        PlayerPos = new Vector2Int(140, 240);

        Pos = GetComponent<Objects>().Pos;

        Object = GetComponent<Objects>().Object;

        DrawWindow(0);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            PlayerPos += new Vector2Int(0, 1);
            DrawCursor();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            PlayerPos += new Vector2Int(0, -1);
            DrawCursor();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerPos += new Vector2Int(-1, 0);
            DrawCursor();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            PlayerPos += new Vector2Int(1, 0);
            DrawCursor();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < Pos.Length; i++)
            {
                print("Disctance = " + (Vector2.Distance(Pos[i], PlayerPos)) + "For " + i);
                if (Vector2.Distance(Pos[i], PlayerPos) <=5)
                {
                    Pos[i] = PlayerPos;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (WindowOpen && selected != 999)
            {
                WindowOpen = false;
                selected = 999;
            }
            //print("now");
            CursorColor = Color.red;

            DrawCursor();
            for (int i = 0; i < Pos.Length; i++)
            {
                if (Vector2.Distance(Pos[i], PlayerPos) <= 5)
                {
                    if (!WindowOpen)
                    {
                        WindowOpen = true;
                        selected = i;
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CursorColor = Color.black;
            //print("UP");

            DrawCursor();
        }

    }
    public void DrawCursor()
    {
        Refresh();

        DrawAllObjects();

        if (WindowOpen && selected != 999)
        {
            DrawWindow(selected);
        }

        texture.SetPixel(PlayerPos.x, PlayerPos.y, CursorColor);
        texture.SetPixel(PlayerPos.x + 1, PlayerPos.y, CursorColor);
        texture.SetPixel(PlayerPos.x, PlayerPos.y + 1, CursorColor);
        texture.SetPixel(PlayerPos.x, PlayerPos.y - 1, CursorColor);
        texture.SetPixel(PlayerPos.x - 1, PlayerPos.y, CursorColor);


        texture.Apply();
    }
    public void Refresh()
    {
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = Color.white;
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
    }

    public void DrawAllObjects()
    {
        for (int i = 0; i < Pos.Length; i++)
        {
            p=63;
            for (int y = Pos[i].y-4; y < Pos[i].y + 4; y++)
            {
                for (int x = Pos[i].x-4; x < Pos[i].x + 4; x++)
                {
                    //print(p);
                    texture.SetPixel(x, y, Object[i].Color[p]);
                    if (p > 0)
                    {
                        p--;
                    }
                }
            }
        }
        
        texture.Apply();
    }

    public void DrawWindow(int no)
    {
        for (int y = Resolution.y / 2 - (Resolution.y / 2) + 16; y < Resolution.y / 2 + (Resolution.y / 2) - 16; y++)
        {
            for (int x = Resolution.x / 2 - (Resolution.x / 2) + 16; x < Resolution.x / 2 + (Resolution.x / 2) - 16; x++)
            {
                texture.SetPixel(x, y, GetComponent<Windows>().Window[no].Color);
            }
        }

        texture.Apply();
    }
}
