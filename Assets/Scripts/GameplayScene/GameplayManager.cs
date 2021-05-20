using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public int currentState;

    public List<GameObject> background = new List<GameObject>();
    public List<GameObject> events = new List<GameObject>();
    public List<Text> P1_coin = new List<Text>();
    public List<Text> P2_coin = new List<Text>();
    public Text P1_TotalCoin;
    public Text P2_TotalCoin;
    public GameObject nextStageButton;
    public GameObject continueButton;
    public GameObject startButton;

    public Animator animatorBG;
    public Animator animatorSpaceship;

    private Player player1;
    private Player player2;

    private bool startCheck = false;
    private bool nextStageCheck = false;

    private bool enableButton = false; // false for demo //

    public SceneControl sceneContorl;
    void Start()
    {
        currentState = 1;

        player1 = PlayerManager.Instance.player1;
        player2 = PlayerManager.Instance.player2;

        P1_TotalCoin.text = "x " + player1.totalCoin.ToString();
        P2_TotalCoin.text = "x " + player2.totalCoin.ToString();

        for (int i = 0; i <= 2; i++)
        {
            P1_coin[i].text = "x " + player1.spaceships[i].GetCoin().ToString();
            P2_coin[i].text = "x " + player2.spaceships[i].GetCoin().ToString();
        }

        startButton.SetActive(true);
        nextStageButton.SetActive(false);
        continueButton.SetActive(false);

        nextStageCheck = true;
    }

 
    public void clickNextStage()
    {
        nextStageCheck = true;
        startCheck = false;

        nextStageButton.SetActive(false);
        if (currentState < 3)
        {
            currentState++;  
        }
        else
        {
            Debug.Log("Player reached Earth");
            return;
        }

        //// set background 
        //background[currentState - 1].SetActive(true);

        animatorBG.SetTrigger("ChangeBG");
        animatorSpaceship.SetTrigger("Fly");

        // reset event
        foreach (GameObject item in events)
        {
            item.SetActive(false);
        }

        // wait for change scene and enable start button
        StartCoroutine(WaitforChangeStage());
    }
    public void clickStart()
    {
        Debug.Log("Playing stage : " + currentState.ToString());

        nextStageCheck = false;
        startCheck = true;

        startButton.SetActive(false);

        StartGameplay();
    }

    public void StartGameplay()
    {
        // record shiplog
        for (int i = 0; i <= 2; i++)
        {
            PlayerManager.Instance.player1.spaceships[i].AddShipLog();
            PlayerManager.Instance.player2.spaceships[i].AddShipLog();
        }

        // random event
        var randomEvent = Random.Range(0, events.Count);
        events[randomEvent].SetActive(true);
        

        Debug.Log("Player get event " + events[randomEvent].GetComponent<EventClass>().name);

        // calculate coin
        //CalculateCoin(randomEvent);

        // update coin first time
        UpdateCoinText(1, 0);

        StartCoroutine(ClickNextFlow(randomEvent));

        // remove event from list
        events.RemoveAt(randomEvent);

        // false for demo //
        //if (currentState == 3)
        //    enableButton = false;
    }

    void CalculateCoin(int randomEvent)
    {
        var P1totalCoin = player1.totalCoin;
        var P2totalCoin = player2.totalCoin;

        for (int i=0;i<=2;i++)
        {
            P1totalCoin += events[randomEvent].GetComponent<EventClass>().CalculateCoin(i, player1.spaceships[i].GetCoin());
            P2totalCoin += events[randomEvent].GetComponent<EventClass>().CalculateCoin(i, player2.spaceships[i].GetCoin());
        }
        P1_TotalCoin.text = "x " + P1totalCoin.ToString();
        P2_TotalCoin.text = "x " + P2totalCoin.ToString();

        player1.totalCoin = P1totalCoin;
        player2.totalCoin = P2totalCoin;
    }

    public void P1addCointToSpaceship(int spaceshipNo)
    {
        if(enableButton)
        {
            player1.AddCoinToSpaceship(spaceshipNo);
            UpdateCoinText(1,spaceshipNo);
        }
    }

    public void P1removeCoinFromSpaceship(int spaceshipNo)
    {
        if (enableButton)
        {
            player1.RemoveCoinFromSpaceship(spaceshipNo);
            UpdateCoinText(1, spaceshipNo);
        }
    }

    public void P2addCointToSpaceship(int spaceshipNo)
    {
        if (enableButton)
        {
            player2.AddCoinToSpaceship(spaceshipNo);
            UpdateCoinText(2, spaceshipNo);
        }
    }

    public void P2removeCoinFromSpaceship(int spaceshipNo)
    {
        if (enableButton)
        {
            player2.RemoveCoinFromSpaceship(spaceshipNo);
            UpdateCoinText(2, spaceshipNo);
        }
    }

    void UpdateCoinText(int player,int spaceshipNo)
    {
        if(player==1)
        {
            P1_TotalCoin.text = "x " + player1.totalCoin.ToString();
            P1_coin[spaceshipNo].text = "x " + player1.spaceships[spaceshipNo].GetCoin().ToString();
        }
        else if (player==2)
        {
            P2_TotalCoin.text = "x " + player2.totalCoin.ToString();
            P2_coin[spaceshipNo].text = "x " + player2.spaceships[spaceshipNo].GetCoin().ToString();
        }


        //if (currentState == 3)
        //{
        //    nextStageButton.SetActive(false);
        //    if(startCheck)
        //        continueButton.SetActive(true);
        //}
        //else
        //{
        //    continueButton.SetActive(false);
        //    //if (player1.totalCoin == 0 && player2.totalCoin == 0)
        //    //{
        //        if (!nextStageCheck)
        //            nextStageButton.SetActive(true);
        //        else
        //            startButton.SetActive(true);
        //    //}
        //    //else
        //    //{
        //    //    if (!nextStageCheck)
        //    //        nextStageButton.SetActive(false);
        //    //    else
        //    //        startButton.SetActive(false);
        //    //}
        //}
    }

    public void ClickFinish()
    {
        StartCoroutine(ToResultScene());
    }

    IEnumerator ClickNextFlow(int randomEvent)
    {
        AnimationClip[] clips = events[randomEvent].GetComponent<Animator>().runtimeAnimatorController.animationClips;
        yield return new WaitForSeconds(clips[0].length);
        if (currentState == 3)
            ClickFinish();
        else
            clickNextStage();
    }

    IEnumerator ToResultScene()
    {
        AnimationClip[] clips = animatorSpaceship.runtimeAnimatorController.animationClips;
        animatorSpaceship.SetTrigger("Fly");
        yield return new WaitForSeconds(clips[0].length);
        sceneContorl.ChangeScene("Result");
    }

    IEnumerator WaitforChangeStage()
    {
        AnimationClip[] clips = animatorBG.runtimeAnimatorController.animationClips;
        yield return new WaitForSeconds(clips[1].length-1f);
        startButton.SetActive(true);
    }
}
