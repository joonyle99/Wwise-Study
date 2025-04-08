using UnityEngine;
using AK.Wwise;

public class FootstepSurfaceDetector : MonoBehaviour
{
    public string defaultSurfaceTag = "grass";  // 기본 표면 태그 (소문자)
    public AK.Wwise.Switch grassSwitch;
    public AK.Wwise.Switch concreteSwitch;
    public AK.Wwise.Event footstepEvent;  // 실행할 Wwise 이벤트

    // 애니메이션 이벤트에서 이 메서드를 호출
    public void PlayFootstepSound()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 1.5f);
        //Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.red, 1f);

        string surfaceTag = defaultSurfaceTag;

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                //Debug.Log("Raycast hit: " + hit.collider.name + ", tag: " + hit.collider.tag);
                string tag = hit.collider.tag.ToLower();

                if (tag == "grass" || tag == "concrete")
                {
                    surfaceTag = tag;
                    break; // 우선순위: 가장 먼저 발견된 유효 태그 사용
                }
            }
        }
        else
        {
            Debug.Log("Raycast didn't hit anything.");
        }

        // 태그에 따라 스위치 설정
        switch (surfaceTag)
        {
            case "grass":
                Debug.Log("grass");
                grassSwitch.SetValue(gameObject);
                break;
            case "concrete":
                Debug.Log("concrete");
                concreteSwitch.SetValue(gameObject);
                break;
            default:
                Debug.Log("Unknown surface tag: " + surfaceTag + " - using default");
                grassSwitch.SetValue(gameObject);
                break;
        }

        // 풋스텝 이벤트 실행
        if (footstepEvent != null)
        {
            footstepEvent.Post(gameObject);
        }
    }
}
