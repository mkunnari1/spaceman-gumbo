using UnityEngine;
using UnityEngine.SceneManagement;

public class Fart : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] float RotationSpeed = 200.0f;
    [SerializeField] float ThrustPower = 100000.0f;
    [SerializeField] AudioClip Farts;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] ParticleSystem Gas;
    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] ParticleSystem SuccessParticle;
    AudioSource audioSource;
    bool playFart;
    bool toggleFartSound;
    enum State {Alive, Dying, NextLevel };
    State state = State.Alive;
    int nextLevel;
    float nextLevelDelay = 1.0f;
    float deathDelay = 2.5f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
        playFart = true;
        toggleFartSound = true;
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        
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
        if(state == State.Alive)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rigidBody.AddRelativeForce(Vector3.up * ThrustPower * Time.deltaTime);
                


            }
        }
        //thrust
       
        
    }

    private void Rotation()
    {
        if (state == State.Alive)
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
       
    }

    private void DeathAudio()
    {
        if ( state == State.Dying)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(DeathSound);
        }
        
    }

    //audio for gumbo and his fart rocket
    private void PlayGumboFart()
    {
        if (state == State.Alive)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {

                //start fart sound for gumbo
                if (playFart == true && toggleFartSound == true)
                {
                    audioSource.PlayOneShot(Farts);
                    Gas.Play();
                    toggleFartSound = false;
                }

            }
            //stop fart sound for gumbo
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                audioSource.Stop();
                Gas.Stop();
                toggleFartSound = true;
            }
        }
        
    }
    // functions that are invoked for level competion and death
    //they say they are not referenced but they are used through invoke within OnCollisonEnter so don't touch 
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
    private void Death()
    {
        SceneManager.LoadScene(0);
    }
    // Defines what happens when character runs into stuff
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                
                break;
            case "Finish":
                state = State.NextLevel;
                SuccessParticle.Play();
                Invoke ("LoadNextLevel", nextLevelDelay);
                break;
            case "Food":
                
                break;
            default:
                
                state = State.Dying;
                DeathAudio();
                DeathParticle.Play();
                Invoke("Death", deathDelay);
                break;

        }
    }
}
