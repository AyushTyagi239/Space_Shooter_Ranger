using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Required for InputValue

public class Player : MonoBehaviour
{
    // Store raw input values for movement
    Vector2 rawinput;
    // Adjustable movement speed in Unity Inspector
    [SerializeField] float MoveSpeed = 5f;
    
    // Padding to keep player within screen bounds
    [SerializeField] float padtop;    // Top padding
    [SerializeField] float padright;  // Right padding
    [SerializeField] float padbot;    // Bottom padding
    [SerializeField] float padleft;   // Left padding
    

    // Screen boundaries calculated from camera
    Vector2 minbound; // Bottom-left corner of screen
    Vector2 maxbound; // Top-right corner of screen
    Shooter shooter; // Access to our shooter script

    void Awake(){
        shooter = GetComponent<Shooter>(); //  Capital 'S' in Shooter
    } 

    // Input System callback - triggered when movement keys/joystick are used
    void OnMove(InputValue value){ // Fixed: Typo 'vlaue' → 'value'
        // Get vector2 (x,y) input values (-1 to 1)
        rawinput = value.Get<Vector2>();
        Debug.Log(rawinput); // Show input values in console
    }

    void OnFire(InputValue value){
        if(shooter != null){
            shooter.isFiring = value.isPressed;
        }
    }

    void Start(){
        initbounds(); // Set up screen boundaries when game starts
    }

    void Update()
    {
        Move(); // Update player position every frame
    }

    // Calculate screen boundaries based on camera view
    void initbounds(){
        Camera mainCamera = Camera.main;
        // Convert screen coordinates to world coordinates:
        // (0,0) = bottom-left, (1,1) = top-right of screen
        minbound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxbound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    // Handle player movement and screen boundary enforcement
    void Move(){
        // Calculate movement amount based on input and speed
        Vector2 delta = rawinput * MoveSpeed * Time.deltaTime;
        
        // Calculate new position with boundary restrictions:
        Vector2 newpos = new Vector2();
        // X-axis: Keep between left+padding and right-padding
        newpos.x = Mathf.Clamp(
            transform.position.x + delta.x,
            minbound.x + padleft,
            maxbound.x - padright
        );
        // Y-axis: Keep between bottom+padding and top-padding
        newpos.y = Mathf.Clamp(
            transform.position.y + delta.y,
            minbound.y + padbot,
            maxbound.y - padtop
        );
        
        // Apply the calculated position to the player
        transform.position = newpos;
    }
}