using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

// ������ ���� State
// ��ü���� �ѹ��� �ϳ��� ���¸��� ������ �ϸ�
// ��ü�� ���� ���¿� �ش��ϴ� �ൿ���� ������
// ���ͿͰ��� ���� ���� ����

// ����
// 1. ������ �ڷ������� ��ü�� ���� �� �ִ� ���µ��� ����
// 2. ���� ���¸� �����ϴ� ������ �ʱ� ���¸� ����
// 3. ��ü�� �ൿ�� �־ ���� ���¸��� �ൿ�� ����
// 4. ��ü�� ���� ������ �ൿ�� ���� �� �� ���� ��ȭ�� ���� �Ǵ�
// 5. ���� ��ȭ�� �־�� �ϴ� ��� ���� ���¸� ��� ���·� ����
// 6. ���°� ����� ��� ���� �ൿ�� �־ �ٲ� ���¸��� �ൿ�� ����
//
// ����
// 1. ��ü�� ������ �ൿ�� ������ ���ǹ��� ���·� ó���� �����ϹǷ�, ���� ó���� ���� �δ��� ����
// 2. ��ü�� ������ �������¿� ���� ������¸��� �����ϹǷ�, ����ӵ��� �پ
// 3. ��ü�� ���õ� ��� ������ ������ ���¿� �л��Ű�Ƿ�, �ڵ尡 �����ϰ� �������� ����
//
// ����
// 1. ������ ������ ��Ȯ���� �ʰų� ������ ���� ���, ���� ���� �ڵ尡 �������� �� ����
// 2. ������ ������ ��ü�� ���������� �����ϴ� ���, ������ ������ ���� �ڵ差�� �����ϰ� ��
// 3. ���¸� ĸ��ȭ ���� �ʴ� ��� ���°��� ������ �����ϹǷ�, ��������Ģ�� �ؼ����� ����


public class Eagle : Monster
{
    // ������������ ���������̴�.
    // ���¸� enum���� �������ش�.
    /* public enum State
     {
         Idle,
         Trace,
         Return,
         Die,
     }*/

    public StateMachine fsm;
    public Vector3 startPos;

    private void Awake()
    {
        //fsm = new StateMachine<State, Eagle>();
        // Idle ���¸� �־��ش�.
        //fsm.AddState(State.Idle, idleState);
        //fsm.AddState(State.Trace, traceState);
        //fsm.AddState(State.Return, returnState);

    }

    private void Start()
    {
        startPos = transform.position;
        /*  fsm = new StateMachine<State, Eagle>();

  // Idle ���¸� �־��ش�.
  fsm.AddState(State.Idle, idleState);
  fsm.AddState(State.Trace, traceState);
  //fsm.AddState(State.Return, returnState);
  //curState = State.Idle;
  fsm.SetInitState(State.Idle);*/
        fsm = new StateMachine(this);

    }
    // ������Ʈ���� ������� �ʴ� �ڷ���� ��������

    private void Update()
    {
        fsm.Update();


        /*
        if ()
        {
            fsm.ChangeState(State.Trace);
        }
        */
        /*        // ��ó�� ����Ͽ� Update���� �ϳ��� ���길 �ϰ� ���ش�.
                switch (curState)
                {
                    case State.Idle:
                        IdleUpdate(); break;
                    case State.Trace:
                        TraceUpdate(); break;
                    case State.Return:
                        ReturUpdate(); break;
                    case State.Die:
                        DieUpdate(); break;

                }*/
        /* // �÷��̾�� �����̴� ���⼭ ��� Ȯ���ؾߵ�
         // Vector3 playerPos = playerTransform.position;


         // normalized �� ���� 
         //Vector2 dir = (startPos - transform.position).normalized;
         // magnitude�� ũ��
         //float scale = (playerPos - transform.position).magnitude;
         //transform.Translate(dir * moveSpeed * Time.deltaTime);*/
    }
    public void ChangeState(string stateName)
    {
        switch (stateName)
        {
            case "Idle":
                fsm.ChangState(fsm.idleState); break;
            case "Trace":
                fsm.ChangState(fsm.traceState); break;
            case "Return":
                fsm.ChangState(fsm.returnState); break;
        }
    }
    public class StateMachine
    {
        public IState curState;
        public IdleState idleState;
        public TraceState traceState;
        public ReturnState returnState;
        public StateMachine(Eagle eagle)
        {
            idleState = new IdleState(eagle);
            traceState = new TraceState(eagle);
            returnState = new ReturnState(eagle);

            curState = idleState;
            curState.Enter();
        }
        public void Update()
        {
            curState.Update();
        }
        public void ChangState(IState nextState)
        {
            curState.Exit();
            curState = nextState;
            curState.Enter();
        }

    }

