using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Merchant : BasicNpc, IInteractible
{

    public Transform shopCanvas;

    public MerchantShop shop;

    public List<Item> currentMerchantShop = new List<Item>();

    public Animator animator;

    public void interact()
    {
        
    }
    public TextMeshProUGUI currency;
    public string[] animations = new string[5];

    public void Start()
    {
        animations[0] = "idle";

        animations[1] = "showcasing";

        animations[2] = "thanking";

        animations[3] = "talking";
    }

    public override void Update()
    {
          base.Update();

          if(isCloseFromPlayer)
          {

            setOneTrueOneFalse(1, 0);

              if(Input.GetKeyDown(KeyCode.Q))
              {
                  ChainVars.onTrade = !ChainVars.onTrade;

                  shopCanvas.gameObject.SetActive(ChainVars.onTrade);

                  if(ChainVars.onTrade)
                  {
                      shop.loadShopItems(currentMerchantShop);
                  }
              }

            if (ChainVars.onTrade)
                currency.text = Player.instance.currency.ToString();
}
          else
        {
            setOneTrueOneFalse(0, 3);
        }

        
    }

    public void setOneTrueOneFalse(int index1, int index2)
    {
        animator.SetBool(animations[index1], true);

        animator.SetBool(animations[index2], false);
    }
}

