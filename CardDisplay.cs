using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card = null;
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] TextMeshProUGUI positionText = null;
    [SerializeField] TextMeshProUGUI attackText = null;
    [SerializeField] TextMeshProUGUI defenseText = null;
    [SerializeField] TextMeshProUGUI overallText = null;
    [SerializeField] Image artworkImage = null;



    // Start is called before the first frame update
    void Start()
    {
        UpdateCardVisuals();
    }

    public void UpdateCardVisuals()
    {
        nameText.text = card.GetPlayerName();
        positionText.text = card.GetPlayerPosition();
        attackText.text = card.GetPlayerAttack().ToString();
        defenseText.text = card.GetPlayerDefense().ToString();
        overallText.text = card.GetPlayerOverall().ToString();
        artworkImage.sprite = card.GetPlayerImage();
        GetComponent<Image>().color = card.GetColor();
    }
    
}
