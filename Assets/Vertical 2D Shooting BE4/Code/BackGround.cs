using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float ViewHeight;

    void Awake()
    {
        ViewHeight = Camera.main.orthographicSize * 2;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if (sprites[endIndex].position.y < ViewHeight*(-1))
        {
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up* ViewHeight;

            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexSave - 1 + sprites.Length) % sprites.Length;
        }
    }
}
