# << 포트폴리오 - BubblePang >>
## 목표
  * **포코팡의 게임 방식을 모방한 모작 게임을 만드는 것**
  * **유니티의 여러 기능을 적극적으로 사용해 개발하는 것**

## 기능 설명 및 게임 방식
### ※ 화면 구성 및 조작법
  * 게임 화면은 크게 2개로 타이틀 화면, 메인 화면으로 구성된다.
  * 화면별 조작법
    * 타이틀 화면
      * Start 버튼 클릭시 게임이 시작 되며 메인 화면으로 전환
      * Exit 버튼 클릭시 게임이 종료
    * 메인 화면
      * 게임 시작시 3초간 조작이 불가능
      * 화면의 블록을 한붓 그리기 식으로 드래그하면 블록이 연결됨
      * 시간이 종료 되면 조작이 불가능해지며 결과 팝업창이 출력
      * 팝업창의 Home 버튼 클릭시 타이틀 화면으로 넘어가며 게임이 재시작
  * 각 화면은 아래의 그림과 같이 구성되어 있다.
  <p align="center">
    <img src="https://user-images.githubusercontent.com/75964372/172511987-fce0c19b-1950-4695-86c7-f2b2fa5da78c.png">
    <img src="https://user-images.githubusercontent.com/75964372/172512249-5b024fcd-347d-44b8-816c-e3ca9e2e0654.png">
  </p>

### ※ 게임 방식
  1. 화면의 블록의 배치를 보고 같은 블록을 연결
  2. 연결된 블록의 개수에 따라 아래의 결과가 발생  
      |개수|결과|
      |---|---|
      |3개 미만|아무 일도 일어나지 않고 블록 연결 해제|
      |3개 이상|연결된 블록이 제거 되고 그 자리에 **버블** 생성|
      |8개 이상|연결된 맨 마지막 블록의 자리에 랜덤 아이템 생성|
  3. 빈자리 위의 블록은 아래로 낙하해 빈자리를 채움
  4. 낙하 후의 빈칸에 새로운 블록을 생성
  5. 제한 시간(**90초**) 내에 **1~4**를 최대한 반복해서 고득점을 목표
### ※ 게임 요소
  * 블록 및 아이템
    * 블록은 게임에서 플레이어가 연결해야하는 오브젝트로, 색상으로 구분된다.
      <p align="center">
        <img src="https://user-images.githubusercontent.com/75964372/179955581-8ff51c57-e838-4bf2-aad7-70814827ac38.png" align="center">
      </p>  
    * 아이템은 블록이면서 연결없이 단독으로 특수효과를 발생시키는 오브젝트이다.
    * 아이템의 종류  
        |사진|이름|효과 설명|
        |---|---|---|
        |<img src="https://user-images.githubusercontent.com/75964372/179938604-135c0d5d-cfc2-40bc-9247-7281d69257fc.png">|폭탄 블록|아이템을 중심으로 2겹의 블록을 단번에 제거 후 **버블** 생성|
        |<img src="https://user-images.githubusercontent.com/75964372/179939154-82e90eaf-0104-4eab-b65d-659a98f4d678.png">|랜덤 블록|임의의 색상을 지정 후 필드의 같은 색상의 모든 블록을 제거 후 **버블** 생성|  
  * 버블 및 콤보
    * 적 오브젝트(**크라켄**)를 공격하는 오브젝트
    * 버블이 터질 때 게임 점수(스코어)가 가산되며 콤보의 값이 1 증가
    * 콤보는 값이 1 이상일 때 **3**초 내에 다른 버블이 터지지 않을 경우 값이 0으로 초기화
    * 콤보의 값은 스코어 가산 시 영향을 주며 그 값은 콤보 수에 비례해 증가
  * 적 오브젝트
    * 플레이어의 적인 오브젝트
    * 현재 크라켄 1종류가 존재(추후 추가 예정)
      <p align="right"><img src="https://user-images.githubusercontent.com/75964372/172537325-733c1cb8-5f06-4e68-90c7-a00fdeb5f4c5.gif"></p>
