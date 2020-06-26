using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.CustomFunctions;

namespace ProjectM.ePEa.LDSystem
{
    public class EnemyCreator : MonoBehaviour
    {
        [SerializeField] TextAsset m_spawnDataJson;
        [SerializeField] int m_pattonIndex;
        [SerializeField] GameObject m_Spawner;
        [SerializeField] GameObject m_Walls;

        SpawnData[] datas;
        bool m_isSpawn = false;

        int m_maxGroup;
        int m_nowGroup = 0;

        private void Awake()
        {
            datas = JsonHelper.FromJson<SpawnData>(m_spawnDataJson.text) as SpawnData[];
            m_maxGroup = GetMaxGroup(m_pattonIndex);
        }

        void Update()
        {
            if (m_isSpawn && m_nowGroup < m_maxGroup && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Spawner").Length == 0)
            {
                SpawnGroup(m_nowGroup);
                m_nowGroup++;
            }

            if (m_nowGroup >= m_maxGroup && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Spawner").Length == 0)
                m_Walls.SetActive(false);
        }

        int GetMaxGroup(int SpawnGroupIndex)
        {
            int num = 0;
            foreach(SpawnData data in datas)
            {
                if (data.PattonIndex == m_pattonIndex && data.SpawnGroup > num)
                    num = data.SpawnGroup;
            }

            return num + 1;
        }

        void SpawnGroup(int SpawnGroupIndex)
        {
            Dictionary<int, SpawnData> enemys = new Dictionary<int, SpawnData>();

            int num = 0;
            foreach(SpawnData data in datas)
            {
                if (data.PattonIndex == m_pattonIndex && data.SpawnGroup == SpawnGroupIndex)
                    enemys.Add(num++, data);
            }

            for(int i=0;i<enemys.Count;i++)
            {
                GameObject e = Instantiate(m_Spawner);
                e.transform.position = transform.position + new Vector3(enemys[i].Position[0], enemys[i].Position[1], enemys[i].Position[2]);
                e.transform.rotation = Quaternion.Euler(new Vector3(0.0f, enemys[i].Rotation, 0.0f));
                e.GetComponent<Spawner>().m_enemyName = enemys[i].EnemyType;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PlayerDamage")
            {
                m_isSpawn = true;
                m_Walls.SetActive(true);
            }
        }
    }

    [System.Serializable]
    public class SpawnData
    {
        public int PattonIndex;
        public string EnemyType;
        public float[] Position;
        public float Rotation;
        public int SpawnGroup;
    }
}