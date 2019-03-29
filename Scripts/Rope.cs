using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rope : MonoBehaviour
{
    [Header("Attributes")]
    public float baseRotationSpeed = 100f;
    public float rotationSpeed = 100f;
    public static float topClamp = 188f;
    public static float minClamp = 72.5f;
    public int consecutiveSuccess = 0;
    public PhysicMaterial bounceMaterial;
    public int count = 0;
    public int soundFXVolume;

    [Header("Third Party Objects")]
    public GameObject rope;
    public AudioClip[] audioClips;
    public GameObject ballPrefab;
    public GameObject appearEffectPrefab;
    public GameObject disappearEffectPrefab;

    [Header("Testing Values")]
    public float currentRotation;
    public float ropeSize = 5.2f;
    public float ballSize = 0.55f;
    public int givenScore = 50;
    public float speedIncrement = 5f;
    public int scoreIncrement = 20;
    public float consecutiveSuccessSpeedIncrement = 15f;
    public int consecutiveSuccessScoreIncrement = 30;

    private GameObject ball;
    private AudioSource audioSource;
    private GameObject targetObject;
    private bool isDone = false;
    public float totalSize;
    private float[] pos = new float[2];
    private bool isAbleToIncreaseConsecutive = true;

    // Create Yolo
    void CreateTargetObject()
    {
        float minAngle = 30f, maxAngle = 150f;
        // Pick a random angle and calculate its respective position on scanning area
        float randomAngle = Random.RandomRange(30f, 150f);
        float rotation;

        CalculatePos(150f);
        // Calculate angle for top clamp
        if(pos[1] >= topClamp)
            maxAngle = Mathf.Asin((topClamp - this.transform.position.y) / totalSize) * Mathf.Rad2Deg;

        CalculatePos(390f);
        // Calculate angle for bottom clamp
        if(pos[1] <= minClamp)
            minAngle = Mathf.Acos((this.transform.position.y - minClamp) / totalSize) * Mathf.Rad2Deg;

        if (randomAngle >= 90f)
            if (randomAngle >= maxAngle)
                randomAngle = maxAngle;

        else if (randomAngle >= 0f)
            if (randomAngle <= minAngle)
                randomAngle = minAngle;

        
        randomAngle = Random.RandomRange(minAngle, maxAngle);
        rotation = randomAngle;

        CalculatePos(randomAngle);

        // Instantiate a yolo at calculated position and angle
        targetObject = Instantiate(appearEffectPrefab, new Vector3(pos[0], pos[1], this.transform.position.z), Quaternion.Euler(0f, 0f, rotation)) as GameObject;
        Pattern targetPattern = targetObject.GetComponent<Pattern>();
        targetPattern.currentRotation = rotation;

        if (count % 8 == 0 && count <= 56f)
            this.baseRotationSpeed = 100f + (count / 8) * 15f;

        if (count > 56)
            targetPattern.patternIndex = (int)Random.Range(0f, 7f);
        else
            targetPattern.patternIndex = count / 8;

        targetPattern.minAngleToScan = minAngle;
        targetPattern.maxAngleToScan = maxAngle;
        targetPattern.rope = this.transform;
    }

    // Calculate (X, Y) position on orbit for given angle
    private void CalculatePos(float angle)
    {
        float newX, newY;
        angle -= 90;
        if (angle >= 270f)
        {
            angle -= 270;
            newX = totalSize * Mathf.Sin(angle * Mathf.Deg2Rad);
            newY = this.transform.position.y - Mathf.Sqrt(totalSize * totalSize - newX * newX);
            newX += this.transform.position.x;

        }
        else if (angle >= 180f)
        {
            angle -= 180;
            newY = totalSize * Mathf.Sin(angle * Mathf.Deg2Rad);
            newX = this.transform.position.x - Mathf.Sqrt(totalSize * totalSize - newY * newY);
            newY = this.transform.position.y - newY;

        }
        else if (angle >= 90f)
        {
            angle -= 90;
            newX = totalSize * Mathf.Sin(angle * Mathf.Deg2Rad);
            newY = this.transform.position.y + Mathf.Sqrt(totalSize * totalSize - newX * newX);
            newX = this.transform.position.x - newX;

        }
        else
        {
            newY = totalSize * Mathf.Sin(angle * Mathf.Deg2Rad);
            newX = this.transform.position.x + Mathf.Sqrt(totalSize * totalSize - newY * newY);
            newY += this.transform.position.y;
        }

        pos[0] = newX;
        pos[1] = newY;
    }

    // Adjust other objects' position
    private void Awake()
    {
        soundFXVolume = PlayerPrefs.GetInt("SOUND_KEY");
        Camera.main.GetComponent<GameCamera>().SetPosition(new Vector3(this.transform.position.x <= 64f ? 64f : this.transform.position.x,
                                                                       this.transform.position.y,
                                                                       this.transform.position.z) + new Vector3(0f, -1f, -18f));
    }
    
    void Start ()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.volume = soundFXVolume;
        totalSize = ballSize + ropeSize;
        ball = Instantiate(ballPrefab, this.transform.position + new Vector3(0f, -totalSize, 0f), Quaternion.Euler(0f, 90f, 0f)) as GameObject;
        ball.transform.localScale = new Vector3(1.042655f, 1.042655f, 1.042655f);
        ball.GetComponent<Renderer>().material.mainTexture = TextureManager.instance.selectedTexture;
        CreateTargetObject();
        currentRotation = 0f;
	}

    void Update()
    {
        Vector3 posRelativeToCamera = Camera.main.WorldToViewportPoint(this.transform.position);
        if(posRelativeToCamera.x < -0.1f)
        {
            Destroy(ball);
            Destroy(this.gameObject);
        }

        if (currentRotation >= 180f)
        {
            consecutiveSuccess = 0;
            this.rotationSpeed = baseRotationSpeed;
            isAbleToIncreaseConsecutive = false;
        }

        // Check if rope has completed its lifecycle
        if (!isDone)
        {
            // Endless Rotate
            currentRotation += Time.deltaTime * rotationSpeed;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0f, 0f, currentRotation), Time.deltaTime * rotationSpeed);
            ball.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0f, 90f, currentRotation), Time.deltaTime * rotationSpeed);
            CalculatePos(currentRotation);
            ball.transform.position = new Vector3(pos[0], pos[1], this.transform.position.z);
            currentRotation = currentRotation > 360f ? (currentRotation / 360f) : currentRotation;
            // On leftClick / Touch check for collision
            if (Input.GetMouseButtonDown(0))
                CheckCollision();
        }

	}

    // Check if half of the ball is lying inside the yolo
    void CheckCollision()
    {
        isDone = true;
        float ropeAngle = this.transform.localEulerAngles.z;
        float targetObjectAngle = targetObject.transform.localEulerAngles.z;
        float desiredAngle = Mathf.Acos(1 - (((2 * ballSize / (10f / 7f)) * (2 * ballSize / (10f / 7f))) / (2 * totalSize * totalSize))) * Mathf.Rad2Deg;
        float difference = Mathf.Abs(targetObjectAngle - ropeAngle);

        if (difference <= desiredAngle)
        {
            if (isAbleToIncreaseConsecutive)
                consecutiveSuccess = consecutiveSuccess < 9 ? consecutiveSuccess + 1 : consecutiveSuccess;

            float newSpeed = Mathf.Clamp(rotationSpeed + speedIncrement + consecutiveSuccess * consecutiveSuccessSpeedIncrement, 0f, 250f);
            
            CalculatePos(currentRotation);
            Vector3 newPos = new Vector3(pos[0], pos[1], this.transform.position.z - (count % 4 == 0 ? -2f : 1f)); // Will be changed when real objects imported

            // Create a sound

            audioSource.PlayOneShot(audioClips[consecutiveSuccess]); // Clip indices will be determined
            // index = consecutiveSuccess?

            Instantiate(disappearEffectPrefab,  ball.transform.position + new Vector3(0.17f, -0.17f, 0f), Quaternion.identity);

            // Create a rope and set new attributes
            GameObject nextRope = Instantiate(rope, newPos, this.transform.rotation) as GameObject;
            Rope ropeObject = nextRope.GetComponent<Rope>();

            if (this.baseRotationSpeed == 180f)
                ropeObject.baseRotationSpeed = 180f;
            else
                ropeObject.baseRotationSpeed = this.baseRotationSpeed + speedIncrement;
            ropeObject.rotationSpeed = newSpeed;
            ropeObject.givenScore += scoreIncrement;
            ++ropeObject.count;

            GameManagement.instance.IncreaseScore(givenScore + consecutiveSuccess * consecutiveSuccessScoreIncrement);

            this.gameObject.isStatic = true;
            ball.GetComponent<Ball>().isDone = true;
            Destroy(targetObject);
        }

        else
        {
            // Create a sound
            
            audioSource.PlayOneShot(audioClips[audioClips.Length - 1]);
            
            Vector3 forceDirection = Vector3.zero;

            float tempRotation = currentRotation >= 270f ? currentRotation - 270 : (currentRotation >= 180f ? currentRotation - 180f : (currentRotation >= 90f ? currentRotation - 90f : currentRotation));

            float currentRotationInRadians = (90f - tempRotation) * Mathf.Deg2Rad;

            ball.AddComponent<SphereCollider>();
            ball.AddComponent<Rigidbody>();
            ball.GetComponent<SphereCollider>().material = bounceMaterial;

            if (currentRotation >= 270f)
            {
                forceDirection.x = Mathf.Cos(currentRotationInRadians);
                forceDirection.y = -Mathf.Sin(currentRotationInRadians);
            }
            else if (currentRotation >= 180f)
            {
                forceDirection.y = -Mathf.Cos(currentRotationInRadians);
                forceDirection.x = -Mathf.Sin(currentRotationInRadians);
            }
            else if (currentRotation >= 90f)
            {
                forceDirection.x = -Mathf.Cos(currentRotationInRadians);
                forceDirection.y = Mathf.Sin(currentRotationInRadians);
            }
            else
            {
                forceDirection.y = Mathf.Cos(currentRotationInRadians);
                forceDirection.x = Mathf.Sin(currentRotationInRadians);
            }


            ball.GetComponent<Rigidbody>().AddForce(forceDirection * 500f);
            GameManagement.instance.SetHighestScore();
        }
    }

    // Visualizing the area rope scans
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, ropeSize + ballSize);
    }
}
