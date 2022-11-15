using UnityEngine;
using _Scripts.U_Variables;

public class BracoRelogio : MonoBehaviour
{

    private void Update()
    {
        Quaternion rot = Quaternion.Euler(0, 0,
                            Mathf.Clamp(hourMap(DaysController.Instance.time.Hours, 0, 90), 0, 90)
                         );
        transform.rotation = rot;
    }

    private float hourMap(float hour, float NewLimStart, float NewLimEnd)
    {
        return NewLimStart + (hour - 7) * (NewLimEnd - NewLimStart) / (DaysController.Instance.finisHourPatients - 7);
    }
}