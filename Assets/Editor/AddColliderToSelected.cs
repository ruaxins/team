using UnityEngine;
using UnityEditor;

public class AddColliderToSelected : EditorWindow
{
    [MenuItem("Tools/批量添加 BoxCollider")]
    static void AddBoxCollider()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            if (obj.GetComponent<Collider>() == null)
            {
                Undo.AddComponent<BoxCollider>(obj); // 支持撤销
            }
        }

        Debug.Log("已为选中的物体添加 BoxCollider");
    }
}
