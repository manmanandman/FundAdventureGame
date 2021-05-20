using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int startCoin;
    public int totalCoin;
    public GameObject spaceshipPrefab;
    public List<Spaceship> spaceships = new List<Spaceship>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<=2; i++)
        {
            GameObject spaceshipObject = Instantiate(spaceshipPrefab, this.transform);
            spaceships.Add(spaceshipObject.GetComponent<Spaceship>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetCoin()
    {
        totalCoin = startCoin;
        if(spaceships.Count>0)
        {
            for (int i = 0; i <= 2; i++)
            {
                spaceships[i].GetComponent<Spaceship>().SetCoin(0);

            }
        }
    }

    public void AddCoinToSpaceship(int spaceshipNo)
    {
        if(totalCoin > 0)
        {
            totalCoin--;
            spaceships[spaceshipNo].AddCoin();
        }
    }

    public void RemoveCoinFromSpaceship(int spaceshipNo)
    {
        if(spaceships[spaceshipNo].GetCoin()>0)
        {
            totalCoin++;
            spaceships[spaceshipNo].RemoveCoin();
        }
    }
}
