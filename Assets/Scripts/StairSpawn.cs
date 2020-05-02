using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairSpawn : MonoBehaviour
{

    [SerializeField]
    GameObject stairPreFab;


    int index = 0;
    [SerializeField]
    float stairWidth = 2f, stairHeight = 1f;
    [SerializeField]
    int minX=-5,maxX=5;

    public static  StairSpawn instance = null;


    List<GameObject> stairList = new List<GameObject>();

    float hue;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        makeObjects();
        initColor();
        for(int i=0; i < 5; i++)
        {
            makeNewStair();
        }
    }


    void initColor()
    {
        hue = UnityEngine.Random.Range(0f, 1f);
        Camera.main.backgroundColor = Color.HSVToRGB(hue,0.6f,0.8f);
    }
    private void makeObjects()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject stair = Instantiate(stairPreFab);
            stair.SetActive(false);
            stairList.Add(stair);
        }
    }


    GameObject getStair()
    {
        GameObject  obj = null;
        for(int i = 0;i < stairList.Count;i++)
        {
            if (!stairList[i].activeInHierarchy)
            {
                obj= stairList[i];
                return obj;
            }
        }
        return null;
    }

    public void makeNewStair()
    {
        int randomPosX;
        if (index == 0)
            randomPosX = 0;
        else
            randomPosX = Random.Range(minX, maxX);


        Vector2 newPosition = new Vector2(randomPosX, index * 5);

        GameObject stair = getStair();
        if (stair == null)
            return; 
        stair.SetActive(true);
        stair.transform.position = newPosition;
        stair.transform.rotation = Quaternion.identity;
        stair.transform.localScale = new Vector2(stairWidth, stairHeight);
        stair.transform.SetParent(transform);
        stair.GetComponent<StairScript>().changePos();
        setColor(stair);
        stairWidth -= 0.02f;
        decreaseWidth();
        index++;
    }



    void decreaseWidth()
    {
        if (stairWidth < 1)
            stairWidth = 1f;    //bring 'em back else stairs will disappear
        stairWidth -= 0.02f;
    }
    void setColor(GameObject stair)
    {
        if (UnityEngine.Random.Range(0, 3) != 0)
        {
            hue += 0.11f;
            if (hue >= 1)
            {
                hue -= 1f;
            }
        }
        stair.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hue, 1f, 1f);
    }
 }
