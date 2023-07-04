using System.Linq;
using UnityEngine;

public class BasicNpc : Entity
{

   public bool isCloseFromPlayer = false;

    public Player player;

    public override void Start()
    {
        health = 100;
    }

    
    public virtual void Update()
    {
        isCloseFromPlayer = getMagnitude();
    }

    public float minMagnitudeFromPlayer;

    public bool getMagnitude()
    {
        if((player.transform.position - transform.position).magnitude < minMagnitudeFromPlayer)
        {
            return true;
        }


        return false;
    }

}
