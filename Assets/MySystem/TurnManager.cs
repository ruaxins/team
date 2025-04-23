using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    GameObject screen;
    Manager manager = new Manager();
    public enum TurnPhase
    {
        PlayerTurn,
        EnemyTurn,
        TurnEnd
    }

    public TurnPhase CurrentPhase { get; private set; }

    private void Start()
    {
        screen = GameObject.Find("Enemy_Round");
        CurrentPhase = TurnPhase.PlayerTurn;
        StartPlayerTurn();
    }
    //怪物回合
    public void EndPlayerTurn()
    {
        CurrentPhase = TurnPhase.EnemyTurn;
        StartEnemyTurn();
    }
    //回合结束
    public void EndEnemyTurn()
    {
        CurrentPhase = TurnPhase.TurnEnd;
        StartTurnEnd();
    }
    //出牌
    public void StartNewTurn()
    {
        CurrentPhase = TurnPhase.PlayerTurn;
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        screen.SetActive(false);
        // 玩家回合开始逻辑
        Debug.Log("玩家回合开始");
        manager.Data_clear_player(Round_Message.RMsg.Player, false);
        //从牌库补充卡牌至上限
        if (Round_Message.RMsg.hand_in_instances.Count < Round_Message.RMsg.Hand_in_card_num_max)
            for (int i = Round_Message.RMsg.hand_in_instances.Count; i < Round_Message.RMsg.Hand_in_card_num_max; i++)
            {
                if (Round_Message.RMsg.bank_in_instances.Count <= 0) break;
                manager.Get_card();
            }
        GameObject.Find("RoundManager").GetComponent<Combat_System>().Flash_pos();
    }
    private void StartEnemyTurn()
    {
        screen.SetActive(true);
        // 怪物回合开始逻辑
        Debug.Log("怪物回合开始");
        StartCoroutine(EnemyTurnActions());
    }

    private void StartTurnEnd()
    {
        // 回合结束逻辑
        Debug.Log("回合结束阶段");
        Round_Message.RMsg.Player.player_health -= (int)(Round_Message.RMsg.Player.player_health * 0.05f * Round_Message.RMsg.Player.player_hurt);
        //数据清空
        manager.Data_clear_player(Round_Message.RMsg.Player, true);
        manager.Data_clear_enemy(Round_Message.RMsg.Enemy_Now);
        manager.Data_clear_card();
        // 开始新回合
        StartNewTurn();
    }

    private IEnumerator EnemyTurnActions()
    {
        // 模拟怪物思考时间
        yield return new WaitForSeconds(1f);

        // 执行每个怪物的行动
        foreach (string enemy in Round_Message.RMsg.enemy_instances)
        {
            yield return PerformAction(enemy);
        }

        // 怪物行动结束后
        EndEnemyTurn();
    }
    public IEnumerator PerformAction(string type)
    {
        //播放动画
        //animator.Play("Prepare");
        Enemy enemy = manager.Get_Enemy_Data(type);
        //切换攻击模式
        enemy.Attack_mode_change(Round_Message.RMsg.Player, enemy);
        yield return new WaitForSeconds(0.5f);

    }
}
