using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CannonScript : MonoBehaviour
{
    // ------------------------- UI -------------------------

    [SerializeField]
    private Text ammoText;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Text cannonStatus;

    [SerializeField]
    public Text CrateText;

    [SerializeField]
    public Text ScoreText;

    [SerializeField]
    public Text MassText;

    // ------------------------------------------------------


    // ------------------------- Camera info -------------------------

    private Vector3 cameraPosition = Vector3.zero;

    [Header("Camera Info")]

    [SerializeField]
    private float pitch = 0f;

    [SerializeField]
    private float yaw = 0f;

    [SerializeField]
    private float radius = 8f;

    [SerializeField]
    private float mouseSensitivity = 0f;

    [SerializeField]
    private float mouseRadiusSensitivity = 0f;
    // --------------------------------------------------------------


    // ------------------------- Cannon info -------------------------

    [Header("Cannon Info")]

    [SerializeField]
    private float delay;

    [SerializeField]
    private int ammo;

    [SerializeField]
    public int NumCrates = 0;

    [SerializeField]
    public int Mass = 0;

    [SerializeField]
    private float timeSeconds;

    [SerializeField]
    private float m_cannonBallSpeed;

    [SerializeField]
    private GameObject m_cannonBallPrefab;

    [SerializeField]
    private Transform m_spawnPoint;

    [SerializeField]
    private GameObject m_rotator = null;

    [SerializeField]
    private GameObject m_barrel = null;

    // --------------------------------------------------------------

    // Yaw (x), Pitch (y)
    [SerializeField]
    private Vector2 rotationSpeed = Vector2.zero;

    private float timesincefire = 0f;
    
    void Start()
    {
        MassText.text = "Crate Mass: " + Mass.ToString() + " kg";

        CrateText.text = "x " + NumCrates.ToString();
        if (Utilities.ScenesChanged == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        Utilities.ScenesChanged++;
        Application.targetFrameRate = 60;

        // The player starts with the cannon already reloaded
        timesincefire = delay;

        // Setup the camera to where it shold be with the yaw and pitch we specified in the inspector
        CalculateSphericalCoordinates();
        Camera.main.transform.position = cameraPosition;
        Camera.main.transform.LookAt(m_barrel.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        HandleCamera();
        HandleFiring();
        HandleUserInput();
        HandleUI();
    }
    private void HandleUI()
    {
        timeSeconds -= Time.deltaTime;
        if (timeSeconds <= 0f)
        {
            timeSeconds = 0f;
            // Load lose scene
            SceneManager.LoadScene("LoseScene");

        }
        ammoText.text = "x " + ammo.ToString("F0");
        timerText.text = "Time: 0:" + timeSeconds.ToString("00");
    }
    private void CalculateSphericalCoordinates()
    {

        // Spherical coordinates:
        // Helpful link: https://tutorial.math.lamar.edu/classes/calciii/SphericalCoords.aspx

        // Camera orbits the centre of the cannon, so we have to calculate its offset from teh centre of it
        cameraPosition = m_barrel.transform.position;

        cameraPosition.x += radius * (Mathf.Cos(yaw * Mathf.Deg2Rad) * Mathf.Cos(pitch * Mathf.Deg2Rad));
        cameraPosition.y += radius * (Mathf.Sin(pitch * Mathf.Deg2Rad));
        cameraPosition.z += radius * (Mathf.Sin(yaw * Mathf.Deg2Rad) * Mathf.Cos(pitch * Mathf.Deg2Rad));
    }

    private void HandleCamera()
    {
        // The mouse x and y axis mapping return a value between -1 and 1. We can use that to rotate the camera's pitch and yaw with this:
        if (Input.GetKey(KeyCode.Mouse0))
        {

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Note: important to use delta time to ensure the speed remains the same regardless of framerate!

            if (mouseX != 0f)
                yaw += mouseX * mouseSensitivity * Time.deltaTime;

            if (mouseY != 0f)
                pitch += mouseY * mouseSensitivity * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            float mouseY = Input.GetAxis("Mouse Y");

            radius -= mouseY * mouseRadiusSensitivity * Time.deltaTime;

            radius = Mathf.Clamp(radius, 6.0f, 18.0f);

        }


        CalculateSphericalCoordinates();

        Camera.main.transform.position = cameraPosition;
        Camera.main.transform.LookAt(m_barrel.transform.position);
    }

    private void HandleUserInput()
    {


        // Better then doing key inputs directly and hard coding the rotation speed
        // This way, the axis mapping will return a value that interpolates to 1 as you press it, making it look more natural rather then strafing rotation
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        if (horizontalAxis != 0.0f)
        {
            // Roll, pitch, yaw
            Vector3 rotationBy = new Vector3(0f, horizontalAxis * rotationSpeed.x * Time.deltaTime, 0f);

            m_rotator.transform.eulerAngles += rotationBy;
        }
        if (verticalAxis != 0.0f)
        {
            Vector3 rotationBy = new Vector3(verticalAxis * rotationSpeed.y * Time.deltaTime, 0f, 0f);

            m_barrel.transform.eulerAngles -= rotationBy;
            float rotX = m_barrel.transform.eulerAngles.x;

            if (rotX <= 315f && rotX >= 315f - rotationSpeed.y)
            {
                rotX = 315f;
            }
            else if (rotX >= 10f && rotX <= 10f + rotationSpeed.y)
            {
                rotX = 10f;
            }
            m_barrel.transform.eulerAngles = new Vector3(rotX, m_barrel.transform.eulerAngles.y, m_barrel.transform.eulerAngles.z);
            
        }
        
    }
    private void HandleFiring()
    {
        timesincefire += Time.deltaTime;

        if (timesincefire <= delay && ammo > 0)
        {
            cannonStatus.text = "--> Reloading...";
            cannonStatus.color = Color.yellow;
        }
        else if (timesincefire >= delay && ammo > 0)
        {
            cannonStatus.text = "--> Ready!";
            cannonStatus.color = Color.green;
        }
        else
        {
            cannonStatus.text = "--> No ammo!";
            cannonStatus.color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.Space) && timesincefire >= delay && ammo > 0)
        {
            Utilities.Multiplier = 1;
            timesincefire = 0f;
            ammo--;
            //GetComponent<ParticleSystem>().Play();

            // Local point of spawn location
            Vector3 direction = m_spawnPoint.position - m_barrel.transform.position;

            // Bring the length of the vector back to 1 (for accurate speed results):
            direction.Normalize();

            // Now scale the vector by the speed we want:
            direction *= m_cannonBallSpeed;

            // Spawn ball and impulse!
            GameObject cannonBall = Instantiate(m_cannonBallPrefab, m_spawnPoint.position, Quaternion.identity);
            cannonBall.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);


        }
    }

}
