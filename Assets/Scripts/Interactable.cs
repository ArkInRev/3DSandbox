
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float interactRadius=3.0f;
    public Transform interactionTransform;
    public bool isInRange;
    public string interactableName;
    public AudioClip pickupSound;
    public AudioSource interactableAudioSource;


    public virtual void Interact(PlayerController player)
    {   // this method is meant to be overridden
        //Debug.Log("Interacting with " + interactionTransform.name);

        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance > interactRadius)
        {
            Debug.Log("interaction of " + distance.ToString() + " is out of the interact range of " + interactRadius.ToString());
            isInRange = false;
            return;
        } else
        {
            isInRange = true;
            interactableAudioSource.Play();
        }


    }


    // Start is called before the first frame update
    void Start()
    {
        interactionTransform = this.transform;
        interactableAudioSource = this.GetComponent<AudioSource>();
        interactableAudioSource.clip = pickupSound;
    }


    private void OnDrawGizmosSelected()
    {

        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
