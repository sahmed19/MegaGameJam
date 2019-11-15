using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsQueue : MonoBehaviour
{

    private Queue<Vector3> posQueue = new Queue<Vector3>();
    public float closeEnough = 3f;
    public Vector3 nextPos;

    // Update is called once per frame
    void Update()
    {
        //If Queue Contains anything
        if(posQueue.Count > 0)
        {
            //stores the values at front of queue
            Vector3 temp = posQueue.Peek();

            //If not close to point
            if (!CloseToPoint(temp))
            {
                //Continue forward
                //MoveTowards(temp);
            }

            //Else remove from queue
            else
            {
                posQueue.Dequeue();
            }
        }
    }

    
    //Adds Vector 3 to the Queue
    private void AddToQ(Vector3 position)
    {
        posQueue.Enqueue(position);
    }


    //Adds locations from array inot Queue
    public void AddPositionsToQ(Vector3[] posArray)
    {
        //Adds individual elements to Queue
        for(int rep = 0; rep <= posArray.Length; rep++)
        {
            AddToQ(posArray[rep]);
        }
    }

    //Checks whether vector param is within distance closeEnough
    private bool CloseToPoint(Vector3 reference)
    {
        //Finds difference vector
        Vector3 node = reference - transform.position;

        //Squares difference vector
        float squareMag = node.sqrMagnitude;

        //If within bounds created by closeEnough
        if (squareMag < closeEnough) return true;
        else return false;
    }
}
