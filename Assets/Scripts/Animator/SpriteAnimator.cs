using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{

    public SpriteRenderer mySpriteRenderer;
    public AnimationData baseAnimation;


    public IEnumerator PlayAnimation(AnimationData data)
    {
        int spriteAmount = data.sprites.Length, i = 0;
        float waitTime = data.frameOfGap * AnimationData.targetFrameTime;

        while(i < spriteAmount)
        {
            mySpriteRenderer.sprite = data.sprites[i++];
            yield return new WaitForSeconds(waitTime);
            if (i == spriteAmount)
            { i = 0; }

        }
        yield return null;
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayAnimation(baseAnimation));

    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
