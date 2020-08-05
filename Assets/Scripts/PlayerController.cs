using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Speed of the movement
    [SerializeField] private float moveSpeed = 5f;
    //Dummy game object which the player will move toward
    [SerializeField] private Transform movePoint = null;
    //How much the player will move
    [SerializeField] private float offsetMovement = 1.2f;
    [SerializeField] private float secondsToNextMove = 0.5f;
    private Rigidbody2D rb2d = null;
    private Vector3 lastMovePosition;
    private bool waitCouroutineIsRunning = false;

    // Start is called before the first frame update

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();    
    }

    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move the player towards the move point
        rb2d.position = Vector3.MoveTowards(rb2d.position, movePoint.position, moveSpeed * Time.deltaTime);

        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

    
        //move the move point
        if(Vector3.Distance(rb2d.position, movePoint.position) <= Mathf.Epsilon  && waitCouroutineIsRunning == false)
        {
            
            //move horizontally
            if(Mathf.Abs(xInput) == 1f)
            {
                if(ActionCaster.instance.isCasting)
                    ActionCaster.instance.CancelCasting();
                
                lastMovePosition = movePoint.position;
                movePoint.position += new Vector3(xInput * offsetMovement, 0, 0);
                StartCoroutine("WaitToNextMove");
            }
            else if(Mathf.Abs(yInput) == 1f) //move vertically
            {
                if(ActionCaster.instance.isCasting)
                    ActionCaster.instance.CancelCasting();
                    
                lastMovePosition = movePoint.position;
                movePoint.position += new Vector3(0, yInput * offsetMovement, 0);
                StartCoroutine("WaitToNextMove");
            }
        }
    }


    private IEnumerator WaitToNextMove()
    {
        waitCouroutineIsRunning = true;
        yield return new WaitForSeconds(secondsToNextMove);
        waitCouroutineIsRunning = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //Se bateu na parede retorna para a última posição
        if(other.gameObject.tag == "Wall")
        {
            movePoint.position = lastMovePosition;
        }
    }
}
