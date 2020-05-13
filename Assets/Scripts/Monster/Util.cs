using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{

    public static bool Detect(Camera sight, 
        float aspect, 
        CharacterController cc)
    {
        if (cc == null)
            return false;

        sight.aspect = aspect;
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        return GeometryUtility.TestPlanesAABB(ps, cc.bounds);
    }

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
        Transform self, 
        Vector3 targetPos,
        float moveSpeed,
        float rotateSpeed,
        float fallSpeed)
    {
        Vector3 deltaMove = Vector3.MoveTowards(
            self.position,
            targetPos,
            moveSpeed * Time.deltaTime
            ) - self.position;



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
