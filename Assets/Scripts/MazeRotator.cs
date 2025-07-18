using UnityEngine;

public class MazeRotator : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        float input = 0;

#if UNITY_EDITOR
        // Editörde test için: A/D tuşları
        if (Input.GetKey(KeyCode.A)) input = 1;
        if (Input.GetKey(KeyCode.D)) input = -1;
#else
        // Mobil için: Ekranın sağına/soluna dokunma
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x < Screen.width / 2) input = 1;
                else input = -1;
            }
        }
#endif

        transform.Rotate(0, 0, input * rotationSpeed * Time.deltaTime);
    }
}