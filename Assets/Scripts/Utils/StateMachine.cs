using System.Collections.Generic;
using UnityEngine;

public class StateMachine<TState, TOwner> where TOwner : Monster
{
    // Dictionary�� Ž�������� ���°� �ƴ϶� ���� ���µ��� ������ ������ ���ش�? (������?)
    private Dictionary<TState, IState> states = new Dictionary<TState, IState>();
    private IState curState;

    public void Update()
    {
        curState.Update();
    }
    // �ʱ� ���� ����
    public void SetInitState(TState type)
    {
        curState = states[type];
        // �ʱ�ȭ ���� 
        curState.Enter();
    }

    // ������¿��� �ٸ� ���� ��ȯ �Ҷ�
    public void ChangeState(TState type)
    {
        // ������¿��� �������Ҷ��� �ʿ�
        curState.Exit();
        // ������¸� ���� ���·� �ٲ���
        curState = states[type];
        // �ٲ� ���¿��� �ؾߵ� �۾����� ��������
        curState.Enter();
    }

    public void AddState(TState type, IState state)
    {
        states.Add(type, state);
    }
}
