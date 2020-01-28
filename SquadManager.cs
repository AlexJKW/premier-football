using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    //Configuration parameters
    int squadSize = 5;

    // Start is called before the first frame update
    void Start()
    {
        SquadDisplay squadDisplay = FindObjectOfType<SquadDisplay>();
        GameStatus gameStatus = FindObjectOfType<GameStatus>();

        CardDisplay [] squadCards = squadDisplay.GetSquadSpaces();

        for(int i = 0; i < squadSize; i++)
        {
            squadCards[i].card = gameStatus.currentSquad[i];
            squadCards[i].UpdateCardVisuals();
        }
    }
}
