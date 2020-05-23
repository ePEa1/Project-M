using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{

    public static bool Detect(Vector3 pos, Vector3 targetPos, float Distance)
    {
        //멀리 있을 때
        if (Vector3.Distance(targetPos, pos) >= Distance)
            return false;

        //아님 가까이 있을 때
        else
            return true;
    }

    //public static bool Detect(Camera sight,
    //    float aspect,
    //    Collider cc)
    //{
    //    if (cc == null)
    //        return false;

    //    sight.aspect = aspect;
    //    Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
    //    return GeometryUtility.TestPlanesAABB(ps, cc.bounds);
    //}

    public static void CKRotate(
        Transform self,
        Vector3 targetPos,
        float rotateSpeed
    )
    {
        Vector3 dir = targetPos - self.position;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            self.rotation = Quaternion.RotateTowards(
                self.rotation,
                Quaternion.LookRotation(dir),
                rotateSpeed * Time.deltaTime
                );
        }
    }

    public static void CKMove(
        GameObject cc,
        Transform self, 
        Vector3 targetPos,
        float moveSpeed,
        float rotateSpeed
        )
    {
        Vector3 deltaMove = Vector3.MoveTowards(
            self.position,
            targetPos,
            moveSpeed * Time.deltaTime
            ) - self.position;

        cc.transform.Translate(new Vector3(0, 0, 1)*moveSpeed*
            Time.deltaTime) ;

        Vector3 dir = targetPos - self.position;
        dir.y = 0;
        if(dir != Vector3.zero)
        {
            self.rotation = Quaternion.RotateTowards(
                self.rotation,
                Quaternion.LookRotation(dir),
                rotateSpeed * Time.deltaTime
                );
        }
    }
}
