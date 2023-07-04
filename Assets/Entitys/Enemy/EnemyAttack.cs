using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class EnemyAttack : EntityAttack
{

    public Enemy currentEnemy;

    public void Start()
    {

    }

    public override void Update()
    {
        if (currentEnemy.nearPlayer)
            attack();



        base.Update();
    }



}

