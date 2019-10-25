using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//罠一覧と魔物一覧をボタン一つで切り替える
public class ButtonPanel : MonoBehaviour
{
    //ボタン配列の初期化
	public GameObject[] buttons;

	//スイッチの切り替えの判定に使用する
	private int switchButtonFlag;

	public void SwitchButoon()
	{
		

		
		if (switchButtonFlag == 0)
		{
			//魔物一覧を表示

            //トラバサミを非表示
			buttons[0].SetActive(false);
            //スライムを表示
			buttons[1].SetActive(true);
			//ゴブリン
			//buttons[2].SetActive(false);

			//罠一覧切り替えフラグを持たせる
			switchButtonFlag += 1;

		}
		else{
            //罠一覧を表示

			//トラバサミを表示
			buttons[0].SetActive(true);
            //スライムを非表示
			buttons[1].SetActive(false);
			//ゴブリン
			//buttons[2].SetActive(true);
			//魔物ボタン一覧に切り替えフラグを持たせる
			switchButtonFlag = 0;
		}
	}

}



