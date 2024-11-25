using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public DynamicJoystick joystick;
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 2f;
    public float threshold = 0.5f;

    private Animator animator;
    private Rigidbody rb;
    private string currentAnimation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveCharacter(); 
        PlayAnimation(); 
    }

    void MoveCharacter()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        float currentSpeed = moveSpeed;
        if (direction.magnitude > threshold)
        {
            currentSpeed *= runSpeedMultiplier;
        }

        if (rb != null)
        {
            rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);
        }

        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void PlayAnimation()
    {
        float joystickMagnitude = new Vector2(joystick.Horizontal, joystick.Vertical).magnitude;
        string newAnimation;

        if (joystickMagnitude > threshold)
        {
            newAnimation = "Run";
        }
        else if (joystickMagnitude > 0.01f)
        {
            newAnimation = "Walk";
        }
        else
        {
            newAnimation = "Idle";
        }

        if (newAnimation != currentAnimation)
        {
            float transitionTime = GetTransitionTime(currentAnimation, newAnimation);
            currentAnimation = newAnimation;
            animator.CrossFade(currentAnimation, transitionTime);
        }
    }

    float GetTransitionTime(string from, string to)
    {
        if ((from == "Idle" && to == "Run") || (from == "Run" && to == "Idle"))
        {
            return 0.01f;
        }

        if ((from == "Walk" && to == "Run") || (from == "Run" && to == "Walk"))
        {
            return 0.2f;
        }

        if ((from == "Idle" && to == "Walk") || (from == "Walk" && to == "Idle"))
        {
            return 0.01f;
        }

        return 0.3f;
    }
}