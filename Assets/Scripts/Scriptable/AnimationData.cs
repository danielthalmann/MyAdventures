using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animation Data", menuName = "Scriptable Objets/Animation Data", order = 0)]
public class AnimationData : ScriptableObject
{
    public static float targetFrameTime = 0.0167f;
    public int frameOfGap;
    public Sprite[] sprites;

}
