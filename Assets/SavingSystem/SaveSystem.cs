
using Pathfinding;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.SceneTemplate;
using UnityEngine;

public static class SaveSystem 
{

   public static string[] paths = new string[10];

    public static void initializePaths()
    {
        paths[0] = "/saveFile1.txt";

        paths[1] = "/saveFile2.txt"; 
    }

   

    public static void save(int saveID)
    {
        Entity[] entities = Object.FindObjectsOfType<Entity>();

        float[] health = new float[entities.Length];

        Vector3[] positions = new Vector3[entities.Length];

        foreach (Entity entity in entities)
        {
            if (entity.GetType() != typeof(link))
            {
                health[entity.ID] = entity.health;

                positions[entity.ID] = entity.transform.position;
            }
        }

        EntityStats stats = new EntityStats
        {
            health = health,
            positions = positions,
            amount = InventoryManager.instance.amount,
            currency = Player.instance.currency,
            skillPoints = Player.instance.currentSkillPoints
        };

        string json = JsonUtility.ToJson(stats);

        File.WriteAllText(Application.persistentDataPath + paths[saveID], json);
    }

    public static void load(int saveID)
    {
        if(File.Exists(Application.persistentDataPath + paths[saveID]))
        {
            Entity[] entities = Object.FindObjectsOfType<Entity>();

            string saveString = File.ReadAllText(Application.persistentDataPath + paths[saveID]);

            EntityStats stats = JsonUtility.FromJson<EntityStats>(saveString);

            foreach (Entity entity in entities)
            {
                if (stats.health[entity.ID] != 0 || stats.positions[entity.ID] != Vector3.zero)
                {
                    if (entity.GetType() != typeof(Player))
                        entity.health = stats.health[entity.ID];
                    else Player.instance.linq.health = stats.health[entity.ID];

                    entity.transform.position = stats.positions[entity.ID];
                }
            }

        //    InventoryManager.instance.setInventoryItemsOnLoad(stats.amount);
            Player.instance.currency = stats.currency;
            Player.instance.currentSkillPoints = stats.skillPoints;
        }
        else
        {
            Debug.Log("No save file has been found in " + paths[saveID] + "!");
        }


    }

    private class EntityStats
    {
      public float[] health;

     public Vector3[] positions;

        public int[] amount;

        public int currency;

        public int skillPoints;
    }

}
