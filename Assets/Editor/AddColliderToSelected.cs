using UnityEngine;
using UnityEditor;

public class AddColliderToSelected : EditorWindow
{
    [MenuItem("Tools/������� BoxCollider")]
    static void AddBoxCollider()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            if (obj.GetComponent<Collider>() == null)
            {
                Undo.AddComponent<BoxCollider>(obj); // ֧�ֳ���
            }
        }

        Debug.Log("��Ϊѡ�е�������� BoxCollider");
    }
}