    [Serializable]
    public class IdleState : IState
    {
        private Eagle eagle;
        private Transform playerTransform;
        private float range = 5f;
        public IdleState(Eagle eagle)
        {
            this.eagle = eagle;
        }

        public void Enter()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            Debug.Log("Idle Enter");
        }

        public void Exit()
        {
            Debug.Log("Idle Exit");
        }

        public void Update()
        {
            if(Vector2.Distance(playerTransform.position, eagle.transform.position) < range)
            {
                eagle.ChangeState("Trace");
            }
        }
    }
    [Serializable]
    public class TraceState : IState
    {
        private Eagle eagle;
        public TraceState(Eagle eagle)
        {
            this.eagle = eagle;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }
    [Serializable]
    public class ReturnState : IState
    {
        private Eagle eagle;
        public ReturnState(Eagle eagle)
        {
            this.eagle = eagle;
        }
        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }

    /*private class IdleState : IState
    {
        private Eagle owner;
        private Transform playerTransform;
        private float findRange = 5;

        public IdleState(Eagle owner)
        {
            this.owner = owner;
        }
        public void Enter()
        {
            // ó�� ������ �� Ȯ��
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
        public void Update()
        {
            // �ƹ��͵� ����
            if (Vector2.Distance(playerTransform.position, owner.transform.position) < findRange)
            {
                // ���� ����
                owner.ChangState("Trace");
            }
        }
        public void Exit()
        {

        }

    }
    private class TraceState : IState
    {
        private Eagle owner;
        private Transform playerTransform;
        private float findRange = 5;
        private float moveSpeed = 2;

        public TraceState(Eagle owner)
        {
            this.owner = owner;
        }
        public void Enter()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
        public void Update()
        {
            // �÷��̾����� ����.
            Vector3 dir = (playerTransform.position - owner.transform.position).normalized;
            owner.transform.Translate(dir * moveSpeed * Time.deltaTime);
            // �־��������� �Ѿư��� �ʰԵȴ�.
            // �� �Լ��� �i�ư��°Ÿ� �ϰ� ���ư��� �� Retur���� �Ѱ��ش�.
            if (Vector2.Distance(playerTransform.position, owner.transform.position) > findRange)
            {

            }
        }
        public void Exit()
        {

        }
    }
    private class ReturState : IState
    {
        private Eagle owner;
        private float moveSpeed = 10;

        public ReturState(Eagle owner)
        {
            this.owner = owner;
        }
        public void Enter()
        {

        }
        public void Update()
        {
            // ���� �ڸ��� ���ư�
            Vector3 dir = (owner.startPos - owner.transform.position).normalized;
            owner.transform.Translate(dir * moveSpeed * Time.deltaTime);
            // ���ư��� ������ �־�ߵǴ� Idle�� �ٽ� ������
            if (Vector2.Distance(owner.transform.position, owner.startPos) < 0.01f)
            {
                owner.ChangState("Idle");
            }

        }
        public void Exit()
        {

        }
    }*/
    /*    // �� �Լ��� �ؾ��� �ൿ�鸸 �ݺ������ָ�ȴ�.
        private void IdleUpdate()
        {
            // �ƹ��͵� ���ϴ� ����
            // Distance �� ������ �Ÿ��� �������
            if (Vector2.Distance(playerTransform.position, transform.position) < findRange)
            {
                // Idle�� ������ �ִ� �����̱� ������ Trace�� �Ѱ��ش�.
                curState = State.Trace;
            }
        }
        private void TraceUpdate()
        {
            // �÷��̾����� ����.
            Vector3 dir = (playerTransform.position - transform.position).normalized;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            // �־��������� �Ѿư��� �ʰԵȴ�.
            // �� �Լ��� �i�ư��°Ÿ� �ϰ� ���ư��� �� Retur���� �Ѱ��ش�.
            if (Vector2.Distance(playerTransform.position, transform.position) > findRange)
            {
                curState = State.Return;
            }
        }
        private void ReturUpdate()
        {
            // ���� �ڸ��� ���ư�
            Vector3 dir = (startPos - transform.position).normalized;
            transform.Translate(dir*moveSpeed * Time.deltaTime);
            // ���ư��� ������ �־�ߵǴ� Idle�� �ٽ� ������
            if(Vector2.Distance(transform.position , startPos) < 0.01f)
            {
                curState = State.Idle;
            }
            // ���ư��� �÷��̾ �ٰ����� ���󰡰� Trace�� ������
            if(Vector2.Distance(transform.position, playerTransform.position) < findRange)
            {
                curState = State.Trace;
            }
        }
        private void DieUpdate()
        {

        }*/
}

