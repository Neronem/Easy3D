using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // 싱글톤 인스턴스를 저장할 변수
    private static CharacterManager _instance;

    // 외부에서 접근 가능한 싱글톤 인스턴스
    public static CharacterManager Instance
    {
        get
        {
            // 인스턴스가 없으면 새로 생성 (런타임 중 Null 오류 방지)
            if (_instance == null)
            {
                _instance = new GameObject("CharacerManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    // 플레이어 오브젝트를 참조하는 변수
    private Player _player;

    // 외부에서 Player에 접근하고 설정할 수 있도록 프로퍼티 제공
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()
    { // 싱글톤 패턴
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}