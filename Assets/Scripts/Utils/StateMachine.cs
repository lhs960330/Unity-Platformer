using System.Collections.Generic;
using UnityEngine;

public class StateMachine<TState, TOwner> where TOwner : Monster
{
    // Dictionary를 탐색용으로 쓰는게 아니라 여러 상태들이 있을수 있으니 써준다? (보관용?)
    private Dictionary<TState, IState> states = new Dictionary<TState, IState>();
    private IState curState;

    public void Update()
    {
        curState.Update();
    }
    // 초기 상태 지정
    public void SetInitState(TState type)
    {
        curState = states[type];
        // 초기화 진행 
        curState.Enter();
    }

    // 현재상태에서 다른 상태 전환 할때
    public void ChangeState(TState type)
    {
        // 현재상태에서 마무리할때가 필요
        curState.Exit();
        // 현재상태를 다음 상태로 바꿔줌
        curState = states[type];
        // 바뀐 상태에서 해야될 작업들을 실행해줌
        curState.Enter();
    }

    public void AddState(TState type, IState state)
    {
        states.Add(type, state);
    }
}
