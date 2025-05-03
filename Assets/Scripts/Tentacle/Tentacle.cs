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

        if (targetDir == null) return;

        // 헤드 따라가게
        segmentPoses[0] = targetDir.position;
        segmentPoses[0].z = 0f;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            Vector3 direction = (segmentPoses[i - 1] - segmentPoses[i]).normalized;
            Vector3 targetPos = segmentPoses[i - 1] + direction * targetDist;

            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);
            segmentPoses[i].z = 0f;
        }

        lineRend.SetPositions(segmentPoses);

    }

}
