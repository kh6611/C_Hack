using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHero : MonoBehaviour
{
    

    public enum HeroState
    {
        Walk,
        Wait,
        Chase
    };

    [SerializeField]
    float speed = 0.05f;
    Vector2 inputAxis;
    //private Rigidbody2D _rig;
    //public Rigidbody2D Rig { get { return this._rig ? this._rig : this._rig = GetComponent<Rigidbody2D>(); } }
    Rigidbody2D rb;
    private CharacterController heroController;
    //　目的地
    private Vector3 destination;
    //　歩くスピード
    [SerializeField]
    private float walkSpeed = 1.0f;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;
    //　到着フラグ
    private bool arrived;
    //　SetPositionスクリプト
    //private SetPosition setPosition;
    //　待ち時間
    [SerializeField]
    private float waitTime = 5f;
    //　経過時間
    private float elapsedTime;
    // 敵の状態
    private HeroState state;
    //　プレイヤーTransform
    private Transform MamonoTransform;

    // Start is called before the first frame update
    void Start()
    {
        heroController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
        arrived = false;
        elapsedTime = 10f;
        SetState(HeroState.Walk);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state == HeroState.Walk || state == HeroState.Chase)
        {
            var v = new Vector2(-speed, rb.velocity.y);
            transform.Translate(v);
            //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == HeroState.Chase)
            {
                //setPosition.SetDestination(MamonoTransform.position);
                //プレイヤーの方向に向かって移動していく
                Vector2 diff = (MamonoTransform.position - transform.position); //プレイヤーと対照との差分を取得
                v = new Vector2((diff.x + 1) * speed, (diff.y - 2) * speed);//取得した座標に対して変数をかけてやると進む
                transform.Translate(v);
               
                if (diff.x < 5 && diff.y <5)
                {
                    //Debug.Log("diff" + diff);
                    SetState(HeroState.Wait);
                }
            }

        }
        else if (state == HeroState.Wait)
        {
            elapsedTime += Time.deltaTime;



            //　待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime)
            {
                SetState(HeroState.Walk);
            }
        }
    }
    //　敵キャラクターの状態変更メソッド
    public void SetState(HeroState tempState, Transform targetObj = null) {
        if (tempState == HeroState.Walk)
        {
            arrived = false;
            elapsedTime = 0f;
            state = tempState;
        }
        else if (tempState == HeroState.Chase)
        {
            state = tempState;
            //　待機状態から追いかける場合もあるのでOff
            arrived = false;
            //　追いかける対象をセット
            MamonoTransform = targetObj;
        }
        else if (tempState == HeroState.Wait)
        {
            elapsedTime = 0f;
            state = tempState;
            arrived = true;
            velocity = Vector2.zero;
            //Debug.Log("diff");
        }
    }
    //　敵キャラクターの状態取得メソッド
    public HeroState GetState()
    {
        return state;
    }
}