using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    
    [SerializeField] float speed;
    private Vector2 moverDir;

    private void FixedUpdate()
    {
        
    }
    private void OnMove(InputValue value)
    {
        moverDir = value.Get<Vector2>();
    }
    private void OnJump(InputValue value)
    {

    }
}
