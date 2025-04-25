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
    //����غ�
    public void EndPlayerTurn()
    {
        CurrentPhase = TurnPhase.EnemyTurn;
        StartEnemyTurn();
    }
    //�غϽ���
    public void EndEnemyTurn()
    {
        CurrentPhase = TurnPhase.TurnEnd;
        StartTurnEnd();
    }
    //����
    public void StartNewTurn()
    {
        CurrentPhase = TurnPhase.PlayerTurn;
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        screen.SetActive(false);
        // ��һغϿ�ʼ�߼�
        Debug.Log("��һغϿ�ʼ");
        manager.Data_clear_player(Round_Message.RMsg.Player, false);
        //���ƿⲹ�俨��������
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
        // ����غϿ�ʼ�߼�
        Debug.Log("����غϿ�ʼ");
        StartCoroutine(EnemyTurnActions());
    }

    private void StartTurnEnd()
    {
        // �غϽ����߼�
        Debug.Log("�غϽ����׶�");
        Round_Message.RMsg.Player.player_health -= (int)(Round_Message.RMsg.Player.player_health * 0.05f * Round_Message.RMsg.Player.player_hurt);
        //�������
        manager.Data_clear_player(Round_Message.RMsg.Player, true);
        manager.Data_clear_enemy(Round_Message.RMsg.Enemy_Now);
        manager.Data_clear_card();
        // ��ʼ�»غ�
        StartNewTurn();
    }

    private IEnumerator EnemyTurnActions()
    {
        // ģ�����˼��ʱ��
        yield return new WaitForSeconds(1f);

        // ִ��ÿ��������ж�
        foreach (string enemy in Round_Message.RMsg.enemy_instances)
        {
            yield return PerformAction(enemy);
        }

        // �����ж�������
        EndEnemyTurn();
    }
    public IEnumerator PerformAction(string type)
    {
        //���Ŷ���
        //animator.Play("Prepare");
        Enemy enemy = manager.Get_Enemy_Data(type);
        //�л�����ģʽ
        enemy.Attack_mode_change(Round_Message.RMsg.Player, enemy);
        yield return new WaitForSeconds(0.5f);

    }
}
