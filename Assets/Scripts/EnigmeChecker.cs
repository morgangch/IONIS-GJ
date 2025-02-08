using UnityEngine;

public class EnigmeChecker : MonoBehaviour
{
    public int current;
    public int needed;

    public void IncrementCurrent() {
        current++;
        Debug.Log("EnigmeChecker: current incremented to " + current);
    }

    public void DecrementCurrent() {
        current = Mathf.Max(0, current - 1);
        Debug.Log("EnigmeChecker: current decremented to " + current);
    }

    void Update()
    {
        if(current == needed)
        {
            Debug.Log("variableA is equal to variableB");
        }
        else
        {
            // Debug.Log("variableA is not equal to variableB");
        }
    }
}
