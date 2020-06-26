using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.ePEa.LDSystem
{
    public class EnemyCreator : MonoBehaviour
    {
        [SerializeField] GameObject m_enemyMelee;
        [SerializeField] GameObject m_enemyBoss;
        [SerializeField] TileCreator m_tile;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                CreateMelee();
        }

        void CreateMelee()
        {
            GameObject e = Instantiate(m_enemyMelee);
            float x = Random.Range(-m_tile.m_tileX / 2, m_tile.m_tileX / 2);
            float y = Random.Range(-m_tile.m_tileY / 2, m_tile.m_tileY / 2);
            e.transform.position = new Vector3(x, 0.65f, y);
        }
    }
}