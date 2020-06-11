using UnityEngine;

namespace CardMemory
{
    public class CameraSetting : MonoBehaviour
    {
        private void Awake()
        {
            ScreenSetting();
        }

        private void ScreenSetting()
        {
            Camera camera = GetComponent<Camera>();
            Rect rect = camera.rect;

            float scaleHight = ((float)Screen.width / Screen.height) / ((float)16 / 9);
            float scaleWidth = 1f / scaleHight;

            if (scaleHight < 1)
            {
                rect.height = scaleHight;
                rect.y = (1f - scaleHight) / 2f;
            }
            else
            {
                rect.width = scaleWidth;
                rect.x = (1f - scaleWidth) * 0.5f;
            }

            camera.rect = rect;
        }
    }
}
