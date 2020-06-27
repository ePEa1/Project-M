using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.ePEa.LDSystem
{
    public class TileCreator : MonoBehaviour
    {
        [SerializeField] public int m_tileX = 0;
        [SerializeField] public int m_tileY = 0;

        [SerializeField] GameObject m_tile;
        [SerializeField] BoxCollider m_tileCollider;

        int m_x = 0;
        int m_y = 0;

        void Awake()
        {
            CreateTile();
        }

        // Update is called once per frame
        void Update()
        {
            if (m_tileX != m_x || m_tileY != m_y)
                CreateTile();
        }

        void CreateTile()
        {
            for (int i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);

            Vector3 pos = new Vector3(transform.position.x, 0.0f, transform.position.z);

            for (int i = 0; i < m_tileY; i++)
            {
                for (int j = 0; j < m_tileX; j++)
                {
                    GameObject t = Instantiate(m_tile);
                    t.transform.parent = transform;
                    t.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    t.transform.rotation = Quaternion.Euler(new Vector3(-90.0f, 0.0f, 0.0f));
                    t.transform.position = pos + new Vector3(j * 2 - m_tileX + 1, 0.0f, i * 2 - m_tileY + 1);
                }
            }
            m_tileCollider.center = Vector3.up * (0.05f - transform.position.y);
            m_tileCollider.size = new Vector3(m_tileX * 2, 0.1f, m_tileY * 2);
            m_x = m_tileX;
            m_y = m_tileY;
        }
    }
}