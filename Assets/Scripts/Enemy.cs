using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    private Vector2 targetPos;
    public float notRedVal = 255;
    public float rayDist = 1;
    private float randYB;
    private float randYT;
    private float randX;
    public AnimationCurve animCurve;
    private SpriteRenderer sr;
    private Color spriteColor;
    public GameObject playerTrans;


    void Start()
    {
        //Sets random x values for instantiation
        randYT = Random.Range(3f, 4f);
        randYB = Random.Range(-4f, -3f);
        randX = Random.Range(-7f, 7f);
        //Gets sprite renderer 
        sr = GetComponent<SpriteRenderer>();
        //Gets animation


        //Sets the target to lerp to based on where it is instantiated.
        if (transform.position.y < 0)
        {
            targetPos = new Vector2(randX, randYB);
        }
        else if (transform.position.y > 0)
        {
            targetPos = new Vector2(randX, randYT);
        }

    }

    void Update()
    {
        //Lerps to targetPosition
        transform.position = Vector2.Lerp(transform.position, targetPos, animCurve.Evaluate(Time.deltaTime));
        //Lerps colour from white to red
        notRedVal = Mathf.Lerp(notRedVal, 0f, animCurve.Evaluate(Time.deltaTime));
        spriteColor = new Color(255f, notRedVal, notRedVal);
        sr.color = spriteColor;

    }

}
