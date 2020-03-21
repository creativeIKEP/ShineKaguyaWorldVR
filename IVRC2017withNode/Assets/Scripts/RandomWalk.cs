using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Video;

public class RandomWalk : MonoBehaviour
{
    public GameObject walkTarget;     //おじいさんの歩く先
    public GameObject camera;    //自分
    public AudioSource audio;     //BeetSound
    public AudioSource footStep;    //おじいさんの足音
    public AudioSource whatVoice;    //おじいさんの声
    public GameObject cameraTarget;     //おじいさんが近ずいてくるときの経由位置
    public GameObject plane;      //自分の視界を暗くしている板
    public GameObject shadetext;
    public Text shakePercent;
    public GameObject shakeImage;
    public GameObject walkTarget2;
    private VideoPlayer videoPlayer;
    public GameObject plane1;
    public GameObject plane2;
    public GameObject plane3;
    public GameObject plane4;
    public GameObject plane5;
    public GameObject Ax;
    public Text tex;

    private float HeadMove = 0;     //頭の移動距離(HeadTracking)
    private Vector3 deta;     //以前のUpdate()呼び出し時のカメラ座標
    private Animator animator;     //キャラクターのAnimator
    private NavMeshAgent agent;
    private float xpos, zpos;
    private float distanceData = 0;
    private Vector3 vectorData;
    private float time = 0;
    private Transform cameraTrans;
    private Rigidbody rigidbody;
    private CapsuleCollider collider;
    private bool turn = true;
    private WebSocketControl lightControl;
    private AirGunFight airgun;
    float time2 = 0;
    float alfa;
    bool coljudge=true;
    float dis;
    double shakeper=0;
    float totaldis=10;
    float suptime = 0;
    bool coming = false;
    private Collision col;
    int count = 0;
    bool isfinish = false;
    

    // Use this for initialization
    void Start()
    {
        videoPlayer = plane1.GetComponent<VideoPlayer>();
        shakeImage.transform.localScale = Vector3.zero;
        alfa = plane.GetComponent<MeshRenderer>().material.color.a;
        airgun = GetComponent<AirGunFight>();
        lightControl = GetComponent<WebSocketControl>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();     //characterのanimation取得
        deta = Vector3.zero;      //初期設定

        //walkTargetの初期位置決定
        float x = Random.Range(-15.0f, 15.0f);
        float z = Random.Range(0, 15.0f);
        //Debug.Log("X,Z: " + x.ToString("F2") + ", " + z.ToString("F2"));
        walkTarget.transform.position = new Vector3(x, 0, z);
        
        agent = GetComponent<NavMeshAgent>();     //NavMeshAgentの取得
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid) { 
        agent.destination = walkTarget.transform.position;     //歩行先の設定
    }
        vectorData = transform.position;     //characterの初期位置を格納
        cameraTrans = camera.transform;     //cameraの初期位置を格納
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 characterVector = transform.position;     //characterの座標を更新
        distanceData = Vector3.Distance(characterVector, vectorData);     //以前のcharacterの座標から現在の位置までの距離
        time += Time.deltaTime;     //経過時間の更新
        vectorData = characterVector;     //次回のUpdate()で使用するために現在のcharacter座標を格納
        //Debug.Log("" + distanceData.ToString("F2"));


        //事前準備
        Vector3 walkTargetVector = walkTarget.transform.position;
        Vector3 cameraVector = camera.transform.position;

        dis = Vector3.Distance(cameraVector, deta);
        HeadMove += dis;    //headTrackingDateの更新
        shakeper = HeadMove / (totaldis * 0.01f);
        if (shakeper > 100) { shakePercent.text =""; shakeImage.SetActive(false); }
        else {
            shakePercent.text = shakeper.ToString("F0") + "/100%";
            shakeImage.transform.localScale = new Vector3(0.05f * (float)shakeper, 0.05f * (float)shakeper, 1);
        }

