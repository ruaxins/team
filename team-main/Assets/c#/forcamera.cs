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
        // 鼠标输入
        if (isMouseLocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // 垂直视角旋转（摄像机）
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // 水平旋转（角色身体）
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
        Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标到屏幕中心
        Cursor.visible = false; // 隐藏鼠标
    }

    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None; // 解锁鼠标
        Cursor.visible = true; // 显示鼠标
    }
}