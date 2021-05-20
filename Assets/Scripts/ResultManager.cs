using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultManager : MonoBehaviour
{
    public List<Text> P1_S1_Text;
    public List<Text> P1_S2_Text;
    public List<Text> P1_S3_Text;
    public List<Text> P2_S1_Text;
    public List<Text> P2_S2_Text;
    public List<Text> P2_S3_Text;

    public Text P1_TotalCoin;
    public Text P2_TotalCoin;

    public Text P1_TotalScore;
    public Text P2_TotalScore;

    private Player player1;
    private Player player2;

    private List<List<Text>> P1Text = new List<List<Text>>();
    private List<List<Text>> P2Text = new List<List<Text>>();

    void Start()
    {
        player1 = PlayerManager.Instance.player1;
        player2 = PlayerManager.Instance.player2;
        P1Text.Add(P1_S1_Text);
        P1Text.Add(P1_S2_Text);
        P1Text.Add(P1_S3_Text);
        P2Text.Add(P2_S1_Text);
        P2Text.Add(P2_S2_Text);
        P2Text.Add(P2_S3_Text);
        GetResult();
    }



    void GetResult()
    {
        
        // spaceship loop
        for (int i = 0; i <= 2; i++)
        {
            List<int> P1Log = player1.spaceships[i].GetShipLog();
            List<int> P2Log = player2.spaceships[i].GetShipLog();

            // stage loop
            for (int j = 0; j <= 2; j++)
            {
                P1Text[i][j].text = "x " + P1Log[j].ToString();
                P2Text[i][j].text = "x " + P2Log[j].ToString();
            }
        }

        P1_TotalCoin.text = player1.totalCoin.ToString();
        P2_TotalCoin.text = player2.totalCoin.ToString();

    }
}
