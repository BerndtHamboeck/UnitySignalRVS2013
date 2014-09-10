using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Camera cam;
    private float maxWidth;
    private float maxHeight;

    bool isHandOpen = true;
    public Sprite clickedSprite;
    private Sprite standardSprite;

    private SignalRUnityController _signalr;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        _signalr = (SignalRUnityController)GameObject.Find("SignalRObject")
            .GetComponent(typeof(SignalRUnityController));

        var corner = new Vector3(Screen.width, Screen.height, 0f);
        var targetWidth = cam.ScreenToWorldPoint(corner);
        float playerWidth = renderer.bounds.extents.x;
        float playerHeight = renderer.bounds.extents.y;
        maxWidth = targetWidth.x - playerWidth;
        maxHeight = targetWidth.y - playerHeight;

        standardSprite = this.GetComponent<SpriteRenderer>().sprite;

    }
    void FixedUpdate()
    {
        var currentPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        float targetWidth = Mathf.Clamp(currentPosition.x, -maxWidth, maxWidth);
        float targetHeight = Mathf.Clamp(currentPosition.y, -maxHeight, maxHeight);
        var newPosition = new Vector3(targetWidth, targetHeight, 0f);
        rigidbody2D.MovePosition(newPosition);

        if (Input.GetMouseButtonDown(0))
        {
            isHandOpen = false;
            MouseDown();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isHandOpen = true;
            MouseUp();
        }

        var worldCoordinates = cam.WorldToScreenPoint(newPosition);
        var json = "{" + string.Format("\"x\": \"{0}\", \"y\": \"{1}\", \"handOpen\": \"{2}\"", 
            worldCoordinates.x, worldCoordinates.y, isHandOpen) + "}";
        _signalr.Send("Position", json);

    }

    private void MouseDown()
    {
        Debug.Log("MouseDown");
        this.GetComponent<SpriteRenderer>().sprite = clickedSprite;

    }

    private void MouseUp()
    {
        Debug.Log("MouseUp");
        this.GetComponent<SpriteRenderer>().sprite = standardSprite;
    }
}
