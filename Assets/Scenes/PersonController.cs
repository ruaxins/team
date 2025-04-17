using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    [Header("Look Settings")]
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;
    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape)) Message.Msg.IsLock = !Message.Msg.IsLock;
        if (!Message.Msg.IsLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            HandleLook();
            HandleMovement();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ��ֱ��ת�����¿���
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // ˮƽ��ת������ת��
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        // ������
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ��΢��ѹ��ȷ����ұ����ڵ���
        }

        // ��ȡ����
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // ȷ���ƶ��ٶȣ��Ƿ�סShift�ܲ���
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // �����ƶ������������ҳ���
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // ��Ծ
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Ӧ������
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
