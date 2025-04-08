using UnityEngine;
using AK.Wwise; // Wwise 이벤트 쓰는 경우 필요

public class FootstepLimiter : MonoBehaviour
{
    public AkEvent footstepEvent;        // Unity Inspector에서 할당
    public float cooldownTime = 0.1f;    // 두 소리 사이 최소 시간 간격
    private float lastPlayTime = -1f;

    public void PlayFootstep()
    {
        if (Time.time - lastPlayTime > cooldownTime)
        {
            footstepEvent?.HandleEvent(gameObject);
            lastPlayTime = Time.time;
        }
    }
}
