using System;
using UnityEngine;

[Serializable]
public class TraceState<T> : IState where T : Monster
{
    [SerializeField] T owner;
    [SerializeField] float moveSpeed;

    private Transform playerTransform;

    public void Enter()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    public void Update()
    {
        Vector2 dir = (playerTransform.position - owner.transform.position).normalized;
        owner.transform.Translate(dir * moveSpeed*Time.deltaTime);
    }

    public void Exit()
    {
        
    }

    
}
