using UnityEngine;

public class card : MonoBehaviour
{
    [Header("卡面设置")]
    public Sprite cardFront;  // 卡牌正面图片
    [HideInInspector] public card2 controller; // 对应的控制器
}