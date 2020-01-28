using UnityEngine;

public class PackManager : MonoBehaviour
{
    //Configuration parameters
    public Card[] cardDeck;
    CardDisplay[] newCards;
    [SerializeField] float xPackPosition = 0f;
    [SerializeField] float yPackPosition = 0f;
    int noOfBronzes;
    int noOfSilvers;
    int noOfGolds;
    [SerializeField] Card[] bronzeCards;
    [SerializeField]  Card[] silverCards;
    [SerializeField]  Card[] goldCards;

    private void Start()
    {
        newCards = FindObjectsOfType<CardDisplay>();
        FillCardRarityArrays();
    }

    private void FillCardRarityArrays()
    {
        for (int i = 0; i < cardDeck.Length; i++)
        {
            if (cardDeck[i].GetPlayerOverall() < 75)
            {
                noOfBronzes++;
            }
            else if (cardDeck[i].GetPlayerOverall() < 85)
            {
                noOfSilvers++;
            }
            else
            {
                noOfGolds++;
            }
        }

        bronzeCards = new Card[noOfBronzes];
        silverCards = new Card[noOfSilvers];
        goldCards = new Card[noOfGolds];

        int bronzeCount = 0;
        int silverCount = 0;
        int goldCount = 0;

        for (int i = 0; i < cardDeck.Length; i++)
        {
            if (cardDeck[i].GetPlayerOverall() < 75)
            {
                bronzeCards[bronzeCount] = cardDeck[i];
                bronzeCount++;
            }
            else if (cardDeck[i].GetPlayerOverall() < 85)
            {
                silverCards[silverCount] = cardDeck[i];
                silverCount++;
            }
            else
            {
                goldCards[goldCount] = cardDeck[i];
                goldCount++;
            }
        }

    }

    public void OpenBronzePack()
    {
        int randomizer;
        float deckDecider;
        Card[] deckUsed;
        Card packedCard;

        for (int i = 0; i < newCards.Length; i++)
        {
            deckDecider = Random.Range(0f, 1f);
            if(deckDecider < 0.75f)
            {
                deckUsed = bronzeCards;
            }
            else if(deckDecider < 0.99f)
            {
                deckUsed = silverCards;
            }
            else
            {
                deckUsed = goldCards;
            }
            randomizer = Random.Range(0, deckUsed.Length);
            packedCard = deckUsed[randomizer];
            newCards[i].card = packedCard;
            newCards[i].UpdateCardVisuals();
            newCards[i].transform.position = new Vector3(xPackPosition-300+(300*i), yPackPosition, 0f);
        }
    }

    public void OpenSilverPack()
    {
        int randomizer;
        float deckDecider;
        Card[] deckUsed;
        Card packedCard;

        for (int i = 0; i < newCards.Length; i++)
        {
            deckDecider = Random.Range(0f, 1f);
            if (deckDecider < 0.95f)
            {
                deckUsed = silverCards;
            }
            else
            {
                deckUsed = goldCards;
            }
            randomizer = Random.Range(0, deckUsed.Length);
            packedCard = deckUsed[randomizer];
            newCards[i].card = packedCard;
            newCards[i].UpdateCardVisuals();
            newCards[i].transform.position = new Vector3(xPackPosition - 300 + (300 * i), yPackPosition, 0f);
        }
    }

    public void OpenGoldPack()
    {
        int randomizer;
        Card[] deckUsed;
        Card packedCard;

        for (int i = 0; i < newCards.Length; i++)
        {
            deckUsed = goldCards;
            randomizer = Random.Range(0, deckUsed.Length);
            packedCard = deckUsed[randomizer];
            newCards[i].card = packedCard;
            newCards[i].UpdateCardVisuals();
            newCards[i].transform.position = new Vector3(xPackPosition - 300 + (300 * i), yPackPosition, 0f);
        }
    }
}
