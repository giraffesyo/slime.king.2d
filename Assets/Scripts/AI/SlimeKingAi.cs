using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKingAi : AI
{
    public bool testing = false;       // Used for testing, delete later
    public bool testing2 = false;       // Used for testing, delete later

    SummonAbility summon;
    DebreeAttack debree;
    int stage = 1;

    protected override void Start()
    {
        base.Start();
        summon = GetComponent<SummonAbility>();
        debree = GetComponent<DebreeAttack>();
    }

    protected override bool Move()
    {
        if (testing)
        {
            debree.Use("Debree");
            testing = false;

        }
        if (testing2)
        {
            // Use animation event instead of calling Use
            summon.Use("Summon");
            testing2 = false;
            stage++;
            summon.setStage(stage);
        }

        return true;
    }
}
