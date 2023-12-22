// �Ǘ��� ����
using System;
using System.Linq;
using UnityEngine;
namespace SLib
{
    namespace AI
    {
        /// <summary> ���؂̍��W�����i�[���Ă��� </summary>
        public class PathHolder : MonoBehaviour
        {
            /// <summary> AI�̃p�g���[�����铹�؂̊e����_�̃g�����X�t�H�[����Ԃ� </summary>
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