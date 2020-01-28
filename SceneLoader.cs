using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMatchScene()
    {
        SceneManager.LoadScene("Match");
    }

    public void LoadSquadScene()
    {
        SceneManager.LoadScene("Squad");
    }

    public void LoadStoreScene()
    {
        SceneManager.LoadScene("Store");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
