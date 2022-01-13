using UnityEngine.SceneManagement;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private bool gotHit = false;
    private float delayReload = 0.0f;

    private void Update()
    {
        if (gotHit)
        {
            if (delayReload % 60 >= 10.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            delayReload += Time.deltaTime;
        }
    }

    public void StartReloadScene()
    {
        gotHit = true;
    }
}
