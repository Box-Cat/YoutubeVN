using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    public float rotationSpeed;
    private Vector2 direction;
    public float moveSpeed;
    public Vector2 offsetFromMouse = Vector2.zero;

    void Update()
    {
        direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetFromMouse - (Vector2)transform.position;
        float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);


        Vector2 cursorPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetFromMouse;
        transform.position = Vector2.MoveTowards(transform.position,cursorPos,moveSpeed * Time.deltaTime);
    }
}
