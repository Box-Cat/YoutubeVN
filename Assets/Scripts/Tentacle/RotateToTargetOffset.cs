using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTargetOffset : MonoBehaviour
{
    public float rotationSpeed;
    public float moveSpeed;
    public Vector2 offsetFromMouse = Vector2.zero; // 마우스 기준 오프셋

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPos = mousePos + offsetFromMouse;

        Vector2 direction = targetPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}