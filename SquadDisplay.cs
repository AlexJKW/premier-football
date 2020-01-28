using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadDisplay : MonoBehaviour
{
    [SerializeField] CardDisplay[] squadSpaces = null;

    public CardDisplay[] GetSquadSpaces()
    {
        return squadSpaces;
    }
}
