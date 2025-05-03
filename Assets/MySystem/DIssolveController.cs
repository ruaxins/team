using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class DIssolveController : MonoBehaviour
{
    public Material material;
    // �ܽ�Э�̴��ص�����
    IEnumerator DissolveWithCallback(float duration, System.Action<bool> onComplete)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float amount = Mathf.Lerp(0, 1, elapsed / duration);
            material.SetFloat("_Cutoff", amount);
            elapsed += Time.deltaTime;
            yield return null;
        }

        material.SetFloat("_Cutoff", 1f);
        onComplete?.Invoke(true); // �ɹ��ص�
    }

    public void StartDissolve_Play()
    {
        StartCoroutine(DissolveWithCallback(2.0f, (success) => {
            if (success)
            {
                Debug.Log("�ܽ���ɣ�����ִ����������");
                GameObject.Find("RoundManager").GetComponent<Combat_System>().Select_Open();
                material.SetFloat("_Cutoff", 0f);
            }
            else
            {
                Debug.LogError("�ܽ�ʧ��");
            }
        }));
    }
    public void StartDissolve_Drop()
    {
        StartCoroutine(DissolveWithCallback(2.0f, (success) => {
            if (success)
            {
                Debug.Log("�ܽ���ɣ�����ִ����������");
                GameObject.Find("RoundManager").GetComponent<Combat_System>().Drop();
                material.SetFloat("_Cutoff", 0f);
            }
            else
            {
                Debug.LogError("�ܽ�ʧ��");
            }
        }));
    }
}