***
  
## 구현
### ※ 개발 전 고려 사향
  * 실무에서 역할을 분담하는 것을 고려해 혼자 개발하지만 개발 파트를 클라이언트 모듈와 UI 모듈로 나누어 개발한다.
    * 각 모듈를 각각의 네임스페이스로 묶어 다른 파트의 접근을 차단
    * Manager 클래스를 두어 각 파트의 사이에서 제어하고 중계하도록 설계
  * 최대한 클래스 간의 커플링을 가능한 줄이고 클래스가 맡는 책임을 한가지로 응집시키는 것을 고려한다.
  * 싱글톤 패턴의 사용을 가능한 최소화하고 UnityEvent나 직렬화 등으로 대체한다.

### ※ 클래스 다이어그램(간략화)
  <p align="">
    <img src="https://user-images.githubusercontent.com/75964372/172616063-f7985607-8872-4003-8b85-58edc19f3455.png">
  </p>
  
  |분류|설명|
  |---|---|
  |Object 모듈|게임의 객체들의 집합, 클라이언트 파트에 해당|
  |View 모듈|게임의 UI에 관련된 클래스들의 집합, UI 파트에 해당|
  |ScriptableObject|어느 포함되지 않고, SriptableObject를 상속받은 클래스와 그 클래스를 참조하는 클래스들의 집합|
  |GameManager|Object와 View, 두 네임스페이스의 사이에서 GameState에 따라 게임을 제어하는 클래스|
  |SoundManager|게임에 사용될 사운드들을 한 곳으로 모아 관리하고 필요할 때 사운드를 재생하는 클래스|

### ※ 주요 구현 설명
#### 블록 연결
  * 블록 연결 할 때 아래와 같은 예외 조건을 만족해야한다.
    1. 서로 인접한 블록
    2. 같은 색상의 블록
    3. 연결되어있지 않은 블록
    4. **위 3개의 조건을 모두 만족**
  * 위 조건 중 1의 조건을 검사하기 위해 알고리즘을 Cell의 Offset을 이용해 아래의 방법으로 구현해 보았다.
    * Offset(Col,Row)을 왼쪽 아래의 그림과 같은 규칙으로 각 Cell에 내부 변수로 부여한다.
    * Cell의 인접한 Cell과의 Offset 차는 아래와 규칙성(아랫 중간 그림)을 볼 수 있다.
      * 이때 Offset의 짝수, 홀수 열에 따라 규칙성이 다른 것을 확인 할 수 있다.
    * 열에 따른 규칙성를 없애기 위해 Offset을 3차원좌표값(오른쪽 아래 그림)으로 치환한다.
      * 치환 공식
        > q = Col    
        > r = row - (col - (col&1)) / 2    
        > s = -q-r    
    * 치환한 좌표값의 모든 차가 |1| 이하일 경우 인접한 Cell이므로 이를 조건문으로 Bool 값을 반환하도록 구현했다.
    <p align="center">
      <img src="https://user-images.githubusercontent.com/75964372/173723160-6d94c000-7f1b-40bc-9a7d-76bf745a9fdd.png" width="30%">
      <img src="https://user-images.githubusercontent.com/75964372/173723165-9e11c8c0-20ae-43b0-9631-f373f0ec1758.png" width="30%">
      <img src="https://user-images.githubusercontent.com/75964372/173723169-a2e5efe2-846a-4327-8f4e-d48ad65fdccd.png" width="30%">
    </p>
  * 코드
    > ``` C#
    > struct Offset {
    >     public int col, row;
    >     public Vector3 ToVector3()
    >     {
    >         float q = col;
    >         float r = row - (col + (col & 1)) / 2;
    >         return new Vector3(q, r, -q - r);
    >     }
    > }
    > class Cell : MonoBehaviour {
    >     public Offset offset { get; set; }
    >     public bool IsNeighbor(Cell cell)
    >     {
    >         Vector3 delta = cell.offset.ToVector3() - offset.ToVector3();
    >         Vector3 abs = new Vector3(Mathf.Abs(delta.x), Mathf.Abs(delta.y), Mathf.Abs(delta.z));
    >         return Mathf.Max(abs.x, abs.y, abs.z) < 2;
    >     }
    >     ...
    > }
    > ```
    
