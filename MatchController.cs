using UnityEngine;
using TMPro;

public class MatchController : MonoBehaviour
{
    //Configuration parameters
    int fullTime = 0;
    int halfTime = 45;
    [SerializeField] float highlightProbability = 0.1f;

    //Cached references
    [SerializeField] TextMeshProUGUI dialogueText = null;
    [SerializeField] TextMeshProUGUI scorelineText = null;
    [SerializeField] TextMeshProUGUI homeScorersText = null;
    [SerializeField] TextMeshProUGUI awayScorersText = null;
    SceneLoader sceneLoader;
    GameStatus gameStatus;

    // State Variables
    int minuteOfMatch = 0;
    bool paused = true;
    bool finished = false;
    bool highlightEnded = false;
    int homeScore = 0;
    int awayScore = 0;
    string homeScorers = "";
    string awayScorers = "";
    Card[] homeSquad = null;
    [SerializeField] Card[] awaySquad = null; //Serialized for now - Eventually will be programmatic
    Card[,] teams = new Card[2, 5];
    string holdText = "";
    float teamDifference;

    //Start update performs on start
    public void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameStatus = FindObjectOfType<GameStatus>();
        minuteOfMatch = 0;
        fullTime = Random.Range(90, 95);
        homeSquad = gameStatus.currentSquad;

        int homeOverall = 0;
        int awayOverall = 0;

        for (int i = 0; i < 5; i++)
        {
            teams[0, i] = homeSquad[i];
            teams[1, i] = awaySquad[i];
        }

        for (int i = 0; i < 5; i++)
        {
            homeOverall += teams[0, i].GetPlayerOverall();
            awayOverall += teams[1, i].GetPlayerOverall();
        }
        teamDifference = homeOverall - awayOverall;

