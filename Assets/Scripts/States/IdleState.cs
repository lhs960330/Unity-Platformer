using System;
using UnityEngine;

//����ȭ �����ϴ� 
[Serializable]
public class IdleState<T> : IState where T : Monster
{
    [SerializeField] T owner;
    [SerializeField] float findRange = 5;

    
    private Transform playerTransform;
    public void Enter()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void Update()
    {
        if (Vector2.Distance(playerTransform.position, owner.transform.position) < findRange)
        {
            //fsm.ChangeState(State.Trace);
        }
    }

    public void Exit()
    {

    }


}