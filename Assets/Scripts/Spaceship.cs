using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private int spaceshipNo;
    public int totalCoin = 0;
    private List<int> shiplog = new List<int>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetCoin()
    {
        return totalCoin;
    }

    public void SetCoin(int coin)
    {
        totalCoin = coin;
    }

    public void AddCoin()
    {
        totalCoin++;
    }

    public void RemoveCoin()
    {
        totalCoin--;
    }

    public void AddShipLog()
    {
        shiplog.Add(totalCoin);
    }

    public List<int> GetShipLog()
    {
        return shiplog;
    }
}
