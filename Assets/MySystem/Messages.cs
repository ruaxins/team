using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{

}
public class Message
{
    //��Ϸ��ȫ�ֱ���
    public List<Card> bank = new List<Card>();//ͼ��
    public List<Card> bank_in_cards = new List<Card>();//�ƿ�
    public List<Enemy> enemy_bank = new List<Enemy>();//�����
    //�Ƿ�����
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