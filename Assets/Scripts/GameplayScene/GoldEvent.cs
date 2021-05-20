using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldEvent : EventClass
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override int CalculateCoin(int spaceshipNo, int coin)
    {
        if (spaceshipNo == 0)
        {
            coin = coin + (int)(coin * 0.3f);
            return coin;
        }
        else if (spaceshipNo == 0)
        {
            coin = coin + (int)(coin * 0.4f);
            return coin;
        }
        else
        {
            coin = coin + (int)(coin * 0.5f);
            return coin;
        }
    }
}
