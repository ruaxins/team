using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

// ������������ί������
public delegate void ParameterDelegate(string message);
public class TurnManager : MonoBehaviour
{
    private ParameterDelegate multiDelegate;
    GameObject screen;
    Manager manager = new Manager();
    Skills skill = new Skills();
    public enum TurnPhase
    {
        PlayerTurn,
        EnemyTurn,
        TurnEnd,
        SelectTurn
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
        GameObject.Find("Fight").GetComponent<Button>().enabled = true;
        // ��һغϿ�ʼ�߼�
        Debug.Log("��һغϿ�ʼ");
        //�غϿ�ʼ����
        if (manager.Search_equipment("club3")) skill.Get_skills("club3", Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("club4")) skill.Get_skills("club4", Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("club5")) skill.Get_skills("club5", Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("club8")) skill.Get_skills("club8", Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("curse4")) skill.Get_skills("curse4", Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("curse5")) skill.Get_skills("curse5", Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("clubA"))
        {
            Round_Message.RMsg.select_action.Add("clubA");
            GameObject.Find("RoundManager").GetComponent<Extra_Select_Manager>().StartSelectTurn();
        }
        //
        manager.Data_clear_round();
        //���ƿⲹ�俨��������
        if (Round_Message.RMsg.hand_in_instances.Count < Round_Message.RMsg.Hand_in_card_num_max)
            for (int i = Round_Message.RMsg.hand_in_instances.Count; i < Round_Message.RMsg.Hand_in_card_num_max; i++)
            {
                if (Round_Message.RMsg.bank_in_instances.Count <= 0) break;
                manager.Get_card();
                if (manager.Search_equipment("club10")) skill.Get_skills("club10", Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
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
        //�غϽ����ж�
        foreach (string action in Round_Message.RMsg.round_end_action)
        {
            skill.Get_skills(action, Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        }
        //�غϽ�������
        if (manager.Search_equipment("clubK")) skill.Get_skills("clubK", Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        //��������
        Round_Message.RMsg.Player.player_health -= (int)(Round_Message.RMsg.Player.player_health * 0.05f * Round_Message.RMsg.Player.player_hurt);
        //�������
        manager.Data_clear_endround();
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
        if (Round_Message.RMsg.Enemy_Call != null)
        {
            if (!Round_Message.RMsg.enemy_instances.Contains(Round_Message.RMsg.Enemy_Call)) Round_Message.RMsg.enemy_instances.Add(Round_Message.RMsg.Enemy_Call);
            Round_Message.RMsg.Enemy_Call = null;
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
