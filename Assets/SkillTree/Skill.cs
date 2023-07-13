using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;

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
        atributeFuriousKick,
        increaseHomingRadius,
        increasePorjectileSpeed,
        increaseDashSpeed,
        nothing
    }

    public Type skillType;

    public Skill parent = null;

    public void onSkillClick()
    {
        if (parent == null || parent.unlocked)
        {

            switch (skillType)
            {
                case Type.giveEyeFrames:
                    SkillKeeper.gatheredEyeFrames = true;
                    break;
                case Type.increaseDashSpeed:
                    SkillKeeper.currentUpgradedDashSpeed *= increasingValue;
                    break;
                case Type.increasePorjectileSpeed:
                    SkillKeeper.currentUpgradedProjectileSpeed *= increasingValue;
                    break;
                case Type.atributeFuriousKick:
                    SkillKeeper.unlockedFuriousKick = true;
                    break;
                case Type.increaseHomingRadius:
                    SkillKeeper.currentUpgradedHomingRadius *= increasingValue;
                    break;
                    
            }
            unlocked = true;
        }
    }
}

