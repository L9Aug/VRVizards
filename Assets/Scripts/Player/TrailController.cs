using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FSM;
using Condition;

[RequireComponent(typeof(LocalTrailRenderer))]
public class TrailController : MonoBehaviour
{

    StateMachine SM;
    LocalTrailRenderer TR;
    public Transform HMD;
    public Camera orthoCam;
    bool isDrawing;
    public Texture2D capturedImage;    

    // Use this for initialization
    void Start()
    {
        TR = GetComponent<LocalTrailRenderer>();
        InitStateMachine();
    }

    void InitStateMachine()
    {
        BoolCondition startDrawingCon = new BoolCondition(GetIsDrawing);
        NotCondition endDrawingCon = new NotCondition(startDrawingCon);

        Transition start = new Transition("Start Drawing", startDrawingCon, new List<Action> { PlaceCamera, EnableTrail });
        Transition end = new Transition("Finish Drawing", endDrawingCon, new List<Action> { Snapshot, DisableTrail });

        State idle = new State("Idle", new List<Transition> { start }, new List<Action> {  }, new List<Action> { }, new List<Action> { });
        State drawing = new State("Drawing", new List<Transition> { end }, new List<Action> { }, new List<Action> { }, new List<Action> { });

        start.SetTargetState(drawing);
        end.SetTargetState(idle);

        SM = new StateMachine(idle, new List<State> { idle, drawing });
        SM.InitMachine();
    }

    void PlaceCamera()
    {
        orthoCam.transform.position = transform.position + (transform.forward * -3);
        orthoCam.transform.LookAt(transform);
        orthoCam.gameObject.SetActive(true);
    }

    void Snapshot()
    {
        StartCoroutine("TakeSnapshot");
    }

    IEnumerator TakeSnapshot()
    {
        PositionCamera();
        yield return new WaitForEndOfFrame();
        capturedImage = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        RenderTexture.active = orthoCam.targetTexture;
        orthoCam.Render();
        capturedImage.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        capturedImage.Apply();
        byte[] bytes = capturedImage.EncodeToPNG();
        Destroy(capturedImage);
        File.WriteAllBytes("D:/up690813/VRVizards/VRVizards/TrainingImages/Test.png", bytes );
        Debug.Log("Written to file");
        orthoCam.gameObject.SetActive(false);
    }

    void PositionCamera()
    {
        orthoCam.orthographicSize = TR.GetImageSize();
        orthoCam.transform.position = HMD.position;
        orthoCam.transform.LookAt(TR.GetImageCenter());
    }

    bool GetIsDrawing()
    {
        return isDrawing;
    }

    void EnableTrail()
    {
        TR.enabled = true;
    }

    void DisableTrail()
    {
        TR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        SM.SMUpdate();
    }

    public void SetDrawing(bool set)
    {
        isDrawing = set;
    }

}
