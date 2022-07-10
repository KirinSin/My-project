using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{
    public float duration;
    protected Animator animator;
    protected bool shouldCombo;
    protected int attackIndex;

    protected Collider2D hitCollider;
    private List<Collider2D> collidersDamaged;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
        collidersDamaged = new List<Collider2D>();
        hitCollider = GetComponent<CombatController>().hitbox;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (animator.GetFloat("hitBoxActive") > 0f)
        {
            Attack();
        }

        if (animator.GetFloat("allowComboInput") > 0f && Input.GetMouseButtonDown(0))
        {
            shouldCombo = true;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    protected void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {

            if (!collidersDamaged.Contains(collidersToDamage[i]))
            {
                TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

                // Only check colliders with a valid Team Componnent attached
                if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
                {
                    CharacterController controller = GetComponent<CharacterController>();
                    int direction = controller.m_FacingRight ? 1 : -1;
                    Rigidbody2D e_Rigidbody2D = collidersToDamage[i].GetComponentInChildren<Rigidbody2D>();
                    e_Rigidbody2D.AddForce(new Vector2(direction * attackIndex * 20f, 0f));
                    Debug.Log("Enemy Has Taken:" + attackIndex + "Damage");
                    collidersDamaged.Add(collidersToDamage[i]);
                }
            }
        }
    }
}
