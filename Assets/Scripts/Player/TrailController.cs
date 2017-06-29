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
    public GameObject VisibleTrail;

    public string FolderPath;
    public string ImageName;

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

        Transition start = new Transition("Start Drawing", startDrawingCon, new List<Action> { EnableTrail });
        Transition end = new Transition("Finish Drawing", endDrawingCon, new List<Action> { Snapshot });

        State idle = new State("Idle", new List<Transition> { start }, new List<Action> {  }, new List<Action> { }, new List<Action> { });
        State drawing = new State("Drawing", new List<Transition> { end }, new List<Action> { }, new List<Action> { }, new List<Action> { });

        start.SetTargetState(drawing);
        end.SetTargetState(idle);

        SM = new StateMachine(idle, new List<State> { idle, drawing });
        SM.InitMachine();
    }

    void Snapshot()
    {
        StartCoroutine(TakeSnapshot());
    }

    IEnumerator TakeSnapshot()
    {
        PositionCamera();
        orthoCam.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        capturedImage = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        RenderTexture.active = orthoCam.targetTexture;
        orthoCam.Render();
        capturedImage.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        capturedImage.Apply();
        byte[] bytes = capturedImage.EncodeToPNG();
        Destroy(capturedImage);
        File.WriteAllBytes(GetFilePath(), bytes );
        Debug.Log("Written to file");
        orthoCam.gameObject.SetActive(false);
        DisableTrail();
    }

    string GetFilePath()
    {
        int Iterations = 0;
        string RetString = "D:/up690813/VRVizards/VRVizards/TrainingImages/" + FolderPath + "/" + ImageName + Iterations + ".png";
        while (File.Exists(RetString))
        {
            ++Iterations;
            RetString = "D:/up690813/VRVizards/VRVizards/TrainingImages/" + FolderPath + "/" + ImageName + Iterations + ".png";
        }
        return RetString;
    }

    void PositionCamera()
    {
        orthoCam.orthographicSize = TR.GetImageSize();
        orthoCam.transform.position = (TR.GetPointNormal().normalized * 3) + TR.Target.position;
        orthoCam.transform.LookAt(TR.GetImageCenter()+transform.parent.position);
    }

    bool GetIsDrawing()
    {
        return isDrawing;
    }

    void EnableTrail()
    {
        TR.enabled = true;
        VisibleTrail.SetActive(true);
    }

    void DisableTrail()
    {
        TR.enabled = false;
        VisibleTrail.SetActive(false);
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
