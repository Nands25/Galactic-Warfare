using UnityEngine;

public class BossController : EnemyBase
{
    public EnemyData bossData;
    private int maxHp;
    private int phase = 1;

    protected override void Awake()
    {
        base.Awake();
        if (bossData != null) { data = bossData; currentHp = data.maxHp; maxHp = data.maxHp; }
    }

    private void Update()
    {
        float hpRatio = (float)currentHp / maxHp;
        if (phase == 1 && hpRatio <= 0.66f) EnterPhase2();
        if (phase == 2 && hpRatio <= 0.33f) EnterPhase3();
        // implement firing patterns by phase
    }

    void EnterPhase2()
    {
        phase = 2;
        // change animator state, fire missiles, etc.
        Debug.Log("Boss phase 2");
    }

    void EnterPhase3()
    {
        phase = 3;
        // unleash beam
        Debug.Log("Boss phase 3");
    }
}