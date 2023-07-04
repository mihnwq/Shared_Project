using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Stamina : MonoBehaviour
{

    public StaminaBar bar;

    public Player player;

    public void Start()
    {
        bar.setMaxStamina(player.stamina);
    }

    public void Update()
    {
        bar.setStamina(player.stamina);
    }



}

