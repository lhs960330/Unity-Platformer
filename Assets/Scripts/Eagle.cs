using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

// 디자인 패턴 State
// 객체에게 한번에 하나의 상태만을 가지게 하며
// 객체는 현재 상태에 해당하는 행동만을 진행함
// 몬스터와같은 곳에 자주 사용됨

// 구현
// 1. 열거형 자료형으로 객체가 가질 수 있는 상태들을 정의
// 2. 현재 상태를 저장하는 변수에 초기 상태를 지정
// 3. 객체는 행동에 있어서 현재 상태만의 행동을 진행
// 4. 객체는 현재 상태의 행동을 진행 한 후 상태 변화에 대해 판단
// 5. 상태 변화가 있어야 하는 경우 현재 상태를 대상 상태로 변경
// 6. 상태가 변경된 경우 다음 행동에 있어서 바뀐 상태만의 행동을 진행
//
// 장점
// 1. 객체가 진행할 행동을 복잡한 조건문을 상태로 처리가 가능하므로, 조건 처리에 대한 부담이 적음
// 2. 객체가 가지는 여러상태에 대한 현재상태만을 진행하므로, 연산속도가 뛰어남
// 3. 객체와 관련된 모든 동작을 각각의 상태에 분산시키므로, 코드가 간결하고 가독성이 좋음
//
// 단점
// 1. 상태의 구분이 명확하지 않거나 갯수가 많은 경우, 상태 변경 코드가 복잡해질 수 있음
// 2. 간단한 동작의 객체에 상태패턴을 적용하는 경우, 오히려 관리할 상태 코드량이 증가하게 됨
// 3. 상태를 캡슐화 하지 않는 경우 상태간의 간섭이 가능하므로, 개방폐쇄원칙이 준수되지 않음


public class Eagle : Monster
{
    // 디자인패턴중 상태패턴이다.
    // 상태를 enum으로 나누어준다.
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
        // Idle 상태를 넣어준다.
        //fsm.AddState(State.Idle, idleState);
        //fsm.AddState(State.Trace, traceState);
        //fsm.AddState(State.Return, returnState);

    }

    private void Start()
    {
        startPos = transform.position;
        /*  fsm = new StateMachine<State, Eagle>();

  // Idle 상태를 넣어준다.
  fsm.AddState(State.Idle, idleState);
  fsm.AddState(State.Trace, traceState);
  //fsm.AddState(State.Return, returnState);
  //curState = State.Idle;
  fsm.SetInitState(State.Idle);*/
        fsm = new StateMachine(this);

    }
    // 업데이트에서 변경되지 않는 자료들을 쓰지말자

    private void Update()
    {
        fsm.Update();


        /*
        if ()
        {
            fsm.ChangeState(State.Trace);
        }
        */
        /*        // 이처럼 사용하여 Update에서 하나의 연산만 하게 해준다.
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
        /* // 플레이어는 움직이니 여기서 계속 확인해야됨
         // Vector3 playerPos = playerTransform.position;


         // normalized 는 방향 
         //Vector2 dir = (startPos - transform.position).normalized;
         // magnitude는 크기
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
            // 처음 시작할 때 확인
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
        public void Update()
        {
            // 아무것도 안함
            if (Vector2.Distance(playerTransform.position, owner.transform.position) < findRange)
            {
                // 상태 변경
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
            // 플레이어한테 간다.
            Vector3 dir = (playerTransform.position - owner.transform.position).normalized;
            owner.transform.Translate(dir * moveSpeed * Time.deltaTime);
            // 멀어졌을때는 쫓아가지 않게된다.
            // 이 함수는 쫒아가는거만 하고 돌아가는 건 Retur한테 넘겨준다.
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
            // 원래 자리로 돌아감
            Vector3 dir = (owner.startPos - owner.transform.position).normalized;
            owner.transform.Translate(dir * moveSpeed * Time.deltaTime);
            // 돌아가서 가만히 있어야되니 Idle로 다시 보내줌
            if (Vector2.Distance(owner.transform.position, owner.startPos) < 0.01f)
            {
                owner.ChangState("Idle");
            }

        }
        public void Exit()
        {

        }
    }*/
    /*    // 각 함수에 해야할 행동들만 반복시켜주면된다.
        private void IdleUpdate()
        {
            // 아무것도 안하는 상태
            // Distance 두 지점의 거리를 계산해줌
            if (Vector2.Distance(playerTransform.position, transform.position) < findRange)
            {
                // Idle은 가만히 있는 상태이기 때문에 Trace로 넘겨준다.
                curState = State.Trace;
            }
        }
        private void TraceUpdate()
        {
            // 플레이어한테 간다.
            Vector3 dir = (playerTransform.position - transform.position).normalized;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            // 멀어졌을때는 쫓아가지 않게된다.
            // 이 함수는 쫒아가는거만 하고 돌아가는 건 Retur한테 넘겨준다.
            if (Vector2.Distance(playerTransform.position, transform.position) > findRange)
            {
                curState = State.Return;
            }
        }
        private void ReturUpdate()
        {
            // 원래 자리로 돌아감
            Vector3 dir = (startPos - transform.position).normalized;
            transform.Translate(dir*moveSpeed * Time.deltaTime);
            // 돌아가서 가만히 있어야되니 Idle로 다시 보내줌
            if(Vector2.Distance(transform.position , startPos) < 0.01f)
            {
                curState = State.Idle;
            }
            // 돌아가다 플레이어가 다가오면 따라가게 Trace로 보내줌
            if(Vector2.Distance(transform.position, playerTransform.position) < findRange)
            {
                curState = State.Trace;
            }
        }
        private void DieUpdate()
        {

        }*/
}

