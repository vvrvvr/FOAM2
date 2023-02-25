using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequence : MonoBehaviour
{
    public float jumpForce = 1f;   // Сила толчка по оси x
    public float returnForce = 0.1f;  // Сила возврата в исходное положение по оси x

    private Rigidbody rb;           // Ссылка на Rigidbody
    private bool isJumping = false; // Флаг, указывающий, происходит ли в данный момент толчок
    private Vector3 initialPosition; // Начальная позиция тела
    public Transform anchorTransform;
    public Rigidbody AnchorRb;

    void Start()
    {
        rb = AnchorRb;
        initialPosition = anchorTransform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            // Добавляем силу вправо по оси x
            rb.AddRelativeForce(Vector3.right * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }

        // // Если тело находится далеко от исходной позиции, возвращаем его
        // if (Mathf.Abs(anchorTransform.position.x - initialPosition.x) > 0.3f)
        // {
        //     float direction = Mathf.Sign(anchorTransform.position.x - initialPosition.x);
        //     rb.AddForce(Vector3.left * direction * returnForce, ForceMode.Impulse);
        // }
    }
}
