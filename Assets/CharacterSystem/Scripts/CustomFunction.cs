using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.ePEa.CustomFunctions
{
    public class CustomFunction : MonoBehaviour
    {
        /// <summary>
        /// 이동 보정 함수
        /// </summary>
        /// <param name="startPos">시작위치</param>
        /// <param name="size">구체 반지름</param>
        /// <param name="dir">레이 방향 벡터</param>
        /// <param name="speed">레이 길이</param>
        /// <param name="layer">찾을 레이어</param>
        /// <returns></returns>
        public static Vector3 FixedMovePos(Vector3 startPos, float size, Vector3 dir, float speed, LayerMask layer)
        {
            Vector3 fixedPos = Vector3.zero;

            RaycastHit[] hits = Physics.SphereCastAll(startPos, size, dir.normalized, speed, layer);

            if (hits.Length > 0)
            {
                for(int i=0;i<hits.Length;i++)
                {
                    if (hits[i].point!=Vector3.zero)
                    {
                        Vector3 normal = new Vector3(hits[i].normal.x, 0.0f, hits[i].normal.z).normalized;

                        float dot = Vector3.Dot(-dir.normalized, normal);

                        float dis = Vector3.Distance(new Vector3(hits[i].point.x, 0.0f, hits[i].point.z) + normal * size,
                            new Vector3(startPos.x, 0.0f, startPos.z) + dir * speed);

                        fixedPos += normal * dis * dot;
                    }
                    else
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(startPos, dir.normalized, out hit, speed + 2.0f, layer))
                        {
                            Vector3 normal = new Vector3(hit.normal.x, 0.0f, hit.normal.z).normalized;
                            float dot = Vector3.Dot(-dir.normalized, normal);

                            float dis = Vector3.Distance(new Vector3(hit.point.x, 0.0f, hit.point.z),
                            new Vector3(startPos.x, 0.0f, startPos.z) + dir.normalized * speed);

                            if (Vector3.Distance(startPos, new Vector3(hit.point.x, 0.0f, hit.point.z)) >
                                Vector3.Distance(startPos, new Vector3(startPos.x, 0.0f, startPos.z) + dir.normalized * speed))
                                dis *= -1;

                            fixedPos += normal * (dis * dot + size);
                        }
                    }
                }
            }

            return fixedPos;
        }

        public static Vector3 StopMovePos()
        {
            Vector3 stopPos = Vector3.zero;



            return stopPos;
        }
    }
}