using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Skill : MonoBehaviour
{
    public bool unlocked;

    public float increasingValue;

    //remeber to get here when finishing the main skill tree and do if(player has enough skill points)
    public int skillPointsNeeded;

    public Skill skillThatCancelsThisSkill;

    public enum Type
    {
        giveEyeFrames,
        chargedUp,
        solidFreeze,
        bulletPunch,


        furiousKick,
        innerStrength,
        resilience,
        destructiveRage,

        spellbind,
        unbreakableGreed,
        lordOfTrades,
        flawlessChemistry,

        nothing
    }

    public Type skillType;

    public Skill parent = null;

    public bool enable = false;

    public Sprite descImg;

    public void onSkillClick()
    {
        if (!unlocked)
        {
            if (parent == null || parent.unlocked)
            {


                checkSkill(true);

                unlocked = true;
                enable = true;
            }
        }
        else
        {
            checkSkill(!enable);

            enable = !enable;
        }


        if (enable)
            skillImg.sprite = enableSprite;
        else skillImg.sprite = unableSprite;

    }

    public Image skillImg;

    public Sprite enableSprite;
    public Sprite unableSprite;

    public void onClick()
    {
        SkillKeeper.instance.skillDesc.gameObject.SetActive(true);

        SkillKeeper.instance.skillDesc.sprite = descImg;
    }

    public void checkSkill(bool enable)
    {
        switch (skillType)
        {
            case Type.giveEyeFrames:
                SkillKeeper.un_enableSkill(SkillKeeper.gatheredEyeFrames,enable);
                break;
            case Type.chargedUp:
                SkillKeeper.un_enableSkill(SkillKeeper.gatheredChargedUp, enable);
                break;
            case Type.solidFreeze:
                SkillKeeper.un_enableSkill(SkillKeeper.gatheredSolidFreeze, enable);
                break;
            case Type.bulletPunch:
                SkillKeeper.un_enableSkill(SkillKeeper.gatheredBulletPunch, enable);
                break;
            case Type.furiousKick:
                SkillKeeper.un_enableSkill(SkillKeeper.unlockedFuriousKick, enable);
                break;
            case Type.innerStrength:
                SkillKeeper.un_enableSkill(SkillKeeper.unlockedInnerStrength, enable);
                break;
            case Type.resilience:
                SkillKeeper.un_enableSkill(SkillKeeper.unlockedReseilience, enable);
                break;
            case Type.destructiveRage:
                SkillKeeper.un_enableSkill(SkillKeeper.unlockedDestructiveRage, enable);
                break;
            case Type.spellbind:
                SkillKeeper.un_enableSkill(SkillKeeper.gatheredSpellbind, enable);
                break;
            case Type.unbreakableGreed:
                SkillKeeper.un_enableSkill(SkillKeeper.gatheredUnbreakableGreed, enable);
                break;
            case Type.lordOfTrades:
                SkillKeeper.un_enableSkill(SkillKeeper.gatheredLordOfTrades, enable);
                break;

            case Type.flawlessChemistry:
                SkillKeeper.un_enableSkill(SkillKeeper.gatheredFlawlessChemistry, enable);
                break;

        }
    }

}

