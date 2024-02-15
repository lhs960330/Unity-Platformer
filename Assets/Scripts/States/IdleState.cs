using System;
using UnityEngine;

//직렬화 가능하다 
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