        UpdateScoreText();
        UpdateGoalscorerText();
        ShowLineup();

    }

    //Update function is invoked once a frame
    private void Update()
    {
        CheckIfContinued();

        if (!paused && highlightEnded && !finished)
        {
            dialogueText.text = minuteOfMatch.ToString() + ": " + holdText;
            paused = true;
            holdText = "";
            highlightEnded = false;
            UpdateScoreText();
            UpdateGoalscorerText();
        }

        if (!paused && !finished)
        {
            minuteOfMatch++;
            dialogueText.text = minuteOfMatch.ToString();
            if (CheckForHighlight())
            {
                GenerateHighlight();
            }
            else if (CheckForHalfTime())
            {
                HalfTime();
            }
            else if (CheckIfFullTime())
            {
                FullTime();
            }
        }

        if (!paused && finished)
        {
            sceneLoader.LoadMenuScene();
        }
    }

    private void FullTime()
    {
        paused = true;
        finished = true;
        dialogueText.text = minuteOfMatch.ToString() + "': FULL TIME.";
    }

    private void HalfTime()
    {
        paused = true;
        dialogueText.text = "HALF TIME.";
    }

    private bool CheckForHalfTime()
    {
        if (minuteOfMatch == halfTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GenerateHighlight()
    {
        paused = true;

        int teamIndex = GenerateTeamIndex();
        int playerIndex = GeneratePlayerIndex();

        float chanceModifier = GenerateChanceModifier(teamIndex, playerIndex);
        float teamModifier = GenerateTeamModifier();
        float playerModifier = GeneratePlayerModifier(teamIndex,playerIndex);
        float goalkeeperModifier = GenerateKeeperModifier(teamIndex, playerIndex);
        float goalProbability = teamModifier * playerModifier * chanceModifier * goalkeeperModifier;

        bool goalScored = CalculateGoal(goalProbability);

        if(goalScored && teamIndex == 0)
        {
            homeScore++;
            homeScorers = homeScorers + "\n" + teams[teamIndex, playerIndex].GetPlayerName() + " " + minuteOfMatch.ToString() + "'";
        }
        if (goalScored && teamIndex == 1)
        {
            awayScore++;
            awayScorers = awayScorers + "\n" + teams[teamIndex, playerIndex].GetPlayerName() + " " + minuteOfMatch.ToString() + "'";
        }

        Debug.Log("Goalkeeper Modifier: " + goalkeeperModifier);
        Debug.Log(goalProbability);

        GetHoldText(goalScored, teamIndex, playerIndex);
        highlightEnded = true;
    }

    private float GenerateKeeperModifier(int teamIndex, int playerIndex)
    {
        if (teamIndex == 0)
        {
            return 1.7f - teams[1, 0].GetPlayerDefense()/100f;
        }
        else
        {
            return 1.7f - teams[0, 0].GetPlayerDefense()/100f ;
        }
    }

    private void GetHoldText(bool goalScored, int teamIndex, int playerIndex)
    {
        if(goalScored)
        {
            holdText = "GOOAAAAALLLLLL! " + teams[teamIndex, playerIndex].GetPlayerName() + " scores!"; 
        }
        else
        {
            holdText =  "The ball goes over the bar!";
        }
    }

    private float GeneratePlayerModifier(int teamIndex, int playerIndex)
    {
        return teams[teamIndex, playerIndex].GetPlayerAttack() / 100f;
    }

    private float GenerateTeamModifier()
    {
        return 1f;
    }

    private float GenerateChanceModifier(int teamIndex, int playerIndex)
    {
        float greatChanceProbability = 0.2f;
        float goodChanceProbability = 0.5f;
        float averageChanceProbability = 0.8f;

        float greatChanceModifier = 1f;
        float goodChanceModifier = 0.6f;
        float averageChanceModifier = 0.3f;
        float poorChanceModifier = 0.1f;

        float chanceDecider = Random.Range(0f, 1f);
        if(chanceDecider < greatChanceProbability)
        {
            PrintHighlight(0, teamIndex, playerIndex);
            return greatChanceModifier;
        }
        else if(chanceDecider < goodChanceProbability)
        {
            PrintHighlight(1, teamIndex, playerIndex);
            return goodChanceModifier;
        }
        else if(chanceDecider < averageChanceProbability)
        {
            PrintHighlight(2, teamIndex, playerIndex);
            return averageChanceModifier;
        }
        else
        {
            PrintHighlight(3, teamIndex, playerIndex);
            return poorChanceModifier;
        }
    }

    private void PrintHighlight(int chanceQuality, int teamIndex, int playerIndex)
    {
        dialogueText.text = minuteOfMatch.ToString() + ": ";

        if(chanceQuality == 0)
        {
            dialogueText.text = dialogueText.text + "PENALTY! It's going to be taken by " + teams[teamIndex, playerIndex].GetPlayerName();
        }
        else if(chanceQuality == 1)
        {
            dialogueText.text = dialogueText.text + teams[teamIndex, playerIndex].GetPlayerName() + " plays a quick one-two with " + teams[teamIndex, playerIndex-1].GetPlayerName() + " and looks to run in on goal.";
        }
        else if(chanceQuality == 2)
        {
            dialogueText.text = dialogueText.text + "Corner taken by " + teams[teamIndex, playerIndex-1].GetPlayerName();
            
        }
        else if(chanceQuality == 3)
        {
            dialogueText.text = dialogueText.text + teams[teamIndex, playerIndex].GetPlayerName() + " has a shot from distance. ";
        }
    }

    private bool CalculateGoal(float goalProbability)
    {
        if (Random.Range(0f, 1f) < goalProbability)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int GeneratePlayerIndex()
    {
        float playerDecider = Random.Range(0f, 1f);
        if(playerDecider < 0.5)
        {
            return 4;
        }
        else if(playerDecider < 0.8)
        {
            return 3;
        }
        else if(playerDecider < 0.9)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    private int GenerateTeamIndex()
    {
        float probabilityDifference = 3*teamDifference / 1000f;
        float skew = 0.5f + probabilityDifference;
        if(Random.Range(0f,1f) < skew)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    private bool CheckIfFullTime()
    {
        if (minuteOfMatch > fullTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckForHighlight()
    {
        if(Random.Range(0.0f, 1.0f) < highlightProbability)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckIfContinued()
    {
        if (Input.anyKeyDown)
        {
            paused = false;
        }
    }

    private void UpdateScoreText()
    {
        scorelineText.text = homeScore.ToString() + "-" + awayScore.ToString();
    }

    private void UpdateGoalscorerText()
    {
        homeScorersText.text = homeScorers;
        awayScorersText.text = awayScorers;
    }

    private void ShowLineup()
    {
        dialogueText.text = "TEAMNAME: ";
        for (int i = 0; i < 4; i++)
        {
            dialogueText.text = dialogueText.text + homeSquad[i].GetPlayerName() + ", ";
        }
        dialogueText.text = dialogueText.text + homeSquad[4].GetPlayerName() + "\n\n";
        dialogueText.text = dialogueText.text + "LIVERPOOL: ";
        for (int i = 0; i < 4; i++)
        {
            dialogueText.text = dialogueText.text + awaySquad[i].GetPlayerName() + ", ";
        }
        dialogueText.text = dialogueText.text + awaySquad[4].GetPlayerName();
    }
}
