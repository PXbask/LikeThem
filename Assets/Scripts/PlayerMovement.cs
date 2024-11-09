using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float bufferingTime = .05f;
    [SerializeField] float ignoreSoundTime = .01f;


    Rigidbody2D rb;
    Animator animator;
    PauseMenu pauseMenu;
    LastMovePress lastPress;

    bool isMoving = false;
    float lastTimeSound;

    Vector3 normalScale;
    Vector3 flippedScale;

    [Serializable]
    public struct LastMovePress
    {
        public Vector2 direction;
        public float time;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        lastPress = new LastMovePress
        {
            direction = Vector2.zero,
            time = Time.time
        };
        lastTimeSound = Time.time;
        normalScale = new Vector3(1, 1, 1);
        flippedScale = new Vector3(1, -1, 1);
    }
    private void Update()
    {
        if (rb.velocity.magnitude < 40)
        {
            float timeBetweenSounds = Time.time - lastTimeSound;
            if (isMoving && timeBetweenSounds >= ignoreSoundTime)
            {
                lastTimeSound = Time.time;
                AudioManager.instance.Play("MovementStop");
            }
            isMoving = false;
            animator.SetBool("isMoving", false);
        }
        float timeBetweenLastPress = Time.time - lastPress.time;
        if (!isMoving && (timeBetweenLastPress <= bufferingTime) && (timeBetweenLastPress != 0))
        {
            MoveToLastDirection();
        }
    }

    public void MoveToLastDirection()
    {
        Move(lastPress.direction);
    }

    private void OnMove(InputValue value)
    {
        Vector2 movement = value.Get<Vector2>();

        if (movement != Vector2.zero) SaveLastMove(movement);

        //Debug.Log(lastMovement.movement);

        //if (lastPress.direction == movement)
        //{
        //    return;
        //}

        if (isMoving) return;
        Move(movement);
    }

    private void SaveLastMove(Vector2 movement)
    {
        lastPress.direction = movement;
        lastPress.time = Time.time;
    }

    public void Move(Vector2 movement)
    {
        if (movement == Vector2.right)
        {
            MoveRight();
        }
        else if (movement == Vector2.left)
        {
            MoveLeft();
        }
        else if (movement == Vector2.up)
        {
            MoveUp();
        }
        else if (movement == Vector2.down)
        {
            MoveDown();
        }
    }

    private void OnPause(InputValue value)
    {
        pauseMenu.Pause();
    }

    //private void OnEnable()
    //{
    //    if (isMoving)
    //    {
    //        animator.SetBool("isMoving", true);
    //        rb.velocity = lastPress.direction;
    //    }
    //}

    private void MoveDown()
    {
        rb.velocity = Vector2.down * moveSpeed;
        isMoving = true;

        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = normalScale;
        animator.SetBool("isMoving", true);
    }

    private void MoveUp()
    {
        rb.velocity = Vector2.up * moveSpeed;
        isMoving = true;

        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = flippedScale;
        animator.SetBool("isMoving", true);
    }

    private void MoveLeft()
    {
        rb.velocity = Vector2.left * moveSpeed;
        isMoving = true;

        transform.rotation = Quaternion.Euler(0, 0, 90);
        transform.localScale = flippedScale;
        animator.SetBool("isMoving", true);
    }

    private void MoveRight()
    {
        rb.velocity = Vector2.right * moveSpeed;
        isMoving = true;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        transform.localScale = normalScale;
        animator.SetBool("isMoving", true);
    }

    public void Freeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    public void ForceBack(float force)
    {
        // Calculate Angle Between the collision point and the player
        //Vector3 dir = collider.ClosestPoint collision.contacts[0].point - (Vector2) transform.position;
        // We then get the opposite (-Vector3) and normalize it
        // And finally we add force in the direction of dir and multiply it by force. 
        // This will push back the player
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //rb.AddForce(-lastMovement * force);
        //StartCoroutine(AddDrag());
    }

    //IEnumerator AddDrag()
    //{

    //    float current_drag = 0;

    //    while (current_drag < 10)
    //    {
    //        current_drag += Time.deltaTime * 10;
    //        rb.drag = current_drag;
    //        yield return null;
    //    }

    //    rb.velocity = Vector3.zero;
    //    rb.angularVelocity = 0f;
    //    rb.drag = 0;
    //}
}
