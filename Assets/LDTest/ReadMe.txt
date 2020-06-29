1. Table - EnemySpawnTable 엑셀파일에서 스폰정보 수정 후 Json파일로 변환(Plugin - setup 설치 후 추가된 엑셀 Json메뉴를 통해 가능)
2. Prefabs - TileCreator 로 바닥 생성, Prefabs - EnemyCreator 로 테이블에서 작성한 적 생성

Table - EnemySpawnTable.xlsx-----------------------
PattonIndex : 패턴 번호
EnemyType : 적 종류(현재까지 Melee, Boss만 존재)
Position : 생성 좌표
Rotation : 생성 시 시선 방향(360도 단위)
SpawnGroup : 적 생성 묶음 번호(같은 번호끼리 동시에 소환됨)
-------------------------------------------------------

Prefabs - EnemyCreator------------------------------
SpawnDataJson : 건드리지 말 것
Patton Index : 실행시킬 테이블에서 작성한 패턴 번호
BoxCollider : 이 영역 내로 캐릭터가 들어오면 패턴번호에 따라 적이 스폰되기 시작
EnemyCreator - Walls 하위오브젝트 : 패턴 실행시 활성화되는 벽
-------------------------------------------------------

Prefabs - TileCreator---------------------------------
TileX : 좌우로 깔리는 타일 개수 (타일 1개당 2m)
TileY : 앞뒤로 깔리는 타일 개수 (타일 1개당 2m)
Tile, TileCollider : 건드리지 말 것
-------------------------------------------------------