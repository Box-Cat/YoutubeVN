using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public int length;
    public LineRenderer lineRend;
    private Vector3[] segmentPoses;
    private Vector3[] segmentV;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    void Awake()
    {
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];

        if (targetDir != null)
        {
            for (int i = 0; i < length; i++)
            {
                segmentPoses[i] = targetDir.position;
                segmentPoses[i].z = 0f; 
            }
        }
    }

    void Start()
    {
        lineRend.positionCount = length;
        lineRend.SetPositions(segmentPoses);
    }

    void Update()
    {
        segmentPoses[0] = targetDir.position;
        segmentPoses[0].z = 0f;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            // 방향을 현재 촉수 기준으로 계산
            Vector3 direction = (segmentPoses[i - 1] - segmentPoses[i]).normalized;

            // 자연스럽게 따라오도록 target 계산
            Vector3 targetPos = segmentPoses[i - 1] + direction * targetDist;

            // 부드럽게 따라오게 만듦
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);
            segmentPoses[i].z = 0f;
        }

        lineRend.SetPositions(segmentPoses);
    }

}
