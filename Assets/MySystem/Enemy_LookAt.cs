using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LookAt : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
        // ������Ҷ�����"Player"��ǩ
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // ���㳯����ҵķ��򣨺���Y��߶Ȳ
        Vector3 direction = transform.position - playerTransform.position;
        direction.y = 0;

        // ����������Чʱ����ת
        if (direction != Vector3.zero)
        {
            // �����Y�����ת
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
