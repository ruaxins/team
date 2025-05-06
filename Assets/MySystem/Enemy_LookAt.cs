using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LookAt : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
        // 假设玩家对象有"Player"标签
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // 计算朝向玩家的方向（忽略Y轴高度差）
        Vector3 direction = transform.position - playerTransform.position;
        direction.y = 0;

        // 仅当方向有效时才旋转
        if (direction != Vector3.zero)
        {
            // 计算仅Y轴的旋转
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
