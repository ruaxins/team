using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // ��ת������Ϸ���������硰GameScene��
        SceneManager.LoadScene("Fire");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Fire");
    }

   //public void ShowCredits()
   //{
   //    // ��ת��������Ա����
   //    SceneManager.LoadScene("CreditsScene");
   //}
}
