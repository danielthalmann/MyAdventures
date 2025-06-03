using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objets/Cursor", order = 1)]
public class CursorInfo: ScriptableObject
{
    public string tag;
    public Sprite cursor;
    public Vector2 hotspot;
    public Vector2 size;
}