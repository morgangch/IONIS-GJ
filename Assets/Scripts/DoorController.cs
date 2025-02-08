using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Camera playerHead; // Caméra du joueur
    public float doorOpenAngle = 90.0f;
    public float doorCloseAngle = 0.0f;
    public float smooth = 2.0f; // Augmenté pour une transition plus fluide

    private bool isOpening = false; // État de la porte
    private GameObject currentDoor = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerHead.transform.position, playerHead.transform.forward, out hit, 4f))
            {
                if (hit.transform.CompareTag("Door"))
                {
                    currentDoor = hit.transform.parent.gameObject; // Récupérer l'objet parent (la vraie porte)
                    isOpening = !isOpening; // Inverser l'état de la porte
                }
            }
        }

        if (currentDoor != null)
        {
            // Définir l'angle cible
            float targetAngle = isOpening ? doorOpenAngle : doorCloseAngle;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // Appliquer la rotation progressivement
            currentDoor.transform.localRotation = Quaternion.Slerp(
                currentDoor.transform.localRotation,
                targetRotation,
                smooth * Time.deltaTime
            );
        }
    }
}