        if (isfinish) { shadetext.SetActive(true); }
        else if (dis <= 0.001f && shakeper <= 100)
        {
            shadetext.SetActive(true);
        }
        else { shadetext.SetActive(false); }
        Debug.Log("HeadMoveDistance: " + shakeper.ToString("F2")+"%");
        deta = cameraVector;     //現在のカメラ座標を格納(次回のUpDate()でのHeadTrackingの更新に使用)

        //頭の動いた量によってLEDの光量を調整
        if (HeadMove >= (totaldis/4) && HeadMove < (totaldis/2))
        {
            lightControl.LigthOn(1);
            if (coljudge) { plane.GetComponent<MeshRenderer>().material.color = new Color(plane.GetComponent<MeshRenderer>().material.color.r + 0.16f, plane.GetComponent<MeshRenderer>().material.color.g + 0.16f, plane.GetComponent<MeshRenderer>().material.color.b, plane.GetComponent<MeshRenderer>().material.color.a); coljudge = !coljudge; }

        }
        else if (HeadMove >= (totaldis/2) && HeadMove < (totaldis*3/4))
        {
            lightControl.LigthOn(2);
            if (!coljudge) { plane.GetComponent<MeshRenderer>().material.color = new Color(plane.GetComponent<MeshRenderer>().material.color.r + 0.16f, plane.GetComponent<MeshRenderer>().material.color.g + 0.16f, plane.GetComponent<MeshRenderer>().material.color.b, plane.GetComponent<MeshRenderer>().material.color.a); coljudge = !coljudge; }
        }
        else if (HeadMove >= (totaldis*3/4) && HeadMove < totaldis)
        {
            lightControl.LigthOn(3);
            if (coljudge) { plane.GetComponent<MeshRenderer>().material.color = new Color(plane.GetComponent<MeshRenderer>().material.color.r + 0.16f, plane.GetComponent<MeshRenderer>().material.color.g + 0.16f, plane.GetComponent<MeshRenderer>().material.color.b, plane.GetComponent<MeshRenderer>().material.color.a); coljudge = !coljudge; }
        }
        else if (HeadMove >= totaldis)      //characterが自分に気づく条件
        {
            lightControl.LigthOn(4);
            count++;
            whatVoicePlay(count);
            if (count >= 2) { count = 2; }
            if (!coljudge) { plane.GetComponent<MeshRenderer>().material.color = new Color(plane.GetComponent<MeshRenderer>().material.color.r + 0.16f, plane.GetComponent<MeshRenderer>().material.color.g + 0.16f, plane.GetComponent<MeshRenderer>().material.color.b, plane.GetComponent<MeshRenderer>().material.color.a); coljudge = !coljudge; }
            if (Vector3.Distance(characterVector, cameraVector) >= 2.5)      //自分とcharacterに距離がまだあるなら
            {
                //				characterが竹に引っかかって動けなくなった時の処理
                /*if (time >= 1.0f)
                {    //一秒以上経過していて
                    if (distanceData < 0.01f)
                    {    //前回のUpdate()からの移動距離が0.03未満なら(=竹に引っかかってる可能性がある)
                        //SetRandomPosition();      //walkTargetの位置を変更
                       // transform.LookAt(walkTarget.transform);
                    }
                    else
                    {
                        //自分の方へ近づいてくる
                        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
                        {
                            agent.destination = camera.transform.position;
                            //agent.destination = cutpositionVector;
                        }
                        transform.LookAt(camera.transform);
                    }
                    time = 0;
                }*/

                coming = true;

                if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
                {
                    agent.destination = cameraTarget.transform.position;
                    

                    //agent.destination = cutpositionVector;
                }
                //transform.LookAt(camera.transform);
                transform.Rotate(cameraTarget.transform.position.x, 0, cameraTarget.transform.position.z);


                //心臓音はやく
                if (audio.pitch < 5.0f)
                {
                    audio.pitch += 0.01f;
                }

            }
            else     //characterが近づいたら(もしくは竹切り中)
            {

                //if (Vector3.Distance(characterVector, cutpositionVector) <= 0.5) { 

                //agent.isStopped = true;
                
                footStep.Stop();    //characterの足音stop
                animator.SetBool("isNotice", true);     //次のanimationに移行
                turn = false;
                if (audio.pitch < 5.0f)
                {
                    audio.pitch += 0.01f;
                }

                //cameraTarget.transform.position = new Vector3(camera.transform.position.x, 0, camera.transform.position.z);
                //rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                //transform.LookAt(cameraTarget.transform);
                transform.Rotate(cameraTarget.transform.position.x, 0, cameraTarget.transform.position.z);
                

                //agent.destination = new Vector3(100, 0, 100);

                //walkTarget.active = false;
                //collider.radius = 0;
                // }
                //else
                //{
                //    agent.destination=cutpositionVector;
                //}
            }
        }
        else     //まだ気づいてないなら
        {
            //characterが竹に引っかかって動けなくなった時の処理
            /*if (time >= 1.0f)
            {    //一秒以上経過していて
                if (distanceData < 0.03f)
                {    //前回のUpdate()からの移動距離が0.03未満なら(=竹に引っかかってる可能性がある)
                    SetRandomPosition();      //walkTargetの位置を変更
                }
                time = 0;
            }*/

            if (time > 15.0f) { SetRandomPosition(); time = 0; }
            /*else if (Vector3.Distance(characterVector, walkTargetVector) <= 0.5f)
            {     //characterがwalkTargetに到達したら
                SetRandomPosition();     //walkTargetの座標を更新
                
            }*/
            transform.LookAt(walkTarget.transform);
        }
        
    }

    void SetRandomPosition()
    {
        if (turn) { 
            float x = Random.Range(-15.0f, 15.0f);
            float z = Random.Range(0, 10.0f);
         Debug.Log("X,Z: " + x.ToString("F2") + ", " + z.ToString("F2"));
         walkTarget.transform.position = new Vector3(x, 0.0f, z);
         //if (agent.pathStatus != NavMeshPathStatus.PathInvalid) {
             agent.destination = walkTarget.transform.position;
         //}//目的地の設定し直し
        } else{
            walkTarget.transform.position = camera.transform.position;
        }

    }

    void OnApplicationQuit()
    {
        lightControl.LigthOn(0);
        airgun.TurnServo(0);
    }

    void finish()
    {
        /*lightControl.LigthOn(0);
        airgun.TurnServo(0);
        plane1.gameObject.SetActive(true);
        plane2.gameObject.SetActive(true);
        plane3.gameObject.SetActive(true);
        plane4.gameObject.SetActive(true);
        plane5.gameObject.SetActive(true);
        while (true)
        {
            if (videoPlayer.isPlaying == false)
            {*/
                //EditorApplication.isPlaying = false;
            /*}
        }*/
        
    }
    void alfazero()
    {
        plane.GetComponent<MeshRenderer>().material.color = new Color(plane.GetComponent<MeshRenderer>().material.color.r + 0.16f, plane.GetComponent<MeshRenderer>().material.color.g + 0.16f, plane.GetComponent<MeshRenderer>().material.color.b, 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.name=="bamboo 2 1(Clone)")
        {
            animator.SetBool("isBamboo", true);
            col = collision;
           /* for (;;)
            {
                bool check=false;
                check=checkBamboo(collision);
                if (check) { break; }
            }*/
        }
        else if (collision.gameObject==walkTarget)
        {
            SetRandomPosition();
        }
        else if (collision.gameObject == cameraTarget && coming)
        {
            cameraTarget.transform.position = Vector3.zero;
        }
    }

   void cutBamboo()
    {
        Destroy(col.gameObject);
    }

    void whatVoicePlay(int x)
    {
        if (x == 1)
        {
            whatVoice.Play();
        }
    }

    void AliveorDie()
    {
        /*if (Ax.transform.position.y >= camera.transform.position.y)
        {
            tex.text = "You are born!";
            shadetext.SetActive(true);
        }
        else
        {
            tex.text = "You Die!";
            shadetext.SetActive(true);
        }
        isfinish = true;*/
    }
}
