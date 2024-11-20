using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // ��ũ�� �ӵ��� ��Ÿ���� ����
    public float speed;

    // ��������Ʈ �迭���� ���� �� �� �ε��� (����� ó���� �� ��ġ�� ����)
    public int startIndex;
    public int endIndex;

    // ��� ��������Ʈ���� �����ϴ� �迭
    public Transform[] sprites;

    // ī�޶� ���� ������ �����ϱ� ���� ����
    float viewHeight;

    // Awake�� ���� ������Ʈ�� Ȱ��ȭ�� �� ȣ��Ǹ�, �ʱ�ȭ �۾��� ���
    private void Awake()
    {
        // ī�޶��� orthographic ũ�⸦ �̿��� ȭ�� ����(viewHeight) ���
        // orthographicSize�� ī�޶��� �ݳ���, ���� 2�踦 ���� ��ü ���̸� ����
        viewHeight = Camera.main.orthographicSize * 2;
    }

    // Update�� �� ������ ȣ��Ǹ�, ����� �����̰� ��ũ�Ѹ� ó��
    void Update()
    {
        // ����� �Ʒ��� �����̴� �Լ� ȣ��
        Move();

        // ��������Ʈ�� ȭ�� �Ʒ��� ������� �����ϱ� ���� ��ũ�Ѹ� ó��
        Scrolling();
    }

    // ��� ��������Ʈ�� ��ũ�� ó���ϴ� �Լ�
    void Scrolling()
    {
        // ���� �� ��������Ʈ�� ȭ�� �Ʒ�(-viewHeight)�� ������ ����ٸ�
        if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            //#1. Sprite ��Ȱ�� ó��
            // ���� ��������Ʈ�� ��ġ�� ������
            Vector3 backSpritesPos = sprites[startIndex].transform.localPosition;
            // �� ��������Ʈ�� ���� ��ġ�� ������
            Vector3 frontSpritePos = sprites[endIndex].transform.localPosition;
            // �� ��������Ʈ�� ���� ��������Ʈ�� �ٷ� �������� �̵�
            sprites[endIndex].transform.localPosition = backSpritesPos + Vector3.up * viewHeight;

            // ���� �� �� �ε����� ���� (��ȯ�ϵ��� ó��)
            int startIndexSave = startIndex; // ���� ���� �ε����� �ӽ� ����
            startIndex = endIndex; // �� ��������Ʈ�� ���ο� �������� ����
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
            // �� �ε����� ���� �ε����� ���� (��ȯ ������ ó��)
        }
    }

    // ��� ��������Ʈ���� �Ʒ��� �����̴� �Լ�
    void Move()
    {
        // ���� ������Ʈ�� ��ġ�� ������
        Vector3 curPos = transform.position;

        // �ӵ�(speed)�� ������� �̵��� ���ο� ��ġ�� ���
        // Time.deltaTime�� ������ �� �ð� ��������, �ð� ��� �������� ����
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;

        // ���� ��ġ�� �̵����� ���� ���ο� ��ġ�� ������Ʈ
        transform.position = curPos + nextPos;
    }
}



