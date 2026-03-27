using UnityEngine;

public class CamraController : MonoBehaviour
{
    bool isWDown = false;
    bool isSDown = false;
    bool isADown = false;
    bool isDDown = false;

    public float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        setIsKeyDown(ref isWDown, KeyCode.W);
        setIsKeyDown(ref isSDown, KeyCode.S);
        setIsKeyDown(ref isADown, KeyCode.A);
        setIsKeyDown(ref isDDown, KeyCode.D);

        if (isWDown)
        {
            gameObject.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (isSDown)
        {
            gameObject.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        }
        if (isADown)
        {
            gameObject.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        if (isDDown)
        {
            gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

        float mouseMagnitude = Mathf.Sign(Vector2.Dot(new Vector2(0, -1), Input.mouseScrollDelta)) * Input.mouseScrollDelta.magnitude;
        gameObject.GetComponent<Camera>().orthographicSize = gameObject.GetComponent<Camera>().orthographicSize + mouseMagnitude > 0 ? gameObject.GetComponent<Camera>().orthographicSize + mouseMagnitude : gameObject.GetComponent<Camera>().orthographicSize;
    }

    void setIsKeyDown(ref bool keyState, KeyCode code)
    {
        if (Input.GetKeyDown(code))
        {
            keyState = true;
        }
        if (Input.GetKeyUp(code))
        {
            keyState = false;
        }
    }
}
