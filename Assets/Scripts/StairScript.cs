using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    Vector2 startPos, targetPos;
    float randomX;
    float smoothTime = 0.1f;
    Vector2 velocity = Vector2.zero;

    
    
    // Start is called before the first frame update
    public void changePos()
    {
        targetPos = transform.position;
        if(UnityEngine.Random.Range(0,2)==0)
        {
            randomX = -10;
        }
        startPos = new Vector2(targetPos.x + randomX, targetPos.y);
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(targetPos,transform.position)>0.01f)
        {
            moveToTargetPosition();
        }
    }

    private void moveToTargetPosition()
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
