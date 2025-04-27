using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game_Init : MonoBehaviour
{
    Manager manager = new Manager();
    private void Awake()
    {
        #region 创建卡牌实例
        Card d3 = new Card("diamond", "3", 3, "null", 3);
        Card d4 = new Card("diamond", "4", 4, "null", 4);
        Card d5 = new Card("diamond", "5", 5, "null", 5);
        Card d6 = new Card("diamond", "6", 6, "null", 6);
        Card d7 = new Card("diamond", "7", 7, "null", 7);
        Card d8 = new Card("diamond", "8", 8, "null", 8);
        Card d9 = new Card("diamond", "9", 9, "null", 9);
        Card d10 = new Card("diamond", "10", 10, "null", 10);
        Card dJ = new Card("diamond", "J", 10, "null", 11);
        Card dQ = new Card("diamond", "Q", 10, "null", 12);
        Card dK = new Card("diamond", "K", 10, "null", 13);
        Card dA = new Card("diamond", "A", 10, "null", 14);

        Card c3 = new Card("club", "3", 3, "null", 3);
        Card c4 = new Card("club", "4", 4, "null", 4);
        Card c5 = new Card("club", "5", 5, "null", 5);
        Card c6 = new Card("club", "6", 6, "null", 6);
        Card c7 = new Card("club", "7", 7, "null", 7);
        Card c8 = new Card("club", "8", 8, "null", 8);
        Card c9 = new Card("club", "9", 9, "null", 9);
        Card c10 = new Card("club", "10", 10, "null", 10);
        Card cJ = new Card("club", "J", 10, "null", 11);
        Card cQ = new Card("club", "Q", 10, "null", 12);
        Card cK = new Card("club", "K", 10, "null", 13);
        Card cA = new Card("club", "A", 10, "null", 14);

        Card h3 = new Card("heart", "3", 3, "null", 3);
        Card h4 = new Card("heart", "4", 4, "null", 4);
        Card h5 = new Card("heart", "5", 5, "null", 5);
        Card h6 = new Card("heart", "6", 6, "null", 6);
        Card h7 = new Card("heart", "7", 7, "null", 7);
        Card h8 = new Card("heart", "8", 8, "null", 8);
        Card h9 = new Card("heart", "9", 9, "null", 9);
        Card h10 = new Card("heart", "10", 10, "null", 10);
        Card hJ = new Card("heart", "J", 10, "null", 11);
        Card hQ = new Card("heart", "Q", 10, "null", 12);
        Card hK = new Card("heart", "K", 10, "null", 13);
        Card hA = new Card("heart", "A", 10, "null", 14);

        Card s3 = new Card("spade", "3", 3, "null", 3);
        Card s4 = new Card("spade", "4", 4, "null", 4);
        Card s5 = new Card("spade", "5", 5, "null", 5);
        Card s6 = new Card("spade", "6", 6, "null", 6);
        Card s7 = new Card("spade", "7", 7, "null", 7);
        Card s8 = new Card("spade", "8", 8, "null", 8);
        Card s9 = new Card("spade", "9", 9, "null", 9);
        Card s10 = new Card("spade", "10", 10, "null", 10);
        Card sJ = new Card("spade", "J", 10, "null", 11);
        Card sQ = new Card("spade", "Q", 10, "null", 12);
        Card sK = new Card("spade", "K", 10, "null", 13);
        Card sA = new Card("spade", "A", 10, "null", 14);

        Card z1 = new Card("curse", "1", 0, "null", 0);
        Card z2 = new Card("curse", "2", 0, "null", 0);
        Card z3 = new Card("curse", "3", 0, "null", 0);
        Card z4 = new Card("curse", "4", 0, "null", 0);
        Card z5 = new Card("curse", "5", 0, "null", 0);
        #endregion
        #region 创建敌人实例
        //敌人攻击模式
        //0-造成攻击值伤害，1-两层增强，2-护甲+5，3-复制自身，4-获得扣血量的护甲，5-玩家一层自刃
        //6-失去一张手牌，7-护甲+15，8-玩家两层自刃，9-五层增强，10-四层抵挡
        List<int> mode = new List<int>
        {
            0,0,1
        };
        //初始化敌人（血量，攻击值，护甲，攻击模式）
        Enemy fire_ghost = new Enemy(30, 10, 5, mode);
        Enemy fire_insect = new Enemy(40, 10, 5, mode);
        Enemy fire_slime = new Enemy(50, 10, 5, mode);
        Enemy fire_puppet = new Enemy(60, 10, 5, mode);
        Enemy fire_specter = new Enemy(70, 10, 5, mode);
        Enemy fire_dog = new Enemy(80, 10, 5, mode);
        Enemy fire_knight = new Enemy(90, 10, 5, mode);
        Enemy fire_monster = new Enemy(100, 10, 5, mode);
        Enemy fire_witch = new Enemy(100, 10, 5, mode);
        Enemy fire_king = new Enemy(100, 10, 5, mode);
        #endregion
        #region 卡牌实例入库
        //攻击卡
        Register_Card_Object("diamond3", Card_Create(), d3);
        Register_Card_Object("diamond4", Card_Create(), d4);
        Register_Card_Object("diamond5", Card_Create(), d5);
        Register_Card_Object("diamond6", Card_Create(), d6);
        Register_Card_Object("diamond7", Card_Create(), d7);
        Register_Card_Object("diamond8", Card_Create(), d8);
        Register_Card_Object("diamond9", Card_Create(), d9);
        Register_Card_Object("diamond10", Card_Create(), d10);
        Register_Card_Object("diamondJ", Card_Create(), dJ);
        Register_Card_Object("diamondQ", Card_Create(), dQ);
        Register_Card_Object("diamondK", Card_Create(), dK);
        Register_Card_Object("diamondA", Card_Create(), dA);
        //防御卡
        Register_Card_Object("spade3", Card_Create(), s3);
        Register_Card_Object("spade4", Card_Create(), s4);
        Register_Card_Object("spade5", Card_Create(), s5);
        Register_Card_Object("spade6", Card_Create(), s6);
        Register_Card_Object("spade7", Card_Create(), s7);
        Register_Card_Object("spade8", Card_Create(), s8);
        Register_Card_Object("spade9", Card_Create(), s9);
        Register_Card_Object("spade10", Card_Create(), s10);
        Register_Card_Object("spadeJ", Card_Create(), sJ);
        Register_Card_Object("spadeQ", Card_Create(), sQ);
        Register_Card_Object("spadeK", Card_Create(), sK);
        Register_Card_Object("spadeA", Card_Create(), sA);
        //装备卡
        Register_Card_Object("club3", Card_Create(), c3);
        Register_Card_Object("club4", Card_Create(), c4);
        Register_Card_Object("club5", Card_Create(), c5);
        Register_Card_Object("club6", Card_Create(), c6);
        Register_Card_Object("club7", Card_Create(), c7);
        Register_Card_Object("club8", Card_Create(), c8);
        Register_Card_Object("club9", Card_Create(), c9);
        Register_Card_Object("club10", Card_Create(), c10);
        Register_Card_Object("clubJ", Card_Create(), cJ);
        Register_Card_Object("clubQ", Card_Create(), cQ);
        Register_Card_Object("clubK", Card_Create(), cK);
        Register_Card_Object("clubA", Card_Create(), cA);
        //诅咒卡
        Register_Card_Object("curse1", Card_Create(), z1);
        Register_Card_Object("curse2", Card_Create(), z2);
        Register_Card_Object("curse3", Card_Create(), z3);
        Register_Card_Object("curse4", Card_Create(), z4);
        Register_Card_Object("curse5", Card_Create(), z5);
        //技能卡
        Register_Card_Object("heart3", Card_Create(), h3);
        Register_Card_Object("heart4", Card_Create(), h4);
        Register_Card_Object("heart5", Card_Create(), h5);
        Register_Card_Object("heart6", Card_Create(), h6);
        Register_Card_Object("heart7", Card_Create(), h7);
        Register_Card_Object("heart8", Card_Create(), h8);
        Register_Card_Object("heart9", Card_Create(), h9);
        Register_Card_Object("heart10", Card_Create(), h10);
        Register_Card_Object("heartJ", Card_Create(), hJ);
        Register_Card_Object("heartQ", Card_Create(), hQ);
        Register_Card_Object("heartK", Card_Create(), hK);
        Register_Card_Object("heartA", Card_Create(), hA);
        #endregion
        #region 敌人实例入库
        //敌人入库
        Register_Enemy_Object("fire_ghost", Enemy_Create(), fire_ghost);
        Register_Enemy_Object("fire_insect", Enemy_Create(), fire_insect);
        Register_Enemy_Object("fire_slime", Enemy_Create(), fire_slime);
        Register_Enemy_Object("fire_puppet", Enemy_Create(), fire_puppet);
        Register_Enemy_Object("fire_specter", Enemy_Create(), fire_specter);
        Register_Enemy_Object("fire_dog", Enemy_Create(), fire_dog);
        Register_Enemy_Object("fire_knight", Enemy_Create(), fire_knight);
        Register_Enemy_Object("fire_monster", Enemy_Create(), fire_monster);
        Register_Enemy_Object("fire_witch", Enemy_Create(), fire_witch);
        Register_Enemy_Object("fire_king", Enemy_Create(), fire_king);
        #endregion
        #region 选择卡牌实例入库
        //攻击卡
        Register_Select_Object("diamond3", Select_Create());
        Register_Select_Object("diamond4", Select_Create());
        Register_Select_Object("diamond5", Select_Create());
        Register_Select_Object("diamond6", Select_Create());
        Register_Select_Object("diamond7", Select_Create());
        Register_Select_Object("diamond8", Select_Create());
        Register_Select_Object("diamond9", Select_Create());
        Register_Select_Object("diamond10", Select_Create());
        Register_Select_Object("diamondJ", Select_Create());
        Register_Select_Object("diamondQ", Select_Create());
        Register_Select_Object("diamondK", Select_Create());
        Register_Select_Object("diamondA", Select_Create());
        //防御卡
        Register_Select_Object("spade3", Select_Create());
        Register_Select_Object("spade4", Select_Create());
        Register_Select_Object("spade5", Select_Create());
        Register_Select_Object("spade6", Select_Create());
        Register_Select_Object("spade7", Select_Create());
        Register_Select_Object("spade8", Select_Create());
        Register_Select_Object("spade9", Select_Create());
        Register_Select_Object("spade10", Select_Create());
        Register_Select_Object("spadeJ", Select_Create());
        Register_Select_Object("spadeQ", Select_Create());
        Register_Select_Object("spadeK", Select_Create());
        Register_Select_Object("spadeA", Select_Create());
        //装备卡
        Register_Select_Object("club3", Select_Create());
        Register_Select_Object("club4", Select_Create());
        Register_Select_Object("club5", Select_Create());
        Register_Select_Object("club6", Select_Create());
        Register_Select_Object("club7", Select_Create());
        Register_Select_Object("club8", Select_Create());
        Register_Select_Object("club9", Select_Create());
        Register_Select_Object("club10", Select_Create());
        Register_Select_Object("clubJ", Select_Create());
        Register_Select_Object("clubQ", Select_Create());
        Register_Select_Object("clubK", Select_Create());
        Register_Select_Object("clubA", Select_Create());
        //诅咒卡
        Register_Select_Object("curse1", Select_Create());
        Register_Select_Object("curse2", Select_Create());
        Register_Select_Object("curse3", Select_Create());
        Register_Select_Object("curse4", Select_Create());
        Register_Select_Object("curse5", Select_Create());
        //技能卡
        Register_Select_Object("heart3", Select_Create());
        Register_Select_Object("heart4", Select_Create());
        Register_Select_Object("heart5", Select_Create());
        Register_Select_Object("heart6", Select_Create());
        Register_Select_Object("heart7", Select_Create());
        Register_Select_Object("heart8", Select_Create());
        Register_Select_Object("heart9", Select_Create());
        Register_Select_Object("heart10", Select_Create());
        Register_Select_Object("heartJ", Select_Create());
        Register_Select_Object("heartQ", Select_Create());
        Register_Select_Object("heartK", Select_Create());
        Register_Select_Object("heartA", Select_Create());

        #endregion
    }
    GameObject Card_Create()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/card");
        GameObject card = Instantiate(prefab, transform);
        card.SetActive(false);
        return card;
    }
    GameObject Enemy_Create()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/enemy");
        GameObject enemy = Instantiate(prefab, transform);
        enemy.SetActive(false);
        return enemy;
    }
    GameObject Select_Create()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/select");
        GameObject select = Instantiate(prefab, transform);
        select.SetActive(false);
        return select;
    }
    void Register_Card_Object(string type, GameObject obj, Card card)
    {
        Message.Msg.card_bank.Add(type);
        if (!Message.Msg.instance_card.ContainsKey(type))
        {
            Message.Msg.instance_card.Add(type, obj);
        }
        if (!Message.Msg.data_card.ContainsKey(type))
        {
            Message.Msg.data_card.Add(type, card);
        }
        if (card.type == "club" || card.type == "curse") return;
        obj.GetComponent<Button>().onClick.AddListener(() =>
        {
            manager.OnCardClick(type);
        });
        obj.AddComponent<CardHoverEffect>();
    }
    void Register_Enemy_Object(string type, GameObject obj, Enemy enemy)
    {
        if (!Message.Msg.instance_enemy.ContainsKey(type))
        {
            Message.Msg.instance_enemy.Add(type, obj);
        }
        if (!Message.Msg.data_enemy.ContainsKey(type))
        {
            Message.Msg.data_enemy.Add(type, enemy);
        }
        obj.GetComponent<Button>().onClick.AddListener(() =>
        {
            manager.OnEnemyClick(type);
        });
    }
    void Register_Select_Object(string type, GameObject obj)
    {
        if (!Message.Msg.instance_select.ContainsKey(type))
        {
            Message.Msg.instance_select.Add(type, obj);
        }
        obj.GetComponent<Button>().onClick.AddListener(() =>
        {
            manager.Get_Now_Card(obj);
        });
    }
}
