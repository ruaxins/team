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
        Card d3 = new Card("diamond", "3", 3, "攻击力+3", 3);
        Card d4 = new Card("diamond", "4", 4, "攻击力+4", 4);
        Card d5 = new Card("diamond", "5", 5, "攻击力+5", 5);
        Card d6 = new Card("diamond", "6", 6, "攻击力+6", 6);
        Card d7 = new Card("diamond", "7", 7, "攻击力+7", 7);
        Card d8 = new Card("diamond", "8", 8, "攻击力+8", 8);
        Card d9 = new Card("diamond", "9", 9, "攻击力+9", 9);
        Card d10 = new Card("diamond", "10", 10, "攻击力+10", 10);
        Card dJ = new Card("diamond", "J", 10, "攻击力+10", 11);
        Card dQ = new Card("diamond", "Q", 10, "攻击力+10", 12);
        Card dK = new Card("diamond", "K", 10, "攻击力+10", 13);
        Card dA = new Card("diamond", "A", 10, "攻击力+10", 14);

        Card c3 = new Card("club", "3", 3, "获得4层【增强】", 3);
        Card c4 = new Card("club", "4", 4, "获得4层【抵挡】", 4);
        Card c5 = new Card("club", "5", 5, "每场战斗开始额外获得1次出牌机会，回合结束获得2层【自刃】", 5);
        Card c6 = new Card("club", "6", 6, "受到伤害时50%概率反弹给攻击者", 6);
        Card c7 = new Card("club", "7", 7, "打出葫芦时，防御牌点数×2", 7);
        Card c8 = new Card("club", "8", 8, "每当1张梅花牌变为诅咒时，回复5点生命", 8);
        Card c9 = new Card("club", "9", 9, "若出牌数=4张，计分点数×2", 9);
        Card c10 = new Card("club", "10", 10, "抽牌时10%概率额外抽1张", 10);
        Card cJ = new Card("club", "J", 10, "打出顺子时可仅用4张牌（仍需满足连续点数）", 11);
        Card cQ = new Card("club", "Q", 10, "若出牌数≤3张，计分点数×3", 12);
        Card cK = new Card("club", "K", 10, "回合结束后回复最大生命的20%", 13);
        Card cA = new Card("club", "A", 10, "选择2张牌复制并加入卡组，再随机抽2张牌", 14);

        Card h3 = new Card("heart", "3", 3, "玩家本回合伤害点数×2", 3);
        Card h4 = new Card("heart", "4", 4, "无视怪物护甲直接造成伤害（护甲不消失）", 4);
        Card h5 = new Card("heart", "5", 5, "玩家本回合防御点数×2", 5);
        Card h6 = new Card("heart", "6", 6, "将本回合防御值转化为等量伤害返还怪物（如防御值=10，则怪物额外受10伤害）", 6);
        Card h7 = new Card("heart", "7", 7, "获得1层【自刃】+1层【增强】", 7);
        Card h8 = new Card("heart", "8", 8, "获得2层【自刃】，从抽牌堆选1张牌加入手牌", 8);
        Card h9 = new Card("heart", "9", 9, "回复本回合受到怪物伤害的50%生命值", 9);
        Card h10 = new Card("heart", "10", 10, "选择1张卡牌，使其基础数值+1（可叠加）", 10);
        Card hJ = new Card("heart", "J", 10, "获得2层【自刃】，从弃牌堆选1张牌加入手牌", 11);
        Card hQ = new Card("heart", "Q", 10, "对怪物造成15点伤害，玩家损失5点生命", 12);
        Card hK = new Card("heart", "K", 10, "生成5点临时护甲（持续到下回合开始）", 13);
        Card hA = new Card("heart", "A", 10, "在打出的卡牌中任选1张，将其从方块?黑桃转换类型（仅当回合生效）", 14);

        Card s3 = new Card("spade", "3", 3, "防御力+3", 3);
        Card s4 = new Card("spade", "4", 4, "防御力+4", 4);
        Card s5 = new Card("spade", "5", 5, "防御力+5", 5);
        Card s6 = new Card("spade", "6", 6, "防御力+6", 6);
        Card s7 = new Card("spade", "7", 7, "防御力+7", 7);
        Card s8 = new Card("spade", "8", 8, "防御力+8", 8);
        Card s9 = new Card("spade", "9", 9, "防御力+9", 9);
        Card s10 = new Card("spade", "10", 10, "防御力+10", 10);
        Card sJ = new Card("spade", "J", 10, "防御力+10", 11);
        Card sQ = new Card("spade", "Q", 10, "防御力+10", 12);
        Card sK = new Card("spade", "K", 10, "防御力+10", 13);
        Card sA = new Card("spade", "A", 10, "防御力+10", 14);

        Card z1 = new Card("curse", "0", 0, "本轮所有人头牌（J/Q/K/A）的攻击/防御点数=0", 0);
        Card z2 = new Card("curse", "0", 0, "本轮所有攻击/防御牌点数×0.5", 0);
        Card z3 = new Card("curse", "0", 0, "每次攻击敌人后，自身获得1层【自刃】", 0);
        Card z4 = new Card("curse", "0", 0, "敌人获得2层【抵挡】", 0);
        Card z5 = new Card("curse", "0", 0, "敌人获得2层【增强】", 0);
        #endregion
        #region 创建敌人实例
        //敌人攻击模式
        //0-造成攻击值伤害，1-两层增强，2-护甲+5，3-复制自身，4-获得扣血量的护甲，5-玩家一层自刃
        //6-失去一张手牌，7-护甲+15，8-玩家两层自刃，9-五层增强，10-四层抵挡
        List<int> mode0 = new List<int>
        {
            //攻击，攻击，强化
            0,0,1
        };
        List<int> mode1 = new List<int>
        {
            //攻击，防御
            0,2
        };
        List<int> mode2 = new List<int>
        {
            //攻击，分裂
            0
        };
        List<int> mode3 = new List<int>
        {
            //反弹，攻击
            4,0
        };
        List<int> mode4 = new List<int>
        {
            //攻击，削弱
            0,5
        };
        List<int> mode5 = new List<int>
        {
            //攻击，灼烧
            0
        };
        List<int> mode6 = new List<int>
        {
            //攻击，强化
            0,1
        };
        List<int> mode7 = new List<int>
        {
            //攻击，防御，灼烧
            0,7,6
        };
        List<int> mode8 = new List<int>
        {
            //攻击，削弱，召唤
            0,8
        };
        List<int> mode9 = new List<int>
        {
            //攻击，防御，狂暴，召唤
            0,10,9
        };
        //初始化敌人（血量，攻击值，护甲，攻击模式）
        Enemy fire_ghost = new Enemy("S_Enemy", "fire_ghost", 30, 10, 5, mode0);
        Enemy fire_insect = new Enemy("S_Enemy", "fire_insect", 40, 10, 5, mode1);
        Enemy fire_slime = new Enemy("S_Enemy", "fire_slime", 50, 10, 5, mode2);
        Enemy fire_puppet = new Enemy("S_Enemy", "fire_puppet", 60, 10, 5, mode3);
        Enemy fire_specter = new Enemy("S_Enemy", "fire_specter", 70, 10, 5, mode4);
        Enemy fire_dog = new Enemy("S_Enemy", "fire_dog", 80, 10, 5, mode5);
        Enemy fire_knight = new Enemy("B_Enemy", "fire_knight", 90, 10, 5, mode6);
        Enemy fire_monster = new Enemy("B_Enemy", "fire_monster", 100, 10, 5, mode7);
        Enemy fire_witch = new Enemy("B_Enemy", "fire_witch", 100, 10, 5, mode8);
        Enemy fire_king = new Enemy("Boss", "fire_king", 100, 10, 5, mode9);
        //复制体
        Enemy fire_ghost_copy = new Enemy("S_Enemy", "fire_ghost_copy", 30, 10, 5, mode0);
        Enemy fire_insect_copy = new Enemy("S_Enemy", "fire_insect_copy", 40, 10, 5, mode1);
        Enemy fire_slime_copy = new Enemy("S_Enemy  ", "fire_slime_copy", 50, 10, 5, mode2);
        Enemy fire_puppet_copy = new Enemy("S_Enemy", "fire_puppet_copy", 60, 10, 5, mode3);
        Enemy fire_specter_copy = new Enemy("S_Enemy", "fire_specter_copy", 70, 10, 5, mode4);
        Enemy fire_dog_copy = new Enemy("S_Enemy", "fire_dog_copy", 80, 10, 5, mode5);
        Enemy fire_knight_copy = new Enemy("B_Enemy", "fire_knight_copy", 90, 10, 5, mode6);
        Enemy fire_monster_copy = new Enemy("B_Enemy", "fire_monster_copy", 100, 10, 5, mode7);
        Enemy fire_witch_copy = new Enemy("B_Enemy", "fire_witch_copy", 100, 10, 5, mode8);
        Enemy fire_king_copy = new Enemy("Boss", "fire_king_copy", 100, 10, 5, mode9);
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
        //复制体
        Register_Enemy_Object("fire_ghost_copy", Enemy_Create(), fire_ghost_copy);
        Register_Enemy_Object("fire_insect_copy", Enemy_Create(), fire_insect_copy);
        Register_Enemy_Object("fire_slime_copy", Enemy_Create(), fire_slime_copy);
        Register_Enemy_Object("fire_puppet_copy", Enemy_Create(), fire_puppet_copy);
        Register_Enemy_Object("fire_specter_copy", Enemy_Create(), fire_specter_copy);
        Register_Enemy_Object("fire_dog_copy", Enemy_Create(), fire_dog_copy);
        Register_Enemy_Object("fire_knight_copy", Enemy_Create(), fire_knight_copy);
        Register_Enemy_Object("fire_monster_copy", Enemy_Create(), fire_monster_copy);
        Register_Enemy_Object("fire_witch_copy", Enemy_Create(), fire_witch_copy);
        Register_Enemy_Object("fire_king_copy", Enemy_Create(), fire_king_copy);
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
        #region 商店卡牌实例入库
        //攻击卡
        Register_Shop_Object("diamond3", Shop_Create());
        Register_Shop_Object("diamond4", Shop_Create());
        Register_Shop_Object("diamond5", Shop_Create());
        Register_Shop_Object("diamond6", Shop_Create());
        Register_Shop_Object("diamond7", Shop_Create());
        Register_Shop_Object("diamond8", Shop_Create());
        Register_Shop_Object("diamond9", Shop_Create());
        Register_Shop_Object("diamond10", Shop_Create());
        Register_Shop_Object("diamondJ", Shop_Create());
        Register_Shop_Object("diamondQ", Shop_Create());
        Register_Shop_Object("diamondK", Shop_Create());
        Register_Shop_Object("diamondA", Shop_Create());
        //防御卡
        Register_Shop_Object("spade3", Shop_Create());
        Register_Shop_Object("spade4", Shop_Create());
        Register_Shop_Object("spade5", Shop_Create());
        Register_Shop_Object("spade6", Shop_Create());
        Register_Shop_Object("spade7", Shop_Create());
        Register_Shop_Object("spade8", Shop_Create());
        Register_Shop_Object("spade9", Shop_Create());
        Register_Shop_Object("spade10", Shop_Create());
        Register_Shop_Object("spadeJ", Shop_Create());
        Register_Shop_Object("spadeQ", Shop_Create());
        Register_Shop_Object("spadeK", Shop_Create());
        Register_Shop_Object("spadeA", Shop_Create());
        //装备卡
        Register_Shop_Object("club3", Shop_Create());
        Register_Shop_Object("club4", Shop_Create());
        Register_Shop_Object("club5", Shop_Create());
        Register_Shop_Object("club6", Shop_Create());
        Register_Shop_Object("club7", Shop_Create());
        Register_Shop_Object("club8", Shop_Create());
        Register_Shop_Object("club9", Shop_Create());
        Register_Shop_Object("club10", Shop_Create());
        Register_Shop_Object("clubJ", Shop_Create());
        Register_Shop_Object("clubQ", Shop_Create());
        Register_Shop_Object("clubK", Shop_Create());
        Register_Shop_Object("clubA", Shop_Create());
        //诅咒卡
        Register_Shop_Object("curse1", Shop_Create());
        Register_Shop_Object("curse2", Shop_Create());
        Register_Shop_Object("curse3", Shop_Create());
        Register_Shop_Object("curse4", Shop_Create());
        Register_Shop_Object("curse5", Shop_Create());
        //技能卡
        Register_Shop_Object("heart3", Shop_Create());
        Register_Shop_Object("heart4", Shop_Create());
        Register_Shop_Object("heart5", Shop_Create());
        Register_Shop_Object("heart6", Shop_Create());
        Register_Shop_Object("heart7", Shop_Create());
        Register_Shop_Object("heart8", Shop_Create());
        Register_Shop_Object("heart9", Shop_Create());
        Register_Shop_Object("heart10", Shop_Create());
        Register_Shop_Object("heartJ", Shop_Create());
        Register_Shop_Object("heartQ", Shop_Create());
        Register_Shop_Object("heartK", Shop_Create());
        Register_Shop_Object("heartA", Shop_Create());
        #endregion
        #region 装备卡牌实例入库
        Register_Equip_Object("club3", Card_Create());
        Register_Equip_Object("club4", Card_Create());
        Register_Equip_Object("club5", Card_Create());
        Register_Equip_Object("club6", Card_Create());
        Register_Equip_Object("club7", Card_Create());
        Register_Equip_Object("club8", Card_Create());
        Register_Equip_Object("club9", Card_Create());
        Register_Equip_Object("club10", Card_Create());
        Register_Equip_Object("clubJ", Card_Create());
        Register_Equip_Object("clubQ", Card_Create());
        Register_Equip_Object("clubK", Card_Create());
        Register_Equip_Object("clubA", Card_Create());
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
        GameObject prefab = Resources.Load<GameObject>("Prefabs/card");
        GameObject select = Instantiate(prefab, transform);
        select.SetActive(false);
        return select;
    }
    GameObject Shop_Create()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/shop");
        GameObject select = Instantiate(prefab, transform);
        select.SetActive(false);
        return select;
    }
    void Register_Card_Object(string type, GameObject obj, Card card)
    {
        string path = "Card/" + card.type + "/" + card.point_show;
        Image image = obj.GetComponent<Image>();
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite != null) image.sprite = sprite;
        else Debug.Log("can not find sprite:" + path);

        Message.Msg.card_bank.Add(type);
        if (!Message.Msg.instance_card.ContainsKey(type))
        {
            Message.Msg.instance_card.Add(type, obj);
        }
        if (!Message.Msg.data_card.ContainsKey(type))
        {
            Message.Msg.data_card.Add(type, card);
        }
        obj.AddComponent<InfoManager>();
        //判断是否为装备卡
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
        string path = "Card/" + manager.Get_Card_Data(type).type + "/" + manager.Get_Card_Data(type).point_show;
        Image image = obj.GetComponent<Image>();
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite != null) image.sprite = sprite;
        else Debug.Log("can not find sprite:" + path);

        if (!Message.Msg.instance_select.ContainsKey(type))
        {
            Message.Msg.instance_select.Add(type, obj);
        }
        obj.GetComponent<Button>().onClick.AddListener(() =>
        {
            manager.Get_Now_Card(obj);
        });
        obj.AddComponent<InfoManager>();
    }
    void Register_Shop_Object(string type, GameObject obj)
    {
        string path = "Card/" + manager.Get_Card_Data(type).type + "/" + manager.Get_Card_Data(type).point_show;
        Image image = obj.GetComponent<Image>();
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite != null) image.sprite = sprite;
        else Debug.Log("can not find sprite:" + path);

        if (!Message.Msg.instance_shop.ContainsKey(type))
        {
            Message.Msg.instance_shop.Add(type, obj);
        }
        obj.AddComponent<InfoManager>();
    }
    void Register_Equip_Object(string type, GameObject obj)
    {
        string path = "Card/" + manager.Get_Card_Data(type).type + "/" + manager.Get_Card_Data(type).point_show;
        Image image = obj.GetComponent<Image>();
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite != null) image.sprite = sprite;
        else Debug.Log("can not find sprite:" + path);

        if (!Message.Msg.instance_equip.ContainsKey(type))
        {
            Message.Msg.instance_equip.Add(type, obj);
        }
        obj.GetComponent<Button>().onClick.AddListener(() =>
        {
            manager.OnEquipClick(type);
        });
        obj.AddComponent<InfoManager>();
    }
}
