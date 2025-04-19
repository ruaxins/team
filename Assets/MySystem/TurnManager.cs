using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    Skills skills = new Skills();
    public enum TurnState { PlayerTurn, EnemyTurn, Animating, TurnEnd }
    public TurnState currentState;

    public void EndTurn()
    {
        if (currentState != TurnState.PlayerTurn) return;

        StartCoroutine(TurnEndSequence());
    }

    IEnumerator TurnEndSequence()
    {
        // 1. 进入动画状态
        currentState = TurnState.Animating;

        // 2. 播放回合结束动画
        Animation anim = GetComponent<Animation>();
        anim.Play("TurnEndAnim");

        // 3. 等待动画完成
        yield return new WaitWhile(() => anim.isPlaying);

        // 4. 触发回合结束效果
        TriggerTurnEndEffects();

        // 5. 切换回合状态
        currentState = TurnState.EnemyTurn;
        StartEnemyTurn();
    }

    void TriggerTurnEndEffects()
    {
        // 触发卡牌效果、状态结算等
        foreach (string action in Round_Message.RMsg.round_end_action)
        {
            skills.Get_skills(action, Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        }
    }
    void StartEnemyTurn()
    {

    }
}
