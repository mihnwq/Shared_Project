using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class SkillKeeper : MonoBehaviour
{
    //Kinematic gloves
    public static bool gatheredEyeFrames;

    public static bool gatheredChargedUp;

    public static bool gatheredBulletPunch;

    public static bool gatheredSolidFreeze;

    //Character
    public static bool unlockedFuriousKick;

    public static bool unlockedInnerStrength;

    public static bool unlockedDestructiveRage;

    public static bool unlockedReseilience;

    //Affinity
    public static bool gatheredFlawlessChemistry;
    public static bool gatheredLordOfTrades;
    public static bool gatheredUnbreakableGreed;
    public static bool gatheredSpellbind;

    public Image skillDesc;

    public static SkillKeeper instance;

    public void Awake()
    {
        instance = this;

    }

    public TextMeshProUGUI amount;

    public void Update()
    {
        amount.text = Player.instance.currentSkillPoints.ToString();
    }

    public static void un_enableSkill(bool skill, bool enable)
    {
        skill = enable;

       
    }


}

