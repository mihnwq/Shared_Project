using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


public class Astar
{

    public Enemy enemy;

    

    public Astar(Enemy enemy)
    {
        this.enemy = enemy;
    }

    private List<Node> openList = new List<Node>();

    private List<Vector3> nodePositions = new List<Vector3>();
    private List<Node> allNodes = new List<Node>();

    private Vector3 start, goal;
    private float radius;

    public bool goalReached = false;

    public Node currentNode;

    public Node startNode;

    public Node goalNode;

    Dictionary<Vector3, Node> myPositions = new Dictionary<Vector3, Node>();

    public void prepareMap(Vector3 startTransform, Vector3 goalTransform, float searchRadius)
    {
        start = startTransform;
        goal = goalTransform;
        radius = searchRadius;

        openList.Clear();
     //   enemy.shortestPath.Clear();

        getMapNodeCosts();

        goalReached = false;

        currentNode = new Node(startTransform);
         currentNode.position = startTransform;
         openList.Add(currentNode);

        steps = 500;

        
    }

    public void executeSearch()
    {
        getMapNodeCosts();
        search();
    }

    void getMapNodeCosts()
    {
         Collider[] colliders = Physics.OverlapSphere(start, radius);

         foreach (Collider collider in colliders)
         {
             Node node = new Node(collider.transform.position);

             getCosts(node);

        
            nodePositions.Add(node.position);
            allNodes.Add(node);
            
         }

 //       Collider[] colliders = Physics.OverlapSphere(goal, radius);

        foreach (Collider collider in colliders)
        {
            // Node node = new Node(collider.transform.position);

            Node node = new Node(collider.transform.position);

      

            getCosts(node);

            nodePositions.Add(node.position);
            allNodes.Add(node);
        }

    }

    private void getCosts(Node node)
    {

        int gCost = Mathf.RoundToInt(Mathf.Abs(node.position.x - start.x) + Mathf.Abs(node.position.y - start.y) + Mathf.Abs(node.position.z - start.z));

       
        int hCost = Mathf.RoundToInt(Mathf.Abs(node.position.x - goal.x) + Mathf.Abs(node.position.y - goal.y) + Mathf.Abs(node.position.z - goal.z));

        int fCost = gCost + hCost;

        node.SetCosts(gCost, hCost, fCost);

        
     /*   Collider collider = Physics.OverlapSphere(node.position, 0.1f)[0];
        if (collider != null && collider.gameObject.CompareTag("Obstacle"))
        {
            node.solid = true;
        }*/
    }

    public int steps = 500;

    public void search()
    {
        if (!goalReached && steps != 0)
        {
            openList.Remove(currentNode);

            currentNode.setChecked();

            openNode(currentNode.position + Vector3.forward);
            openNode(currentNode.position + Vector3.back);
            openNode(currentNode.position + Vector3.left);
            openNode(currentNode.position + Vector3.right);


            openNode(currentNode.position + new Vector3(-1, 0, 1));
            openNode(currentNode.position + new Vector3(1, 0, -1));
            openNode(currentNode.position + new Vector3(1, 0, 1));
            openNode(currentNode.position + new Vector3(-1, 0, -1));

            int bestNodeIndex = 0;
            float bestNodeCost = 999;

            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].fCost < bestNodeCost)
                {
                    bestNodeCost = openList[i].fCost;
                    bestNodeIndex = i;
                }
                else if (openList[i].fCost == bestNodeCost)
                {
                    if (openList[i].gCost < openList[bestNodeIndex].gCost)
                    {
                        bestNodeIndex = i;
                    }
                }
            }

            Debug.Log(bestNodeIndex);


            if (currentNode == goalNode)
            {
                goalReached = true;
            }

        
            
            currentNode = openList[bestNodeIndex];

            
            

            steps--;
        }
    }


    private void openNode(Vector3 position)
    {
      
        Node node = getNodeAt(position);

        if (node != null && !node.solid && !node.traversed && !node.open)
        {
            node.setOpen();
            openList.Add(node);
            node.parent = currentNode;
        }
    }

    public Node getNodeAt(Vector3 position)
    {
        if(nodePositions.Contains(position))
        {
            return allNodes[(nodePositions.IndexOf(position))];
        }

        return null;
    }

    public void getPath()
    {
        Node current;
        
        current = goalNode.parent;

        while (current != startNode)
        {
        //    enemy.shortestPath.Add(current.parent);

            current = current.parent;
        }
    }
    
}
