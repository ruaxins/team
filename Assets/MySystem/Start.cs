using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void StartGame()
    {
        // 跳转到新游戏场景，比如“GameScene”
        SceneManager.LoadScene("Fire");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Fire");
    }

   //public void ShowCredits()
   //{
   //    // 跳转到开发人员界面
   //    SceneManager.LoadScene("CreditsScene");
   //}
}
