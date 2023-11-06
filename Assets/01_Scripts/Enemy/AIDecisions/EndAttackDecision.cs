using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAttackDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return !_actionData.IsAttack; //공격중일땐 false
    }
}
