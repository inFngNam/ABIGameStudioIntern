using UnityEngine.SceneManagement;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    private bool gotHit = false;
    private bool hitted = false; 

    private float counter = 0.0f;
    private float animateCounter = 0.0f;

    private void Start()
    {
    }

    private void Update()
    {
        if (gotHit && player.GetComponent<Player>().kicked)
        {
            if (animateCounter % 60 >= 1.13f)
            {
                hitted = true;
                gameObject.GetComponent<Animator>().enabled = false;
                gotHit = false;
            }
            animateCounter += Time.deltaTime;
        }
        if (hitted)
        {
            if (counter % 60 >= 10.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            counter += Time.deltaTime;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gotHit = true;
        }
    }


}
