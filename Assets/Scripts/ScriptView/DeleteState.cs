using UnityEngine;
using System.Collections;

public class DeleteState : MonoBehaviour
{

    public void handleClick()
    {
        GameObject state = this.gameObject.transform.parent.gameObject;
        Destroy(state);
    }
   
}
