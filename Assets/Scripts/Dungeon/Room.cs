using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up 1 -Down 2 - Right 3- Left
    public GameObject[] doors;
    public Transform[] positionSpawns;
    public int numberGrid;
    public bool deleteWhenHasCreate=false;

    public GameObject baseMiniMapIcon;
    public GameObject miniMapIconActive;

    protected BoxCollider2D boxCollider2D;
    public bool battleActive = false;

    [Header("Enemy Spawning")]
    public List<EnemyWave> enemyWaves = new List<EnemyWave>();
    public float spawnAreaPadding = 1f; 
    public float obstacleCheckRadius = 0.5f;
    public int numberNomarEnemies;
    public int numberSpecialEnemies;
    public GameObject magicSummon;
    public Transform enemyHolder;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private int currentWave = 0;



    private LayerMask interactableLayer;
    private bool[] status;
    private BossRoom bossRoom;
    public void Awake() {
        boxCollider2D=GetComponent<BoxCollider2D>();
        bossRoom=GetComponent<BossRoom>();
        
    }
    public virtual void Start() {

        interactableLayer=LayerMask.GetMask("Player Foot");
    }

    public void UpdateRoom(bool[] _status){
        status=_status;
        for(int i=0;i<_status.Length;i++){
            doors[i].SetActive(_status[i]);
            walls[i].SetActive(!_status[i]);
        }
    }
    public void ActivateConnectedRooms(){
        for(int i=0;i<doors.Length;i++){
            if(doors[i].activeSelf){
                doors[i].GetComponent<Door>().ActivateConnectedRoom();
            }
        }
    }
    public void ResetMinimapIcon(){
        if(miniMapIconActive==null||baseMiniMapIcon==null) return;
        baseMiniMapIcon.SetActive(true);
        miniMapIconActive.SetActive(false);
    }
    public void ActiveMiniMapIcon(){
        if(miniMapIconActive==null||baseMiniMapIcon==null) return;
        miniMapIconActive.SetActive(true);
        baseMiniMapIcon.SetActive(false);
    }
    public void ActivateEnemyWaitingState(){
        if(spawnedEnemies.Count==0){
            Debug.Log("No enemies to activate");
            return;
        }
        for(int i=0;i<spawnedEnemies.Count;i++){
            Enemy spawnedEnemy=spawnedEnemies[i].GetComponent<Enemy>();
            spawnedEnemy.isWaiting=true;
            spawnedEnemies[i].GetComponent<EnemyStats>().MakeInvinsable(true);
        }
    }
    public void ActivateEnemiesForBattle(){
        for(int i=0;i<spawnedEnemies.Count;i++){
            Enemy spawnedEnemy=spawnedEnemies[i].GetComponent<Enemy>();
            spawnedEnemy.isWaiting=false;
            spawnedEnemy.charaterStats.MakeInvinsable(false);
        }
    }
    private IEnumerator StaggeredEnemyActivation() {
    for (int i = 0; i < spawnedEnemies.Count; i++) {
        Enemy spawnedEnemy = spawnedEnemies[i].GetComponent<Enemy>();
        spawnedEnemy.isWaiting = false;
        spawnedEnemy.charaterStats.MakeInvinsable(false);
        
        yield return new WaitForSeconds(0.1f);
    }
}
    public void SetupBattleEnvironment(){
        for(int i=0;i<doors.Length;i++){
            if(doors[i].activeSelf){
                walls[i].GetComponent<WallDoor>().OpenWallAnimation();
            }
        }
    }
    public void UnlockDoorsPostBattle(){
        for(int i=0;i<status.Length;i++){
            if(status[i]==true){
                walls[i].GetComponent<WallDoor>().CloseWallAnimation();
            }

        }
    }
    public virtual void OnTriggerEnter2D(Collider2D other) {
        if(bossRoom?.isBossDeath==false){
            if(((1 << other.gameObject.layer) & interactableLayer) != 0){
                if(battleActive) return;
                SetupBattleEnvironment();
                UI.instance.ingameUI.SetMiniMapUI(false);
                battleActive=true;
                return;
            }
        }
        if (enemyWaves.Count != 0){
            if(((1 << other.gameObject.layer) & interactableLayer) != 0){
                if(battleActive) return;
                SetupBattleEnvironment();
                ActivateEnemiesForBattle();
                UI.instance.ingameUI.SetMiniMapUI(false);
                battleActive=true;
            }
        }
    }


    ///////////////////////////////////////////
    public void StartSpawningEnemies(){
        if (enemyWaves.Count == 0){
            Debug.Log("No enemy waves defined for this room");
            return;
        }
        currentWave = 0;
        spawnedEnemies.Clear();
        SpawnWave(currentWave,false);
        ActivateEnemyWaitingState();


    }
    public void SpawnWave(int _waveIndex,bool _useMagicSummon){
        Debug.Log("SpawnWave "+_waveIndex);
        EnemyWave wave = enemyWaves[_waveIndex];
        int totalNomarEnemies =0;
        foreach (var enemyData in wave.enemies)
        {
            totalNomarEnemies+=enemyData.count;
        }

        if(totalNomarEnemies<numberNomarEnemies){
            Debug.Log("Total nomar enemies in wave is less than maximum allowed");
            return;
        }
        currentWave++;
        for(int i=0;i<numberNomarEnemies;i++){
            Vector2 spawnPos = GetRandomSpawnPosition();
            GameObject randomEnemy = GetEnemyNomarFromWave(wave);
            if(_useMagicSummon){
                GameObject newEnemy = CreateEnemy(randomEnemy,spawnPos);
                newEnemy.gameObject.SetActive(false);
                GameObject newMagicSummon=Instantiate(magicSummon,spawnPos,Quaternion.identity);
                if(newEnemy!=null) Debug.Log("newEnemy is not null");
                newMagicSummon.GetComponent<MagicSummon>().OnSummon+=newEnemy.gameObject.SetActive;
            }else{

                CreateEnemy(randomEnemy,spawnPos);
            }
        }
        if(wave.specialEnemies.Count==0){
            Debug.Log("No special enemies in wave");
            return;
        }
        for(int i=0;i<numberSpecialEnemies;i++)
        {
            Vector2 spawnPos = GetRandomSpawnPosition();
            GameObject randomEnemy = GetEnemySpecialFromWave(wave);
            if(_useMagicSummon){
                GameObject newEnemy = CreateEnemy(randomEnemy,spawnPos);
                newEnemy.gameObject.SetActive(false);
                GameObject newMagicSummon=Instantiate(magicSummon,spawnPos,Quaternion.identity);
                newMagicSummon.GetComponent<MagicSummon>().OnSummon+=newEnemy.gameObject.SetActive;
            }else{

                CreateEnemy(randomEnemy,spawnPos);
            }
        }
        
    }


    public GameObject GetEnemyNomarFromWave(EnemyWave _wave){
        if(_wave.enemies.Count==0){
            Debug.Log("No enemies in wave");
            return null;
        }
        while(true){
            int randomIndex = Random.Range(0,_wave.enemies.Count);
            GameObject randomEnemy = _wave.enemies[randomIndex].enemyPrefab;
            if(randomEnemy!=null&&_wave.enemies[randomIndex].count>0){
                _wave.enemies[randomIndex].count--;
                return randomEnemy;
            }
        }
    }

    public GameObject GetEnemySpecialFromWave(EnemyWave _wave){
        if(_wave.specialEnemies.Count==0){
            Debug.Log("No special enemies in wave");
            return null;
        }
        while(true){
            GameObject randomEnemy = _wave.specialEnemies[Random.Range(0,_wave.specialEnemies.Count)];
            if(randomEnemy!=null){
                return randomEnemy;
            }
        }
    }

    public GameObject CreateEnemy(GameObject _enemyPrefab,Vector2 _position){
        GameObject enemy = Instantiate(_enemyPrefab, _position, Quaternion.identity);
        enemy.transform.parent = enemyHolder;
        spawnedEnemies.Add(enemy);
        
        // Add event listener for enemy death
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.onDeath += OnEnemyDeath;
        }

        
        return enemy;
    }
    public void SpawnBoss(){
        if(bossRoom!=null){
            GameObject newBoss = Instantiate(bossRoom.boss, transform.position, Quaternion.identity);
            newBoss.transform.parent = enemyHolder;
            newBoss.GetComponent<Enemy>().onDeath+=bossRoom.OnBossDeath;
            newBoss.GetComponent<Enemy>().onDeath+=OnBossDeath;

        }
    }
    private void OnBossDeath(Enemy _enemy){
        UnlockDoorsPostBattle();
        UI.instance.ingameUI.SetMiniMapUI(true);
    }
    private Vector2 GetRandomSpawnPosition()
    {
        if (boxCollider2D == null){
            Debug.LogWarning("BoxCollider2D not set");
            return Vector2.zero;
        }
        
        Bounds bounds = boxCollider2D.bounds;
        float minX = bounds.min.x + spawnAreaPadding;
        float maxX = bounds.max.x - spawnAreaPadding;
        float minY = bounds.min.y + spawnAreaPadding;
        float maxY = bounds.max.y - spawnAreaPadding;
        
        int maxAttempts = 30;
        
        for (int i = 0; i < maxAttempts; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector2 randomPos = new Vector2(randomX, randomY);
            
            Collider2D hit = Physics2D.OverlapCircle(randomPos, obstacleCheckRadius);
            if (hit == null || hit==boxCollider2D)
            {
                return randomPos;
            }
        }
        
        Debug.LogWarning("Could not find valid spawn position");
        return Vector2.zero;
    }

    private void OnEnemyDeath(Enemy _enemy)
    {
        spawnedEnemies.Remove(_enemy.gameObject);
        if (spawnedEnemies.Count == 0)
        {
            if(currentWave < enemyWaves.Count)
            {
                SpawnWave(currentWave,true);
            }
            else
            {
                UnlockDoorsPostBattle();
                UI.instance.ingameUI.SetMiniMapUI(true);
            }
        }
    }
}
