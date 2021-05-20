using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEvent : EventClass
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
            coin = coin - (int)(coin * 0.1f);
            if (coin <= 0)
                return 0;
            else
                return coin;
        }
        else if (spaceshipNo == 0)
        {
            coin = coin - (int)(coin * 0.2f);
            if (coin <= 0)
                return 0;
            else
                return coin;
        }
        else
        {
            coin = coin - (int)(coin * 0.3f);
            if (coin <= 0)
                return 0;
            else
                return coin;
        }
    }
}
