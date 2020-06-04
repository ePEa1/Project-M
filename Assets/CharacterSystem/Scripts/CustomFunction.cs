using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.ePEa.CustomFunctions
{
    public class CustomFunction : MonoBehaviour
    {
        /// <summary>
        /// 이동 보정 함수
        /// 이동시킬 값에 이 함수의 반환값을 더해줘야 벽을 안뚫음
        /// 자세한 계산 방식에 대한 설명은 생략하고
        /// 벽을 뚫어서 이동하려고 하면 뚫은 거리만큼 벽의 노말방향대로 다시 밀어내는 방식.
        /// 이 함수는 그 밀어내는 수치를 계산해서 반환해주는 함수
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

            //RaycastHit[] hits = Physics.SphereCastAll(startPos, size, dir.normalized, speed, layer);

            //if (hits.Length > 0)
            //{
            //    for(int i=0;i<hits.Length;i++)
            //    {
            //        if (hits[i].point!=Vector3.zero)
            //        {
            //            Vector3 normal = new Vector3(hits[i].normal.x, 0.0f, hits[i].normal.z).normalized;

            //            float dot = Vector3.Dot(-dir.normalized, normal);

            //            float dis = Vector3.Distance(new Vector3(hits[i].point.x, 0.0f, hits[i].point.z) + normal * size,
            //                new Vector3(startPos.x, 0.0f, startPos.z) + dir * speed);

            //            fixedPos += normal * dis * dot;
            //        }
            //        else
            //        {
            //            RaycastHit hit;
            //            if (Physics.Raycast(startPos, dir.normalized, out hit, speed + 2.0f, layer))
            //            {
            //                Vector3 normal = new Vector3(hit.normal.x, 0.0f, hit.normal.z).normalized;
            //                float dot = Vector3.Dot(-dir.normalized, normal);

            //                float dis = Vector3.Distance(new Vector3(hit.point.x, 0.0f, hit.point.z),
            //                new Vector3(startPos.x, 0.0f, startPos.z) + dir.normalized * speed);

            //                if (Vector3.Distance(startPos, new Vector3(hit.point.x, 0.0f, hit.point.z)) >
            //                    Vector3.Distance(startPos, new Vector3(startPos.x, 0.0f, startPos.z) + dir.normalized * speed))
            //                    dis *= -1;

            //                fixedPos += normal * (dis * dot + size);
            //            }
            //        }
            //    }
            //}

            RaycastHit[] hits = Physics.RaycastAll(startPos, dir.normalized, speed + size * 10.0f, layer);

            if (hits.Length > 0)
            {
                for(int i=0;i<hits.Length;i++)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(startPos - hits[i].normal * size, dir.normalized, out hit, speed, layer))
                    {
                        Vector3 normal = new Vector3(hits[i].normal.x, 0.0f, hits[i].normal.z).normalized;
                        float dot = Vector3.Dot(-dir.normalized, normal);
                        float dis = Vector3.Distance(startPos - hits[i].normal * size + dir.normalized * speed, hit.point);

                        Debug.Log(dis);

                        fixedPos += normal * (dis + size) * dot;
                    }
                }
            }

            //Debug.Log(fixedPos);

            return fixedPos;
        }
    }
}

//※사용법※----------------------------
//사용해야 하는 스크립트에서 using static ProjectM.ePEa.CustomFunctions.CustomFunction; 작성