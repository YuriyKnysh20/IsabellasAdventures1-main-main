//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ItemsEjector : MonoBehaviour
//{
//    [SerializeField] private ItemsObjectPool _pool;
//    [SerializeField] private float _range;
//    [SerializeField] private PolygonCollider2D _ground;

//    public void EjectFromPool(IItem item, Vector3 position, Vector3 direction)
//    {
//        //берем из пуля просчитываем цель 
//        var presenter = _pool.Get(item);
//        presenter.transform.position = position;

//        var target = position + (direction.normalized * _range);
//        target = _ground.bounds.ClosestPoint(target);

//        presenter.gameObject
//            .AddComponent<MovingAlongCurve>()
//        .StartMoving(position, target, Vector3.Lerp(position, target, 0.5f) + new Vector3(0, 2, 0), 1)
//        .RemoveWhenFinished();
//    }


//}
