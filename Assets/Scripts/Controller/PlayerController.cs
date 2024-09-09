using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10f;

    //마우스 방식으로 이동 하기위한 불 변수
    bool _moveToDest = false;
    Vector3 _destPos;

    //UI_Button uiPopup;
    //float wait_run_ratio = 0;
    Animator anim;
    void Start()
    {
        //Managers.Input.KeyAction -= OnKeyboard;
        //Managers.Input.KeyAction += OnKeyboard;
        anim = GetComponent<Animator>();
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    Managers.UI.ClosePopupUI(uiPopup);
        //}
        for (int i = 0; i < 5; i++)
        {
            Managers.UI.ShowPopupUI<UI_Button>();
        }
        //uiPopup = Managers.UI.ShowPopupUI<UI_Button>();
        //Managers.UI.ShowSceneUI<UI_Inven>();
        //프리팹 폴더를 만들어서 UI_Button을 생성시킨다.
        Managers.Resources.Instantiate("UI/UI_Button");
    }
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
    }

    PlayerState _state = PlayerState.Idle;

    void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
    }

    void UpdateDie()
    {
        //아무것도 못하게
    }

    void UpdateMoving()
    {
        if (_moveToDest)
        {
            //방향
            Vector3 dir = _destPos - transform.position;

            //거리 distance
            if (dir.magnitude < 0.00001f)
            {
                _state = PlayerState.Idle;
                _moveToDest = false;
            }
            else
            {
                float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
                transform.position += dir.normalized * moveDist;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            }
            anim.SetFloat("speed", _speed);
        }
    }
    void UpdateIdle()
    {
        anim.SetFloat("speed", 0);
    }
    //if (_moveToDest)
    //{
    //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10f * Time.deltaTime);
    //    Animator anim = GetComponent<Animator>();
    //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
    //    anim.Play("RUN");
    //}
    //else
    //{
    //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10f * Time.deltaTime);
    //    Animator anim = GetComponent<Animator>();
    //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
    //    anim.Play("WAIT");
    //}

    void OnKeyboard()
    {
        //좌,우, 전,후 이동
        if (Input.GetKey(KeyCode.W))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            // == 보간
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);

            //방향을 정해줬으니 앞으로만 가게했는데..
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);

            //트렌스레이트(로컬좌표)라서 포지션으로 바꿔줌
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            // == 보간
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);

            //transform.Translate(Vector3.back * Time.deltaTime * _speed);

            //트렌스 레이트(로컬좌표)를 포지션으로 바꿔줌
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            // == 보간
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);

            //transform.Translate(Vector3.left *Time.deltaTime * _speed);

            //트렌스 레이트(로컬좌표)를 포지션으로 바꿔줌
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            // == 보간
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);

            //transform.Translate(Vector3.right * Time.deltaTime * _speed);

            //트렌스 레이트(로컬좌표)를 포지션으로 바꿔줌
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

        _moveToDest = false;//클릭 방식으로 이동 불가
    }


    void OnMouseClicked(Define.MouseEvent evt)
    {
        //Press일경우는 작동 안되게끔(그냥 임시로 처리할 수 있게..)
        //프레스 기능을 사용하고 싶다면 삭제
        if (evt != Define.MouseEvent.Click)
            return;
        if (_state == PlayerState.Die)
            return;


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.red, 1f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            _destPos = hit.point;//클릭된 지점을 목적지로 지정
            _state = PlayerState.Moving;
            _moveToDest = true; //클릭 방식으로 이동 가능 하게.
        }
    }
}
