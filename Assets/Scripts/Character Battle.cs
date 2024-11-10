using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class CharacterBattle : MonoBehaviour
{
    private KnightControl characterBase;
    private State state;
    private Vector3 slideTargetPosition;
    private Action onSlideComplete;
    private HealthSystem healthSystem;
    // Start is called before the first frame update
    private World_Bar healthBar;

    private enum State
    {
        Idle,
        Sliding,
        Busy,
    }
    private void Awake()
    {
        characterBase = GetComponent<KnightControl>();
        state = State.Idle;
    }
    void Start()
    {
        //characterBase.running();
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Busy:
                break;
            case State.Sliding:
                float slideSpeed = 10f;
                transform.position += (slideTargetPosition - GetPosition()) * slideSpeed * Time.deltaTime;
                float reachedDistance = 1f;
                if (Vector3.Distance(GetPosition(), slideTargetPosition) < reachedDistance)
                {
                    transform.position = slideTargetPosition;
                    onSlideComplete();
                }
                break;

        }
    }

    public void Setup(bool isPlayerTeam)
    {
        if (isPlayerTeam)
        {
            characterBase.idle();
        }
        else
        {
            characterBase.idle();
        }
        healthSystem = new HealthSystem(100);
        healthBar = new World_Bar(
            transform,                    // Parent transform
            new Vector3(0, 4),           // Position of the bar
            new Vector3(2, 0.15f),         // Scale (width, height) - adjust width as needed for health bar
            Color.grey,                   // Background color
            Color.red,                    // Health bar color (fill color)
            1f,                           // Health ratio (100% health to start)
            100,                          // Sorting order
            new World_Bar.Outline { color = Color.black, size = 0.3f } // Outline with color and size
        );
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        healthBar.SetSize(healthSystem.GetHealthPercent());
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Damage(int damageAmount)
    {
        //Debug.Log("Damaged:" + damageAmount);
        healthSystem.Damage(damageAmount);
        state = State.Busy;
        characterBase.getHit();
        if (healthSystem.IsDead())
        {
            StartCoroutine(HandleDeath());
        }
        state = State.Idle;
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(0.5f);
        characterBase.death();
        yield return new WaitForSeconds(2f); // Adjust time for death animation
    }

    public bool isDead()
    {
        return healthSystem.IsDead();
    }

    public void Attack(CharacterBattle targetCharacterBattle, Action onAttackComplete)
    {
        Vector3 targetPosition = targetCharacterBattle.GetPosition();
        Vector3 startingPosition = GetPosition();

        // Calculate a position slightly in front of the target
        Vector3 directionToTarget = (targetPosition - startingPosition).normalized;
        Vector3 slideTargetPosition = targetPosition - directionToTarget * 3.0f; // Adjust "3.0f" as needed

        // Slide to the adjusted position in front of the target
        SlideToPosition(slideTargetPosition, () =>
        {
            state = State.Busy;

            int attackType = UnityEngine.Random.Range(0, 2); // Randomly selects 0 or 1
            if (attackType == 0)
            {
                characterBase.attack_1();
            }
            else
            {
                characterBase.attack_2();
            }
            int damageAmount = UnityEngine.Random.Range(20, 50);

            // Start a coroutine to manage the attack and damage sequence
            StartCoroutine(WaitAndApplyDamage(targetCharacterBattle, damageAmount, startingPosition, onAttackComplete));
        });
    }

    // Coroutine to wait for attack animation, apply damage, and handle sliding back
    private IEnumerator WaitAndApplyDamage(CharacterBattle targetCharacterBattle, int damageAmount, Vector3 startingPosition, Action onAttackComplete)
    {
        // Wait for the attack animation to complete (adjust duration as needed)
        yield return new WaitForSeconds(0.5f);

        // Apply damage and trigger the gethit animation on the target
        targetCharacterBattle.Damage(damageAmount);

        // Wait briefly to let the gethit animation play before sliding back
        yield return new WaitForSeconds(0.5f);

        // Slide back to the starting position
        SlideToPosition(startingPosition, () =>
        {
            state = State.Idle;
            onAttackComplete();
        });
    }


    private void SlideToPosition(Vector3 slideTargetPosition, Action onSlideComplete)
    {
        this.slideTargetPosition = slideTargetPosition;
        this.onSlideComplete = onSlideComplete;
        state = State.Sliding;
    }
}
