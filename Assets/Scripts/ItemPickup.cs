using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Transform playerHand; // Assign this in the Inspector
    public GameObject playerHead; // Assign this in the Inspector
    private GameObject currentItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
            if (currentItem != null)
            {
                DropItem();
            }
            else
            {
                PickupItem();
            }
        }
    }

    void PickupItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerHead.transform.position, playerHead.transform.forward, out hit, 4f))
        {
            Debug.Log("Raycast hit: " + hit.transform.name);
            if (hit.transform.CompareTag("Pickup"))
            {
                currentItem = hit.transform.gameObject;
                currentItem.transform.SetParent(playerHand);
                currentItem.transform.localPosition = Vector3.zero;
                currentItem.transform.localRotation = Quaternion.identity;
                currentItem.GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log("Picked up: " + currentItem.name);
            }
        }
    }

    void DropItem()
    {
        Debug.Log("Dropped: " + currentItem.name);
        currentItem.transform.SetParent(null);
        currentItem.GetComponent<Rigidbody>().isKinematic = false;
        currentItem = null;
    }
}
