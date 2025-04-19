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
        // 1. ���붯��״̬
        currentState = TurnState.Animating;

        // 2. ���ŻغϽ�������
        Animation anim = GetComponent<Animation>();
        anim.Play("TurnEndAnim");

        // 3. �ȴ��������
        yield return new WaitWhile(() => anim.isPlaying);

        // 4. �����غϽ���Ч��
        TriggerTurnEndEffects();

        // 5. �л��غ�״̬
        currentState = TurnState.EnemyTurn;
        StartEnemyTurn();
    }

    void TriggerTurnEndEffects()
    {
        // ��������Ч����״̬�����
        foreach (string action in Round_Message.RMsg.round_end_action)
        {
            skills.Get_skills(action, Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        }
    }
    void StartEnemyTurn()
    {

    }
}
