using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//モンスターを召還するメイン処理
public class Shoukan : MonoBehaviour
{
	//プレファブを格納するダイアログ
	public GameObject[] prefabs = new GameObject[4];

	//ダイアログの要素数
	//0:スライム 1:ゴブリン 3:罠 4:空のオブジェクトを指定することを前提としている
	int arrayNumber = 3;

	//召還者（マモナー）のMP
	int mamonerMp = 1000;

	//MP消費量
	int cost;


	//スライムを選択
	public void Srime()
	{
		cost = 5;
		//arrayNumberの設定
		arrayNumber = 0;

	}
	//ゴブリンを選択
	public void Goburin()
	{
		cost = 10;

		arrayNumber = 1;

	}
	//罠を選択
	public void wana()
	{
		cost = 10;

		arrayNumber = 2;

	}
	//
	public void cansel()
	{


		arrayNumber = 3;

	}



	//気絶時間の設定
	const float Stun = 0.5f;

	//回復時間の設定
	float recoverTime = 0.0f;

	Vector3 moveDirection = Vector3.zero;

	Animator animator;

	//気絶判定 仮実装したが、不要となった機能
	public bool IsStan()
	{
		return recoverTime > 0.0f;
	}



	private void Update()
	{
		//PointerEventData pointer = new PointerEventData(EventSystem.current);
		//変更する
		//pointer.position = Input.mousePosition;
		//List<RaycastResult> result = new List<RaycastResult>();
		//EventSystem.current.RaycastAll(pointer, result);
		//boolean eleaFlag = true;
		//if (raycastResult.gameObject.name != "UI"){flag = ture;}else{flag = false}

		//TODO もし選択エリアでない場合の分岐を追加
		//MPがから０でない場合、左クリックした位置に召還する
		if (mamonerMp != 0 && Input.GetMouseButtonDown(1) )
		{
			//TODO
			//プレイヤーの現在位置　後々変更。スマホでタップした位置に変える。
			Vector3 syoukanPosision = transform.position;
			//Vector3 mousePos = Input.mousePosition;
			//mousePos.z = 10.0f;
			//Vector3 syoukanPosision = Camera.main.ScreenToWorldPoint(mousePos);

			//タップした位置の座標
			//Vector3 touchPosition = touch.position;
			//Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

			//マウスの位置
			//Vector3 syoukanPosision = Input.mousePosition;

			//召還処理
			Instantiate(prefabs[arrayNumber], syoukanPosision, Quaternion.identity);

			//召還コスト分MPを差し引く
			mamonerMp -= cost;
		}

		//気絶時の行動　仮実装したが、不要となった機能
		if (IsStan())
		{
			//動きを止める　復帰のカウントを進める
			moveDirection.x = 0.0f;
			moveDirection.z = 0.0f;
			recoverTime -= Time.deltaTime;

		}
	}

	//罠に当たった時の判定　不要となった機能
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//すでに気絶時は入力キャンセル
		if (IsStan()) return;

		//気絶状態のセット
		recoverTime = Stun;
		//アニメーションがある場合
		animator.SetTrigger("damage");

		//一度当たった罠は削除
		if (hit.gameObject.tag == "Wana")
		{
			Destroy(hit.gameObject);
		}

	}
}
