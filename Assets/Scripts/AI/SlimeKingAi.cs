using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKingAi : AI
{
    public bool testing = false;       // Used for testing, delete later
    SummonAbility summon;
    int stage = 1;

    protected override void Start()
    {
        base.Start();
        summon = GetComponent<SummonAbility>();
    }

    protected override bool Move()
    {
        if (testing)
        {
            // Use animation event instead of calling Use
            summon.Use("Summon");
            testing = false;
            stage++;
            summon.setStage(stage);
        }


        return true;
    }
}
