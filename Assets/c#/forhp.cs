using UnityEngine;

public class forhp : MonoBehaviour
{
    public RectTransform targetUIElement; // ��Ҫ������ȵ�UIԪ��
    public GameObject person;

    void Update()
    {
        // ��̬���ÿ��
        buff bu = person.GetComponent<buff>();
        targetUIElement.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bu.hp);
    }
}