using UnityEditor.PackageManager;
using UnityEngine;

//I am going to get hate for every static variable i am doing
//but i don't regret a second of it since every variable here it's being changed only once
public class ChainVars
{

    public static bool wantToPlayTutorial = false;

    //Check on skill tree
    public static bool onSkillTree= false;

    //public bool to handle the slope exit
    public static bool playerExitSlope = false;

    //these handle the entering on a slope and the direction the slope is
    public static Vector3 playerSlopeMovementDir;

    public static Vector3 playerSlopeForwardDir;

    public static bool playerOnSlope;

    public static Vector3 slideDirection;


    //bools used to chain the game states to entity controls
    public static bool onTitle = true;

    public static int saveID = 1;

    public static bool isPaused = false;

     public static bool onInventory = false;

    public static bool onTrade = false;

    //Player states
    public static bool playerSliding = false;

    public static bool playerIsLocked = false;

    public static bool isKnocking = false;

     //functions to know and update if the entity is facing a slope and if so updating the direction
     public static void UpdateDir(Vector3 sl)
    {
        playerSlopeMovementDir = sl;
    }

    public static void UpdateOnSlope(bool on)
    {
        playerOnSlope = on;
    }
    
}