#### 오브젝트 풀
  * Block과 Bubble 클래스는 게임 특성상 동일한 오브젝트를 게임 진행 중 빈번한 생성과 파괴가 필요하다.
  * 메모리와 CPU 사용량을 줄이고 게임을 최적화하기 위해 오브젝트 풀링 기법을 적용하여 구현해 보았다.
  * Block은 BlockPool 클래스에서, Bubble은 BubbleShooter 클래스에서 오브젝트 풀을 관리하도록 설계하였다.
  * 각 오브젝트는 프리팹으로 에셋화해 직렬화하였고 게임 시작 시 Board의 Cell 개수의 2배의 오브젝트를 미리 생성한다.
  * 각 오브젝트들은 풀에서 꺼내질 때 활성화되고, 다시 반환됬을 때 비활성화된다.
    > ```C#
    > public class BlockPool : MonoBehaviour
    > {
    >     [SerializeField] private Block prefab;
    >     private Queue<Block> queue = new Queue<Block>();
    >     public void Init(int size)
    >     {
    >         for (int i = 0; i < size*2; ++i)
    >         {
    >             Block temp = Instantiate(prefab);
    >             temp.SetReturn(this);
    >             Enpool(temp);
    >         }
    >     }
    >     public void Enpool(Block block)
    >     {
    >         if (block != null)
    >         {
    >             block.transform.SetParent(transform, false);
    >             block.gameObject.SetActive(false);
    >             block.index = Random.Range(0, 4);
    >             queue.Enqueue(block);
    >         }
    >     }
    >     public Block Depool()
    >     {
    >         Block temp = queue.Peek();
    >         temp.gameObject.SetActive(true);
    >         return queue.Dequeue();
    >     }
    >     ...
    > }
    > ```

#### 모듈 간의 참조
  * 현재 UI와 클라이언트 모듈로 서로 나눠져 있지만 아래와 같은 경우에 서로 통신이 필요하다.
    1. UI는 화면 출력에 필요한 변수 값을 클라이언트에 요구
        * 남은 시간과 점수 출력하기 위한 값 요구 등의 경우들
    2. UI 이벤트에 따른 클라이언트 쪽 처리
        * 메인화면에서 State 버튼 클릭 시 Game State 변화 등의 경우들
    3. 클라이언트의 오브젝트 간에 상호작용에 따라 UI의 변경
        * 제한 시간이 오버 됬을때 게임을 멈추고 결과창을 팝업 등의 경우들
  * 이를 **ScriptableObject**와 **GameManager**를 통해 구현했다.
  * **ScriptableObject**
    * 1의 경우 타 모듈의 변수값을 직접적으로 참조하면 모듈간의 분리나 독립성이 약해지는 문제가 발생한다.
    * 이를 위해 ScpriptableObject를 상속해 FloatVariable 클래스를 정의하고 참조 될 변수들을 미리 에셋화하여 저장해 두었다.
    * 그 후 각 모듈에서 필요한 곳에서 FloatVariable을 참조하고 Unity의 인스펙터 창에서 에셋화한 변수를 직렬화해 참조
    * FloatVariable 클래스를 참조해 변수값을 공유할 뿐 각 모듈은 서로를 알지 못한다.
    <p align="">
      <img src="https://user-images.githubusercontent.com/75964372/179874168-70d02571-abf5-4a7d-8aea-f709a9b9591b.png" width="40%">
      <img src="https://user-images.githubusercontent.com/75964372/179874332-e29c5e20-7714-4d0c-9576-d8df95c38424.png" width="40%">
    </p>
  * **GameManager**
    * 모듈 간에 직접 함수를 호출 하기보단 요청을 받아 대신 함수를 호출하도록 GameManager 클래스를 설계했다. 
      * 단 GameManager를 거쳐가는 만큼 과정은 길어진다.
    * 함수 호출 뿐만 아니라 중앙에서 각 모듈을 관리하기 하도록 구현했다.
      * 모듈의 밖에서 Game State에 따라 Unity Update 함수에서 모듈을 제어한다.
    * 각 모듈은 GameManager의 함수만 호출하므로 각 모듈간에 참조를 존재하지 않는다.
      * 단 모듈이 많아질수록 GameManager의 역할이 커지고 다른 모듈과의 결합이 많아져 유지보수가 힘들어진다.
  
