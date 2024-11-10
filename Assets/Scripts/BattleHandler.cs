using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{

    private static BattleHandler instance;
    public static BattleHandler GetInstance()
    {
        return instance;
    }
    [SerializeField] private Transform pfCharacterBattle;

    private CharacterBattle playerCharacterBattle;
    private CharacterBattle enemyCharacterBattle;

    private CharacterBattle activeCharacterBattle;
    private State state;
    private enum State
    {
        WaitingForPlayer,
        Busy,
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerCharacterBattle = SpawnCharacter(true);
        enemyCharacterBattle = SpawnCharacter(false);

        SetActiveCharacterBattle(playerCharacterBattle);
        state = State.WaitingForPlayer;
    }

    private void Update()
    {
        if (state == State.WaitingForPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = State.Busy;
                playerCharacterBattle.Attack(enemyCharacterBattle, () =>
                {
                    ChooseNextActiveCharacter();
                });
            }
        }
    }


    private CharacterBattle SpawnCharacter(bool isPlayerTeam)
    {
        if (isPlayerTeam)
        {
            Transform characterRight = Instantiate(pfCharacterBattle, new Vector3(-5, -3), Quaternion.identity);
            CharacterBattle characterBattleR = characterRight.GetComponent<CharacterBattle>();
            characterBattleR.Setup(isPlayerTeam);
            return characterBattleR;
        }
        else
        {
            Transform characterLeft = Instantiate(pfCharacterBattle, new Vector3(+5, -3), Quaternion.identity);
            characterLeft.localScale = new Vector3(-1, 1, 1); // Flip character on the x-axis to face left
            CharacterBattle characterBattleL = characterLeft.GetComponent<CharacterBattle>();
            characterBattleL.Setup(isPlayerTeam);
            return characterBattleL;
        }
    }

    private void SetActiveCharacterBattle(CharacterBattle characterBattle)
    {
        activeCharacterBattle = characterBattle;
    }

    private void ChooseNextActiveCharacter()
    {
        if (TestBattleOver()) {
            return;
        }
        if (activeCharacterBattle == playerCharacterBattle)
        {
            SetActiveCharacterBattle(enemyCharacterBattle);
            enemyCharacterBattle.Attack(playerCharacterBattle, () =>
            {
                ChooseNextActiveCharacter();
            });
        }
        else
        {
            SetActiveCharacterBattle(playerCharacterBattle);
            state = State.WaitingForPlayer;
        }
    }

    private bool TestBattleOver() {
        if (playerCharacterBattle.isDead()) {
            Debug.Log("Enemy Wins");
            return true;
        }
        if (enemyCharacterBattle.isDead()) {
            Debug.Log("Player Wins");
            return true;
        }
        return false;
    }

}

