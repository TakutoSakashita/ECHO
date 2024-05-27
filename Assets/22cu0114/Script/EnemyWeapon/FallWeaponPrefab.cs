using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWeaponPrefab : WeaponBaseClass
{
    private Rigidbody rigidbody;

    private Vector3 WeaponSpeed = new Vector3(0.0f, 0.0f, 0.0f);
    
    private bool Changetremble;     // 震える方向の切り替えフラグ
     
    private bool Stoptremble;       // 震えを止めるフラグ
     
    private readonly float ChangeTrembleNum = 0.03f;    // 震える方向を変えるまでの時間
    private readonly float ResetTrembleCount = 0.0f;    // 震える方向を変えるまでの時間カウント

    private float ChangeAfterTrembleCount;              // 震える方向を変えた後の経過時間
    private readonly float TrembleDistance = 0.1f;      // 震える距離

    private readonly int ColorBit = 255; 
    private readonly byte DisappearDeleteSpeed = 7;
    private readonly int DisappearDeleteTime = 30;
    private readonly float DeleteStartDelay = 1.5f;
    private readonly float DeleteSpeedDelay = 0.01f;

    private readonly float NextWeaponInterval = 0.3f;　// 次の武器を落とすまでの間隔
    private readonly float MoveWeaponDelay = 4.5f;     // 武器を落とし始めるまでの時間

    private static readonly string StopObjectTag = "floor"; 

    private bool OnHitStop = false;
    private readonly float HitStopTime = 0.015f;

    private static readonly string MusicName = "BossSlash";

    MeshRenderer mesh;              // 自分自身のメッシュ

    private void Update()
    {
        _trembleWeapon();
    }

    void Delete()
    {
        Destroy(this.gameObject);
    }

    public override void _MoveWeapon(float Speed)
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        WeaponSpeed.y = -Speed;

        StartCoroutine(MoveDelay());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveDelay()
    {
        float DelayTime = AdjustmentDelay();

        yield return new WaitForSeconds(DelayTime);

        SoundManager.Instance.Play(MusicName);

        Stoptremble = true;
        rigidbody.velocity = WeaponSpeed;

        mesh = GetComponent<MeshRenderer>();
        StartCoroutine(Transparent());
    }

    /// <summary>
    /// 武器が降るまでのDelay時間を調整する
    /// </summary>
    /// <returns></returns>
    float AdjustmentDelay()
    {
        float DelayTime = (float)WeaponNumber;
        DelayTime = DelayTime * NextWeaponInterval;
        DelayTime += MoveWeaponDelay;

        return DelayTime;
    }

    /// <summary>
    /// 武器を揺らす
    /// </summary>
    void _trembleWeapon()
    {
        ChangeAfterTrembleCount += Time.deltaTime;

        if (ChangeAfterTrembleCount <= ChangeTrembleNum) return;

        ChangeAfterTrembleCount = ResetTrembleCount;

        if (Stoptremble) return;

        Vector3 objectPos = this.gameObject.transform.position;

        if (Changetremble)
        {
            objectPos.x -= TrembleDistance;
            Changetremble = false;
        }
        else
        {
            objectPos.x += TrembleDistance;
            Changetremble = true;
        }

        this.gameObject.transform.position = objectPos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(StopObjectTag) == true)
        {
            rigidbody.velocity = SpeedReSet;

            if (OnHitStop) return;

            OnHitStop = true;
            HitStopManager.Instance.StartHitStop(HitStopTime);
        }
    }

    /// <summary>
    /// 武器を透明化
    /// </summary>
    /// <returns></returns>
    IEnumerator Transparent()
    {
        yield return new WaitForSeconds(DeleteStartDelay);

        for (int i = 0; i < (ColorBit / DisappearDeleteSpeed); i++)
        {
            mesh.material.color = mesh.material.color - new Color32(0, 0, 0, DisappearDeleteSpeed);
            yield return new WaitForSeconds(DeleteSpeedDelay);

            if(i == DisappearDeleteTime)
            {
                Delete();
            }
        }
    }
}

