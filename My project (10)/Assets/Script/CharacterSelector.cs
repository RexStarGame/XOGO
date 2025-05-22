using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public List<string> availableHeroes = new List<string> { "Warrior", "Mage", "Rogue" };
    public GameObject menuPanel;
    public GameObject gameManager;

    private string playerChoice;

    public void SelectHero(string heroName)
    {
        if (!availableHeroes.Contains(heroName)) return;

        playerChoice = heroName;
        availableHeroes.Remove(heroName);

        // Assign rest to AI
        List<string> aiChoices = new List<string>(availableHeroes);

        gameManager.GetComponent<GameManager>().SetupPlayers(playerChoice, aiChoices);

        // Hide selection menu
        menuPanel.SetActive(false);
    }
}
