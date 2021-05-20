using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public List<SpaceshipButton> spaceshipButtons = new List<SpaceshipButton>();
    public Text totalCoinText;
    public GameObject continueButton;
    public GameObject changePlayerButton;
    public Text playerText;
    public SceneControl sceneControl;

    private bool selectP1;
    private bool P2checked = false;

    private Player player1;
    private Player player2;

    void Start()
    {
        Screen.SetResolution(1080, 1920, true);

        player1 = PlayerManager.Instance.player1;
        player2 = PlayerManager.Instance.player2;

        selectP1 = true;

        continueButton.SetActive(false);

        player1.ResetCoin();
        player2.ResetCoin();

        totalCoinText.text = "x " + player1.totalCoin.ToString();

        continueButton.SetActive(true);
        changePlayerButton.SetActive(false);
    }

    public void selectSpacship(int spaceshipNo)
    {
        if (spaceshipNo == 0)
        {
            spaceshipButtons[0].Active();
            spaceshipButtons[1].Inactive();
            spaceshipButtons[2].Inactive();
        }
        else if (spaceshipNo == 1)
        {
            spaceshipButtons[0].Inactive();
            spaceshipButtons[1].Active();
            spaceshipButtons[2].Inactive();
        }
        else if (spaceshipNo == 2)
        {
            spaceshipButtons[0].Inactive();
            spaceshipButtons[1].Inactive();
            spaceshipButtons[2].Active();
        }
    }

    public void addCointToSpaceship(int spaceshipNo)
    {
        if (selectP1)
            player1.AddCoinToSpaceship(spaceshipNo);
        else
            player2.AddCoinToSpaceship(spaceshipNo);

        UpdateCoinText(spaceshipNo);
    }

    public void removeCoinFromSpaceship(int spaceshipNo)
    {
        if (selectP1)
            player1.RemoveCoinFromSpaceship(spaceshipNo);
        else
            player2.RemoveCoinFromSpaceship(spaceshipNo);

        UpdateCoinText(spaceshipNo);
    }

    void UpdateCoinText(int spaceshipNo)
    {
        if (selectP1)
        {
            totalCoinText.text = "x " + player1.totalCoin.ToString();
            spaceshipButtons[spaceshipNo].coinText.text = "x " + player1.spaceships[spaceshipNo].GetCoin().ToString();
        }
        else
        {
            totalCoinText.text = "x " + player2.totalCoin.ToString();
            spaceshipButtons[spaceshipNo].coinText.text = "x " + player2.spaceships[spaceshipNo].GetCoin().ToString();
        }
        
        //if(PlayerManager.Instance.player1.totalCoin==0)
        //{
            continueButton.SetActive(true);
        //}
        //else
        //{
        //    continueButton.SetActive(false);
        //}
    }

    public void ContinueButton()
    {
        if(!P2checked)
        {
            P2checked = true;
            ChangePlayer();
            changePlayerButton.SetActive(true);
        }
        else
        {
            Debug.Log("Player 1 : " + player1.spaceships[0].GetCoin().ToString() + " / "
                + player1.spaceships[1].GetCoin().ToString() + " / "
                + player1.spaceships[2].GetCoin().ToString());
            Debug.Log("Player 2 : " + player2.spaceships[0].GetCoin().ToString() + " / "
                + player2.spaceships[1].GetCoin().ToString() + " / "
                + player2.spaceships[2].GetCoin().ToString());
            Debug.Log("Start Game");
            sceneControl.ChangeScene("Gameplay");
        }
    }

    public void ChangePlayer()
    {
        selectP1 = !selectP1;

        if(selectP1)
        {
            playerText.text = "Player 1";
        }
        else
        {
            playerText.text = "Player 2";
        }

        spaceshipButtons[0].Active();
        spaceshipButtons[1].Inactive();
        spaceshipButtons[2].Inactive();

        UpdateCoinText(0);
        UpdateCoinText(1);
        UpdateCoinText(2);
    }

}
