using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Camera mainCamera;
    
    [SerializeField]
    public GameObject bulletPrefab;
    
    [SerializeField]
    public GameObject layerPrefab;
    
    [SerializeField]
    public Material filledMaterial;
    
    [SerializeField]
    public float tolerance;
    
    [SerializeField]
    public float speed;

    private Text countdownText;
    private Text remainBulletsText;
    private List<Vector3> points;

    private float countDownTime = 3.5f;
    private float forceSize = 27.0f;
    private float positionY = 0.5f;
    private float delayTime = 1.0f;

    private bool restart = false;
    private bool moving = false;
    private bool start = false;
    private bool win = false;
    private bool end = false;

    private int remainBullets = 15;
    private int index = 0;
    private int layer = 0;

    private void Start()
    {
        countdownText = GameObject.Find("[CountDown]").GetComponent<Text>();
        countdownText.text = countDownTime.ToString();
        
        remainBulletsText = GameObject.Find("[RemainBullets]").GetComponent<Text>();
        remainBulletsText.enabled = false;

        points = new List<Vector3>();
        /// Collect
        points.Add(new Vector3(0.0f, 0.0f, 0.0f));
        points.Add(new Vector3(0.0f, 0.0f, 2.2f));
        points.Add(new Vector3(1.1f, 0.0f, 2.2f));
        points.Add(new Vector3(1.1f, 0.0f, 4.4f));
        points.Add(new Vector3(-1.1f, 0.0f, 4.4f));
        points.Add(new Vector3(-1.1f, 0.0f, 6.6f));
        points.Add(new Vector3(0.0f, 0.0f, 6.6f));

        /// Fill
        points.Add(new Vector3(0.0f, 0.0f, 9.9f));
        points.Add(new Vector3(-2.2f, 0.0f, 9.9f));
        points.Add(new Vector3(-2.2f, 0.0f, 12.1f));
        points.Add(new Vector3(1.1f, 0.0f, 12.1f));
        points.Add(new Vector3(1.1f, 0.0f, 16.4f));

        tolerance = speed * Time.deltaTime;
    }

    private void Update()
    {
        CountDown();
        if (Input.GetButtonDown("Fire1") && !moving && start)
        {
            moving = true;
        }
        Move();
        UpdateCamera();
        Shoot();
        UpdateRemainBulletsText();
        if (remainBullets < 10)
        {
            CheckGameFinish();
        }
    }

    private void CountDown()
    {
        if (!start)
        {
            if (countDownTime > 0)
            {
                countDownTime -= Time.deltaTime;
            }
        }

        if (Mathf.FloorToInt(countDownTime % 60) == 0)
        {
            if (!start)
            {
                start = true;
            }

            if (end)
            {
                countdownText.fontStyle = FontStyle.Bold;

                if (win)
                {
                    countdownText.text = "Win";
                }
                else
                {
                    countdownText.text = ":(";
                }
                
                delayTime = delayTime - Time.deltaTime;

                if (delayTime < 0.0f)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            else 
            {
                if (delayTime > 0.0f)
                {
                    delayTime -= Time.deltaTime;
                    countdownText.fontStyle = FontStyle.BoldAndItalic;
                    countdownText.text = "GO!";
                }
                else
                {
                    countdownText.text = "";
                }
            }
        }
        else
        {
            countdownText.text = Mathf.FloorToInt(countDownTime % 60).ToString();
        }
    }

    private void Move()
    {
        if (moving && index + 1 < points.Count)
        {
            Vector3 nextPoint = new Vector3(points[index + 1].x, positionY, points[index + 1].z);
            Vector3 heading = nextPoint - transform.position;
            transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
            if (heading.magnitude < tolerance)
            {
                index += 1;
                moving = false;
            }
            
            if (index + 1 == points.Count)
            {
                GameObject targets = GameObject.Find("[Targets]");
                targets.GetComponent<Targets>().EnableAll();
                remainBulletsText.enabled = true;
            }
        }
    }

    private void UpdateCamera()
    {
        if (index + 1 == points.Count)
        {
            var cameraFollowScript = mainCamera.GetComponent<CameraFollow>();
            cameraFollowScript.offset = new Vector3(0.0f, 1.0f, -2.0f);
            cameraFollowScript.transform.rotation = Quaternion.Euler(5.0f, 0.0f, 0.0f);
        }
    }

    private void Shoot()
    {
        if (index + 1 == points.Count)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Input.touchCount > 0)
                {
                    ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                }

                if (Physics.Raycast(ray, out RaycastHit hitInfo) && remainBullets != 0)
                {
                    if (hitInfo.collider.gameObject != null)
                    {
                        Vector3 bulletPosition = new Vector3(1.1f, 0.5f, 18.0f);
                        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
                        Vector3 distanceToTarget = hitInfo.point - bulletPosition;
                        Vector3 forceDirection = distanceToTarget.normalized;
                        bullet.GetComponent<Rigidbody>().AddForce(forceDirection * forceSize, ForceMode.Impulse);
                        remainBullets--;
                    }
                }
            }
        }
    }

    private void UpdateRemainBulletsText()
    {
        remainBulletsText.text = remainBullets.ToString();
    }
    
    private void CheckGameFinish()
    {
        var remainTargets = GameObject.FindGameObjectsWithTag("Target").Length;
        if (remainTargets == 0 || remainBullets == 0)
        {
            end = true;
            if (remainTargets == 0)
            {
                win = true;
            }
            if (!restart)
            {
                delayTime = 3.0f;
                restart = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collect(collision);
        Fill(collision);
    }

    private void Collect(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collect"))
        {
            AddLayer();
        }
    }

    private void AddLayer()
    {
        layer += 1;
        Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
        position.y = transform.position.y + (layer * 0.22f);
        layerPrefab.transform.position = position;
        Instantiate(layerPrefab, transform, false);
    }

    private void Fill(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fill"))
        {
            collision.transform.localScale = new Vector3(1.0f, 0.2f, 1.0f);
            collision.transform.gameObject.GetComponent<Renderer>().material = filledMaterial;
            RemoveLayer();
        }
    }

    private void RemoveLayer()
    {
        Destroy(transform.GetChild(layer - 1).gameObject);
        layer -= 1;
    }
    
    private void OnCollisionExit(Collision collision)
    {
        ResizeCollected(collision);
    }

    private void ResizeCollected(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collect"))
        {
            collision.transform.localScale = new Vector3(1.0f, 0.1f, 1.0f);
            Vector3 position = collision.transform.position;
            position.y = 0.05f;
            collision.transform.position = position;
        }
    }
}
