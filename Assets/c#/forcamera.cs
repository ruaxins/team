using UnityEngine;

public class forcamera : MonoBehaviour
{
    [Header("Settings")]
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public bool isMouseLocked;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // �������
        if (isMouseLocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // ��ֱ�ӽ���ת���������
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // ˮƽ��ת����ɫ���壩
            playerBody.Rotate(Vector3.up * mouseX);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            isMouseLocked = !isMouseLocked;
            if (isMouseLocked) LockMouse();
            else UnlockMouse();
        }
    }
    void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked; // ������굽��Ļ����
        Cursor.visible = false; // �������
    }

    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None; // �������
        Cursor.visible = true; // ��ʾ���
    }
}