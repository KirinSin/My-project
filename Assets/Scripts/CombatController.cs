using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{

    private StateMachine meleeStateMachine;
    public CharacterController controller;


    [SerializeField] public Collider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0) && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            if (controller.m_Grounded) meleeStateMachine.SetNextState(new GroundEntryState());
            else{
                meleeStateMachine.SetNextState(new AirSlamState());
                controller.Slam();
            }
        }
    }
}