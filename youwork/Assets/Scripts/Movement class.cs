using UnityEngine;

[System.Serializable]
public class Movement
{
    //Inspector
    public float walkingSpeed, runningSpeed;
    public float speedModifier = 1f;
    public float jumpLength;
    public float jumpSpeed;

    //Working
    [HideInInspector]
    public Vector2 lookDirection = new Vector2(0, 1);
    [HideInInspector]
    public Vector2 input;

    public Movement()       //Standard
    {

    }

    public Movement(float walkingSpeed, float runningSpeed, float jumpLength, float jumpSpeed = 1)
    {
        this.walkingSpeed = walkingSpeed;
        this.runningSpeed = runningSpeed;
        this.jumpLength = jumpLength;
        this.jumpSpeed = jumpSpeed;
    }
}
