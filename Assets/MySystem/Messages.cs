using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{

}
public class Message
{
    //游戏内全局变量
    public List<Card> bank = new List<Card>();//图鉴
    public List<Card> bank_in_cards = new List<Card>();//牌库
    public List<Enemy> enemy_bank = new List<Enemy>();//怪物池
    //是否锁定
    bool isLock = false;
    public bool IsLock { get => isLock; set => isLock = value; }

    private static Message message;
    private Message() { }
    public static Message Msg
    {
        get
        {
            if (message == null)
            {
                message = new Message();
            }
            return message;
        }
    }
}