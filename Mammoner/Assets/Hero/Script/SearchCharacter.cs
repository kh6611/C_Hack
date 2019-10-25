using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacter : MonoBehaviour
{
    private MoveHero moveHero;
    // Start is called before the first frame update
    void Start()
    {
        moveHero = GetComponentInParent<MoveHero>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Monster"){
            //　敵キャラクターの状態を取得
            MoveHero.HeroState state = moveHero.GetState();
            if(state != MoveHero.HeroState.Chase)
            {
                moveHero.SetState(MoveHero.HeroState.Chase, col.transform);
            }
        }
    }
}
