using UnityEngine;
using System.Collections;

public class SSScrollBar : MonoBehaviour {



    [SerializeField, Header("選択中のステージ")]
    private int GiarNum = 1;

    public float Rot ;

    private GameObject _QB;//Questボード

    private Vector3 touchStartPos;//フリック用
    private Vector3 touchEndPos;//フリック用
    private float touchStartTime = 0;//フリック用
    private bool _STr = false;

    void Start ()
    {
        Rot = 0;
        _QB = GameObject.Find("QuestBoard");
    }

    void Update()
    {
        
        iTween.RotateTo(gameObject, iTween.Hash("z", Rot, "islocal", true, "time", 2));
        
        if (Input.GetKeyUp(KeyCode.Mouse0) && _STr == true)//フリック用タッチを話した時
        {
            touchEndPos = new Vector3(Input.mousePosition.x,
                                  Input.mousePosition.y,
                                  Input.mousePosition.z);
            print(touchEndPos.y);
            GetDirection();
            _STr = false;
        }
            touchStartTime += Time.deltaTime ;//フリック用時間
    }
    void GetDirection()//フリック判定
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;
        print(+directionY);
        if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (+ 30 < directionY && touchStartTime <= 3f)
            {
                if (GiarNum == 3)
                {
                    GiarNum = 1;
                    Rot += 120;
                }
                else { GiarNum++;
                    Rot += 120;
                }
                print("上フリックしたよ");
            }
            if ( - 30 > directionY && touchStartTime <= 3f)
            {
                if (GiarNum == 1)
                {
                    Rot -= 120;
                    GiarNum = 3;
                }
                else { GiarNum--;
                    Rot -= 120;
                }
                print("下フリックしたよ");
            }
            if (360 <= Rot || -360 >= Rot)
            {
                Rot = 0;
            }
        }
    }
    public void Touch()
    {
        touchStartTime = 0;
        touchStartPos = new Vector3(Input.mousePosition.x,
                                  Input.mousePosition.y,
                                  Input.mousePosition.z);
        print(touchStartPos.y);
        _STr = true;

    }

    public void StageSelectB()
    {
        int count = 0;
        foreach (Transform child in _QB.transform)
        {
            child.GetComponent<Mission>().ChangeText(GiarNum);
            count++;
        }
    }
}
