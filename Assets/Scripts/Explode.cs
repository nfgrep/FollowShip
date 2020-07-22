using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    //Destroys game object when animation event calls this function
    void EndExplosion()
    {
        Destroy(gameObject);
    }

}
