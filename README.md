### 스파르타 유니티 3D 프로젝트 과제

- 과제 관련 블로그 글: https://velog.io/@junebeom/TIL250311-UNITY-3D-%EA%B2%8C%EC%9E%84-%EA%B5%AC%ED%98%84-%EC%97%B0%EC%8A%B5

#### 폴더 구조

```
Assets/
├── Common
│   ├── Manager
│   ├── System
│   ├── Actor
│
├── Shared
│
├── Entities
│   ├── Player
│   ├── Enemy
│   ├── Obstacle
│   ├── Items
│
├── Resources
│   ├── ...
```
- 매니저: 싱글톤으로 관리될 요소들이며 글로벌 매니저 하위로 오디오, UI 등을 포함합니다.
- 시스템: 오브젝트 감지 기능, 네이게이션과 같이 게임 전반 시스템 기능을 담당합니다.
- 공유(shared): 폴더 구조를 기존의 Assets의 구조로 가져가여 공통으로 사용되는 요소들을 관리합니다.

#### 구현된 사항

> 조작 기능

<img width="743" alt="스크린샷 2025-03-11 오후 1 27 34" src="https://github.com/user-attachments/assets/3111ec01-10a2-40c1-86b4-3cdc4698397e" />

- 설명: 방향키 및 wasd로 캐릭터 조장
- 과정: 플레이어에 자식요소로 카메라를 추가하여 추적을 구현했습니다.(rigidBody의 movePosition을 통해 자연스러운 이동 구현)

---

> 시점 변환(1인칭 -> 3인칭)

<img width="742" alt="스크린샷 2025-03-11 오후 1 27 43" src="https://github.com/user-attachments/assets/e8f55409-4488-4e4a-b4e8-8aadcb5d9b04" />

- 설명: V키를 누르면 시점을 변경합니다. 
- 과정: InputAction으로 bool 값을 토글하여 localTransform과 localPosition을 변경했습니다.
3인칭 시점에서 카메라의 상하 움직임이 캐릭터의 초점을 벗어나는 현상이 있어 CameraArm 빈 오브젝트 안에 카메라를 넣어 수정했습니다.

---

> 레이 캐스팅을 통한 바닥 감지 또는 대상 감지

<img width="499" alt="스크린샷 2025-03-11 오후 1 38 36" src="https://github.com/user-attachments/assets/99620e8b-750a-4624-843e-20b29f1d2db0" />

- 트러블 슈팅 : 카메라의 상하 움직임과 플레이의 상하 움직임이 별개이기 때문에 오브젝트 감지 시 카메라를 기준으로 확인하도록 수정)

> 대상이 감지되는 모습
<img width="654" alt="스크린샷 2025-03-11 오후 1 54 12" src="https://github.com/user-attachments/assets/55407bad-afee-4498-bbdc-d8d3c15639d4" />

- 설명: 대상을 중앙의 크로스헤어와 일치시키면 인지합니다.
- 과정: objectInfo 컴포넌트를 부착한 게임 오브젝트인 경우 레이 캐스팅에게 감지 될 때, 팝업 UI 표시를 요청시킵니다.

> 오브젝트 상호 작용
<img width="782" alt="스크린샷 2025-03-11 오후 1 32 56" src="https://github.com/user-attachments/assets/379638bb-a19b-4383-8963-22404f46b1a4" />

- 설명: 점프대와 충돌한 경우 점핑 되거나, 운반체에서 플레이어가 같이 움직입니다.
- 과정: OnCollisionEnter 발생 시 Impuse 방식으로 대상을 점프 시키고 SetParent로 요소를 묶어 함께 움직이도록 했습니다.

---
> 각종 장애물

<img width="793" alt="스크린샷 2025-03-11 오후 1 22 30" src="https://github.com/user-attachments/assets/4b276603-d6eb-455d-bc62-2ba70a089cb4" />

- 설명: 사운드 감지가 플레이어와의 범위에 따라 변경됨
- 과정: 플레이어의 위치와 대상의 위치를 계산하여 볼륨 조절

---
> 네비게이션

<img width="530" alt="스크린샷 2025-03-11 오후 2 21 08" src="https://github.com/user-attachments/assets/375ae9ef-bcdd-4528-a4dc-8ab252c107d3" />

- 설명: 네비게이션 메쉬 적용
- 과정: 맵을 먼전 생성한 뒤 호출을 통해 베이킹 합니다.