#### Cell 클래스의 역할 분할
  * Cell 클래스는 설계시 크게 아래의 역할을 가졌었다.
    1. 블록을 담아두고 그 블록을 관리하는 것
    2. 유저의 입력에 반응해 상호작용을 하는 것
  * 하나의 클래스는 하나의 역할만을 가지며 한가지만의 책임을 지는게 좋으므로 클래스를 둘로 나눠 책임을 분산시켰다.
    * CellEventListener 클래스는 유저의 입력 이벤트를 Cell 클래스의 함수를 호출해 처리
    * Cell 클래스는 Block의 저장, 반환 등을 처리
  * CellEventListener 클래스는 Cell 클래스를 존재를 알지만 Cell 클래스는 CellEventListener 클래스를 알지 못한다.
    * 단일 연관 관계 통해 최대한 결합도를 낮추도록 고려했다.
  > ```C#
  > public class CellEventListener : MonoBehaviour
  > {
  >     [SerializeField] private Cell targetCell;
  >     private static bool isSlid = false;
  >
  >     private void OnMouseEnter()
  >     {
  >         if (isSlid)
  >         {
  >             targetCell.SetLink();
  >         }
  >     }
  >     ...
  > }
  > public class Cell : MonoBehaviour
  > {
  >     ...
  >     public void SetLink() { ... }
  >     ...
  > }
  > ```
  
#### SoundManager 클래스
  * 효과음의 특성상 여러 상황에서 재생되어지므로 여러 클래스에서 SoundManager로의 접근성이 필요했다.
  * 이러한 특징 떄문에 처음에 싱글톤 패턴을 고려했으나 Unity Event와 ScriptableObject로 옵저버패턴을 응용해 구현해보았다.
  * 기본적으로 Scean에 올라가 있는 GameObject들은 UnityEvent를 사용해 SoundManager를 직렬화한 후 함수를 호출하도록 설계하였다.
    * 대채로 UI의 효과음을 이 방식으로 구현했다.
  * 그 외에 프리팹으로 에셋화된 Bubble 같은 오브젝트에선 바로 Scean에 올라가 있는 SoundManager를 직렬화 할 수 없다. 그래서 ScriptableObject로 옵저버 패턴을 응용해 게임 시작시       SoundManager의 EventListener가 Event를 구독하게해 Event 발생시 SoundManager의 함수를 호출하도록 구현했다.
    <p align="center"><img src="https://user-images.githubusercontent.com/75964372/180136339-2037e198-45df-4b0c-88a1-d74586f97a6f.png"></p>

#### Scean 구성
  <p align="center"><img src="https://user-images.githubusercontent.com/75964372/180141222-79cdf103-addf-4bea-b54a-f3c2da51e783.gif"></p>    
  
  * 본 게임의 크기가 작고 그래픽이 2D라 가벼운 점을 고려해 Scean을 나누지 않고 하나의 Scean에 카메라 워크를 통해 화면이 전환 되는 것 처럼 설계했다.
  * Scean에 미리 모든 화면을 구성해 놓고 게임 시작시 모두 비활성화 시켜 놓고 GameState에 따라 필요한 화면만 활성화 되게 구현했다.
  * 위 처럼 화면 전환 시 다음 화면을 먼저 활성화 시킨 후 카메라를 이동시켜 화면 전환 효과를 주었고 카메라 이동이 완료되면 이전 화면은 비활성화 된다.
  
  
## 시연


## 결과

