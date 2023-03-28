using UnityEngine;

public class aspectRatioFit : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;

    void Start()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        Transform transform1 = object1.transform;
        Transform transform2 = object2.transform;
        RectTransform rectTransform1 = transform1.GetComponent<RectTransform>();
        RectTransform rectTransform2 = transform2.GetComponent<RectTransform>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;

            //上面
            transform1.position = new Vector3(0, (float)Screen.height - (float)Screen.height * rect.y, 0);
            //下面
            transform2.position = new Vector3(0, 0, 0);

            rectTransform1.sizeDelta = new Vector2((float)Screen.width, (float)Screen.height * rect.y);
            rectTransform2.sizeDelta = new Vector2((float)Screen.width, (float)Screen.height * rect.y);
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;

            //左侧
            transform1.position = new Vector3(0, 0, 0);
            //右侧
            transform2.position = new Vector3((float)Screen.width - (float)Screen.width * rect.x, 0, 0);
            rectTransform1.sizeDelta = new Vector2((float)Screen.width * rect.x, (float)Screen.height);
            rectTransform2.sizeDelta = new Vector2((float)Screen.width * rect.x, (float)Screen.height);
        }
    }

    void Update()
    {

    }
}
