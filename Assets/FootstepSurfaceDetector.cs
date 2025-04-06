using UnityEngine;
using AK.Wwise;

public class FootstepSurfaceDetector : MonoBehaviour
{
    public string defaultSurfaceTag = "grass";  // 기본 표면 태그 (소문자)
    public AK.Wwise.Switch grassSwitch;
    public AK.Wwise.Switch concreteSwitch;
    public AK.Wwise.Event footstepEvent;  // 실행할 Wwise 이벤트

    // 애니메이션 이벤트에서 이 메서드를 호출
    public void PlayFootstep()
    {
        PlayFootstepSound();
    }

    private void PlayFootstepSound()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f);

        // 씬에 레이 표시 (디버깅용)
        Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.red, 1f);

        string surfaceTag = defaultSurfaceTag;

        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name + ", tag: " + hit.collider.tag);
            surfaceTag = hit.collider.tag.ToLower();  // 태그를 소문자로 강제 변환
        }
        else
        {
            Debug.Log("Raycast didn't hit anything.");
        }

        // 태그에 따라 스위치 설정
        switch (surfaceTag)
        {
            case "grass":
                grassSwitch.SetValue(gameObject);
                break;
            case "concrete":
                concreteSwitch.SetValue(gameObject);
                break;
            default:
                Debug.Log("Unknown surface tag: " + surfaceTag + " - using default");
                grassSwitch.SetValue(gameObject);  // 디폴트 스위치 (grass)
                break;
        }

        // 풋스텝 이벤트 실행
        if (footstepEvent != null)
        {
            footstepEvent.Post(gameObject);
        }
    }
}
