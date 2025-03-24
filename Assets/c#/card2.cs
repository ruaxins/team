using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Security.Cryptography;
using System.Reflection.Emit;

public class card2 : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    public float hoverHeight = 80f;
    public float hoverScale = 1.2f;
    public bool IsHovered;
    public bool IsChoiced;
    public bool IsDragging;
    public card controller;
    public bool IsDrawed;
    public bool IsFolded;

    private cardsee manager;
    private RectTransform rt;
    private Vector2 targetPosition;
    private float targetRotation;
    private Vector3 targetScale = Vector3.one;

    private bool isLifted = false;
    private Vector2 originalPosition;

    public void Initialize(cardsee manager, int index)
    {
        this.manager = manager;
        rt = GetComponent<RectTransform>();
    }

    public void SetTarget(Vector2 position, float rotation, Vector3 scale)
    {
        originalPosition = position;
        isLifted = false;
        targetPosition = position;
        targetRotation = rotation;
        targetScale = scale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) IsFolded = true; 
        if (Input.GetMouseButtonDown(0))IsDragging = true;
        if (Input.GetMouseButtonUp(0))
        {
            IsDragging = false;
            isLifted = false;
            IsChoiced = false;
        }
        if (IsFolded)
        {
            foldc = manager.foldc;
            targetPosition = foldc.position;
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, foldc.position,
                Time.deltaTime * manager.positionLerpSpeed);
            return;
        }
        if (IsDragging && IsChoiced)
        {
            Vector2 mousePos = Input.mousePosition;



            // 将屏幕坐标转换为UI坐标

            Vector2 localPos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(

                rt.parent as RectTransform,

                mousePos,

                null,

                out localPos

            );
            rt.anchoredPosition = localPos;
            rt.localRotation = Quaternion.Slerp(rt.localRotation,
            Quaternion.Euler(0, 0, 0),
            Time.deltaTime * manager.rotationLerpSpeed);
            return;
        }

        rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPosition,
            Time.deltaTime * manager.positionLerpSpeed);
        rt.localRotation = Quaternion.Slerp(rt.localRotation,
            Quaternion.Euler(0, 0, targetRotation),
            Time.deltaTime * manager.rotationLerpSpeed);
        rt.localScale = Vector3.Lerp(rt.localScale, targetScale, Time.deltaTime * 10f);

        targetPosition = originalPosition + (isLifted ? new Vector2(0, hoverHeight) : Vector2.zero);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovered = true;
        targetScale = Vector3.one * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovered = false;
        targetScale = Vector3.one;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !IsDragging)
        {
            isLifted = !isLifted;
            IsChoiced=true;
        }
    }

    private void OnLeftMouseDown()
    {
    }
    private void OnLeftMousepUp()
    {
        IsDragging = false;
        isLifted = false;
    }

    public Transform foldc;
    void fold()
    {
        foldc = manager.foldc;
        targetPosition = foldc.position;
        rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, foldc.position,
            Time.deltaTime * manager.positionLerpSpeed);
        IsFolded = true;
    }
}