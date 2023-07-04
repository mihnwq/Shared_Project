using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Merchant : BasicNpc, IInteractible
{

    public Transform shopCanvas;

    public MerchantShop shop;

    public List<Item> currentMerchantShop = new List<Item>();

    public void interact()
    {
        
    }

    public override void Update()
    {
        base.Update();

        if(isCloseFromPlayer)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                ChainVars.onTrade = !ChainVars.onTrade;

                shopCanvas.gameObject.SetActive(ChainVars.onTrade);

                if(ChainVars.onTrade)
                {
                    shop.loadShopItems(currentMerchantShop);
                }
            }
        }
    }


}

