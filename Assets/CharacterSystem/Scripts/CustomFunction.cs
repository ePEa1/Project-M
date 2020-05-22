using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.ePEa.CustomFunctions
{
    public class CustomFunction : MonoBehaviour
    {
        //
        public static Vector3 FixedMovePos(Vector3 startPos, float size, Vector3 dir, float speed, LayerMask layer)
        {
            Vector3 fixedPos = Vector3.zero;

            RaycastHit[] hits = Physics.SphereCastAll(startPos, size, dir.normalized, speed, layer);

            if (hits.Length > 0)
            {
                for(int i=0;i<hits.Length;i++)
                {
                    fixedPos += new Vector3(hits[i].normal.x, 0.0f, hits[i].normal.z).normalized * speed * Vector3.Dot(new Vector3(hits[i].normal.x, 0.0f, hits[i].normal.z).normalized, -dir.normalized);
                }
            }

            return fixedPos;
        }
    }
}