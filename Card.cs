using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Card")]

public class Card : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] string playerName = null;
    [SerializeField] string playerPosition = null;
    [SerializeField] int playerDefense = 0;
    [SerializeField] int playerAttack = 0;
    [SerializeField] int playerOverall = 0;
    [SerializeField] Sprite playerImage = null;

    public Color GetColor()
    {
        Color newColor;
        if (playerOverall < 75)
        {
            newColor = new Vector4(130f/256f, 74f/256f, 2f/256f, 1f);
        }
        else if (playerOverall < 85)
        {
            newColor = new Vector4(167f/256f, 167f/256f, 173f/256f, 1f);
        }
        else
        {
            newColor = Color.yellow;
        }

        return newColor;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public string GetPlayerPosition()
    {
        return playerPosition;
    }

    public int GetPlayerDefense()
    {
        return playerDefense;
    }

    public int GetPlayerAttack()
    {
        return playerAttack;
    }

    public int GetPlayerOverall()
    {
        return playerOverall;
    }

    public Sprite GetPlayerImage()
    {
        return playerImage;
    }

}
