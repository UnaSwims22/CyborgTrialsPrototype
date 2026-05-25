using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "RaycastData", menuName = "Game/ new RaycastData")]
public class RaycastData : ScriptableObject
{
    public Transform HitTransfrom { private set; get; }
    public RaycastHit? Hit { private set; get; }

    public void UpdateData(RaycastHit? _hit)
    {
        Hit = _hit;

        if (_hit.HasValue)
        {
            HitTransfrom = Hit.Value.transform;
            Debug.Log(HitTransfrom.name);
        }

        Debug.Log("Stored: " + _hit.Value.transform.name);

        // HitTransfrom = Hit.Value.transform;
        //Hit = _hit;

        //Debug.Log(HitTransfrom.name);
    }

    public bool IsThisNewObject (Transform transform)
    {
        if (HitTransfrom == null) return true;

        if (HitTransfrom == transform) return false;

        return true;
    }

    public void Reset()
    {
        HitTransfrom = null;
        Hit = null;
    }

}
