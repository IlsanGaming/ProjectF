using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }
    void Update()
    {
        Move();
        Scrolling();
    }
    void Scrolling()
    {
        if (sprites[endIndex].position.y< viewHeight*(-1))
        {
            //#1. Sprite ReUse
            Vector3 backSpritesPos = sprites[startIndex].transform.localPosition;
            Vector3 frontSpritePos = sprites[endIndex].transform.localPosition;
            sprites[endIndex].transform.localPosition = backSpritesPos+Vector3.up* viewHeight;

            //Cursor Index Change
            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
        }

    }
    void Move()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down*speed*Time.deltaTime;
        transform.position = curPos + nextPos;

    }
}


