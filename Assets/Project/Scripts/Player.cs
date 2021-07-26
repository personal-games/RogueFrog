using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public delegate void PlayerHandler();
    public event PlayerHandler OnPlayerMoved;
    public event PlayerHandler OnPlayerEscaped;

    public float jumpDistance = 0.32f;
    private bool jumped;
    private Vector3 startingPosition;

	// Use this for initialization
	void Start () {
        startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        if (!jumped) {
            // the frog didn't move
            Vector2 targetPosition = Vector2.zero;
            bool tryingToMove = false;
            if (horizontalMovement !=0 ) {
                tryingToMove = true;
                targetPosition = new Vector2(
                    transform.position.x + (horizontalMovement > 0 ? jumpDistance : -jumpDistance),
                    transform.position.y
                );
            } else if (verticalMovement !=0){
                tryingToMove = true;
                targetPosition = new Vector2(
                    transform.position.x,
                    transform.position.y + (verticalMovement > 0 ? jumpDistance : -jumpDistance)
                );
            }

            Collider2D hitCollider = Physics2D.OverlapCircle(targetPosition, 0.1f);
            if (tryingToMove == true && (hitCollider == null || hitCollider.GetComponent<Enemy>() !=null )) {
                transform.position = targetPosition;
                jumped = true;
                GetComponent<AudioSource>().Play();
                if (OnPlayerMoved !=null) {
                    OnPlayerMoved();
                }
            }

        } else {
            // the frog moved.
            if (horizontalMovement == 0 && verticalMovement == 0) {
                jumped = false;
            }
        }

        // keep the frog inside bounds.
        if (transform.position.y < -(Screen.height / 100f) / 2f){
            transform.position = new Vector3(transform.position.x, transform.position.y + jumpDistance);
        }

        if (transform.position.y > (Screen.height / 100f) / 2f)
        {
            transform.position = startingPosition;
            if (OnPlayerEscaped !=null) {
                OnPlayerEscaped();
            }
        }

        if (transform.position.x < -(Screen.width / 100f) / 2f)
        {
            transform.position = new Vector3(transform.position.x + jumpDistance, transform.position.y);
        }

        if (transform.position.x > (Screen.width / 100f) / 2f)
        {
            transform.position = new Vector3(transform.position.x - jumpDistance, transform.position.y);
        }
	}

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<Enemy>() !=null) {
            Destroy(gameObject);
        }
    }
}
