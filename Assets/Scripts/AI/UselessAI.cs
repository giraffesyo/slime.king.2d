using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessAI : AI
{

    protected override bool Move()
    {
        base.Move(new Vector2(0, 0));
        return true;
    }

}
