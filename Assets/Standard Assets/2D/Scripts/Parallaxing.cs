using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public List<Transform> backgrounds=new List<Transform>();   //存储所有背景图
    private List<float> parallaxScales=new List<float>();    //每张图片对应偏移量
    public float smoothing = 1f;

    private Transform cam;  
    private Vector3 previousCamPos;  //上一帧镜头位置

    void Awake()
    {
        cam = Camera.main.transform;
    }
    // Use this for initialization
    void Start () {
        backgrounds.ForEach(x => parallaxScales.Add(x.position.z * -1));   
    }

    // Update is called once per frame
    void Update () {
        for (int i=0;i<backgrounds.Count;i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX,backgrounds[i].position.y,backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;
    }
}
