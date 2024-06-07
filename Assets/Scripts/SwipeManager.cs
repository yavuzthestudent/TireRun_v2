using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    // Swipe durumlarını ve tıklamayı izleyen değişkenler
    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    private void Update()
    {
        // Her güncellemede durumları sıfırla
        tap = swipeDown = swipeUp = swipeLeft = swipeRight = false;

        #region Standalone Inputs
        // Bilgisayar için tıklama algılaması
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        // Mobil cihaz için dokunma algılaması
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        // Kaydırma mesafesini hesapla
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        // Belirli bir mesafe geçildi mi?
        if (swipeDelta.magnitude > 100)
        {
            // Hangi yönde?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Sol veya Sağ
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                // Yukarı veya Aşağı
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            // Durumu sıfırla
            Reset();
        }
    }

    // Durumları sıfırla
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
