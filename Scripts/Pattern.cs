using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [Header("Attributes")]
    public float currentRotation;
    public float speed = 0f;
    public float minAngleToScan;
    public float maxAngleToScan;
    public float speedChange = 0f;
    public float angleToSpeedChange = -1f;
    public bool  isMovingUp = true;
    public int patternIndex;
    public GameObject referenceObject;
    public float totalSize = 5.75f;
    public float cooldownTimer = 0f;
    public float cooldownTime = 0.5f;
    public float baseSpeed;

    public float newX, newY;
    public Transform rope;

    // Use this for initialization
    void Start()
    {
        if(patternIndex == 7)
        {
            speed = Random.Range(45f, 75f);
            minAngleToScan -= 10;
        }

        else if (patternIndex == 5)
            cooldownTime = 0.5f;
        else if (patternIndex == 6)
            cooldownTime = 2.3f;
        else if (patternIndex == 3)
            speed = (maxAngleToScan - minAngleToScan) * 1.3f;

       else if(patternIndex == 2)
        {
            speed = Random.Range(45f, 75f);
            angleToSpeedChange = (int)Random.Range(minAngleToScan, maxAngleToScan);
            speedChange = 20f;
        }

        else if (patternIndex == 1)
            speed = Random.Range(35f, 65f);

        baseSpeed = speed;
        if (patternIndex == 4)
        {
            speed = 5f;
            baseSpeed = (maxAngleToScan - minAngleToScan) * 2;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

        cooldownTimer -= Time.deltaTime;

        // Direction change
        if (Mathf.Abs(maxAngleToScan - minAngleToScan) <= 1.5f)
            speed = 0;

        else if (currentRotation <= minAngleToScan)
        {
            if (patternIndex == 7)
                maxAngleToScan -= (maxAngleToScan - minAngleToScan) / 5f;
            else if (patternIndex == 4)
                speed = 10f;
            else if (patternIndex == 3)
                speed = baseSpeed;
            isMovingUp = true;
        }
        
        

        else if (currentRotation >= maxAngleToScan)
        {
            if (patternIndex == 7)
                minAngleToScan += (maxAngleToScan - minAngleToScan) / 5f;
            else if (patternIndex == 4)
                speed = 5f;
            else if (patternIndex == 3)
                speed = baseSpeed;
            isMovingUp = false;
        }

        // Rotation change logic
        if (!isMovingUp)
            currentRotation -= Time.deltaTime * speed;
        else
            currentRotation += Time.deltaTime * speed;


        // Changes for different patterns
        if (patternIndex > 0)
        {
            if(patternIndex == 6)
            {
                SpriteRenderer spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
                Color mColor = spriteRenderer.color;
                if (cooldownTimer <= 0f)
                {
                    mColor.a = 1f;
                    cooldownTimer = cooldownTime;
                    currentRotation = Random.Range(minAngleToScan, maxAngleToScan);
                }

                else
                    mColor.a -= Time.deltaTime * (1f / cooldownTime);

                spriteRenderer.color = mColor;
            }

            else if(patternIndex == 5)
            {
                if(cooldownTimer <= 0f)
                {
                    cooldownTimer = cooldownTime;
                    currentRotation += isMovingUp ? (maxAngleToScan - minAngleToScan) / 9f : -(maxAngleToScan - minAngleToScan) / 9f;
                }
            }

            else if (patternIndex == 4)
                speed += baseSpeed * Time.deltaTime;
            else if(patternIndex == 3)
                speed -= baseSpeed * Time.deltaTime / 2f;

            else if(patternIndex == 2)
            {
                if (currentRotation < angleToSpeedChange)
                    speed = baseSpeed - speedChange;
                else
                    speed = baseSpeed;
            }

            CalculatePos(currentRotation);
            this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, currentRotation);
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(newX, newY, this.transform.position.z), (patternIndex == 5 || patternIndex == 6) ? 0.8f : Time.deltaTime * speed);
        }
	}

    private void CalculatePos(float angle)
    {
        angle -= 90;
        if (angle >= 270f)
        {
            angle -= 270;
            newX = totalSize * Mathf.Sin(angle * Mathf.Deg2Rad);
            newY = rope.transform.position.y - Mathf.Sqrt(totalSize * totalSize - newX * newX);
            newX += rope.transform.position.x;

        }
        else if (angle >= 180f)
        {
            angle -= 180;
            newY = totalSize * Mathf.Sin(angle * Mathf.Deg2Rad);
            newX = rope.transform.position.x - Mathf.Sqrt(totalSize * totalSize - newY * newY);
            newY = rope.transform.position.y - newY;

        }
        else if (angle >= 90f)
        {
            angle -= 90;
            newX = totalSize * Mathf.Sin(angle * Mathf.Deg2Rad);
            newY = rope.transform.position.y + Mathf.Sqrt(totalSize * totalSize - newX * newX);
            newX = rope.transform.position.x - newX;

        }
        else
        {
            newY = totalSize * Mathf.Sin(angle * Mathf.Deg2Rad);
            newX = rope.transform.position.x + Mathf.Sqrt(totalSize * totalSize - newY * newY);
            newY += rope.transform.position.y;
        }
    }
}
