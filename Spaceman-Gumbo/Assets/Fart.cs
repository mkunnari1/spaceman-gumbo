using UnityEngine;
using UnityEngine.SceneManagement;

public class Fart : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] float RotationSpeed = 200.0f;
    [SerializeField] float ThrustPower = 100000.0f;
    AudioSource audioSource;
    bool playFart;
    bool toggleFartSound;
    
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
        playFart = true;
        toggleFartSound = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        Thrust();
        Rotation();
        PlayGumboFart();

        
    }
    //controls for user and define how thrust will work
    private void Thrust()
    {

        //thrust
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rigidBody.AddRelativeForce(Vector3.up*ThrustPower*Time.deltaTime);


        }
        
    }

    private void Rotation()
    {
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * RotationSpeed * Time.deltaTime);
        }
        rigidBody.freezeRotation = false;
    }

    //audio for gumbo and his fart rocket
    private void PlayGumboFart()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            
            //start fart sound for gumbo
            if (playFart == true && toggleFartSound == true)
            {
                audioSource.Play();
                toggleFartSound = false;
            }

        }
        //stop fart sound for gumbo
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            audioSource.Stop();
            toggleFartSound = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Finish":
                print("finish");
                SceneManager.LoadScene(1);
                break;
            case "Food":
                print("Food");
                break;
            default:
                print("Dead");
                SceneManager.LoadScene(0);
                break;

        }
    }
}
