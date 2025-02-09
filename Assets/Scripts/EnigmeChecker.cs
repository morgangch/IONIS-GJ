using UnityEngine;

public class EnigmeChecker : MonoBehaviour
{
    public int current;
    public int needed;

    public void IncrementCurrent() {
        current++;
        if(current == needed)
        {
            Debug.Log("variableA is equal to variableB");
        }
    }

    public void DecrementCurrent() {
        current = Mathf.Max(0, current - 1);
    }
}
