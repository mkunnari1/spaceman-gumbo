using UnityEngine;

public class Victory : MonoBehaviour
{
    AudioSource audioSource;
    enum State { Alive, Dying, NextLevel };
    State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            
            default:
                state = State.NextLevel;
                audioSource.Play();
                
                break;

        }
    }
}
