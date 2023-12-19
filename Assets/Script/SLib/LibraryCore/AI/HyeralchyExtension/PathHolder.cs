// 管理者 菅沼
using System;
using System.Linq;
using UnityEngine;
namespace SLib
{
    namespace AI
    {
        /// <summary> 道筋の座標情報を格納している </summary>
        public class PathHolder : MonoBehaviour
        {
            /// <summary> AIのパトロールする道筋の各分岐点のトランスフォームを返す </summary>
            /// <returns></returns>
            public Vector3[] GetPatrollingPath()
            {
                var temp = Array.ConvertAll(transform.GetComponentsInChildren<Transform>(), x => x.position).ToList();
                temp.RemoveAt(0);
                return temp.ToArray();
            }

            private void OnDrawGizmos()
            {
                var points = transform.GetComponentsInChildren<Transform>();
                var work = points.ToList();
                work.RemoveAt(0);
                points = work.ToArray();
                Gizmos.color = Color.yellow;
                foreach (var p in points)
                {
                    Gizmos.DrawWireSphere(p.position, .5f);
                } // draw sphere to each point's position
                //Gizmos.DrawLineStrip(Array.ConvertAll(points, x => x.position), true);
            }
        }
    }
}