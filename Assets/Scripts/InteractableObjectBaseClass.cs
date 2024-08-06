using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectBaseClass : MonoBehaviour
{
    float interactDistance;
    public float interactTime;

    void start()
    {
        var circleCollider = new CircleCollider2D();
        circleCollider.radius = interactDistance;
    }

    public virtual bool Interact(GameObject objectToInteractWith)
    {
        Debug.Log("Nothing seems to happen");
        return false;
    }

    public virtual bool CheckInteraction(GameObject objectToInteractWith)
    {
        Debug.Log("Not implemented - CheckInteraction");
        return false;
    }

}
