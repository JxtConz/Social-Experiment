using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{

    [RequireComponent(typeof(LineRenderer))]
    public class CrawlingRoot : MonoBehaviour
    {
        private const float MIN_NODE_DISTANCE = 0.5f * 0.5f;

        private List<GameObject> points;

        public Transform target;

        public float speed = 3;

        private bool end = false;

        private LineRenderer lineRenderer;

        public int sortingLayerID;
        public int sortingOrder;

        private Vector3 GetTarget()
        {
            return target.position;
        }

        private GameObject createNewNode(Vector3 pos)
        {
            GameObject g = new GameObject(gameObject.name + " " + points.Count);
            g.transform.position = pos;
            g.transform.parent = transform;
            g.AddComponent<BoxCollider2D>();
            return g;
        }

        private bool NeedNewNode(Vector3 a, Vector3 b)
        {
            Vector3 dis = a - b;
            Debug.Log(dis.sqrMagnitude);
            return dis.sqrMagnitude > MIN_NODE_DISTANCE;
        }

        private void MoveOrCreateNode(float delta)
        {
            GameObject a = points[points.Count - 1];
            GameObject b = points[points.Count - 2];

            Vector3 dir = (GetTarget() - a.transform.position);
            if(dir.sqrMagnitude < 0.001f)
            {
                Debug.Log("at player");
                end = true;
            } 
            else
            {
                Vector3 newDir = dir.normalized * delta;
                Vector3 newTarget = a.transform.position + newDir;
                Debug.Log("newTarget " + newTarget + " dir " + dir.normalized + " " + delta);
                DebugCross(newTarget, Color.blue);
                if (NeedNewNode(b.transform.position, newTarget))
                {
                    b = a;
                    a = createNewNode(newTarget);
                    points.Add(a);
                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPosition(points.Count - 1, newTarget);
                }
                else
                {
                    a.transform.position = newTarget;
                    lineRenderer.SetPosition(points.Count - 1, newTarget);
                }
                CalcCollider(a, b);
            }

            
        }

        void CalcCollider(GameObject c, GameObject last)
        {
            BoxCollider2D coll = c.GetComponent<BoxCollider2D>();
            Vector3 dir = last.transform.position - c.transform.position;
            if(dir != Vector3.zero)
            {
                
                float length = dir.magnitude;
                //center =  center / length;
                //float angle = Vector3.Angle(center, transform.forward);
                //Quaternion m_MyQuaternion;

                //m_MyQuaternion = Quaternion.FromToRotation(transform.rotation.SetLookRotation, dir);

                //Debug.Log(length + " " + m_MyQuaternion);

                //c.transform.rotation = m_MyQuaternion * transform.rotation;
                //transform.rotation.SetLookRotation(dir, Vector3.right);
                //c.transform.LookAt(last.transform, Vector3.forward);
                float f = Vector3.SignedAngle(Vector3.forward, dir.normalized, Vector3.forward);
                c.transform.rotation.SetEulerAngles(0, 0, f);
                coll.size = new Vector2(1, length * 2);
                coll.offset = new Vector2(0, -length / 2); 
                
            }
                        
        }

        void DebugCross(Vector3 point, Color c)
        {
            Debug.DrawLine(point + Vector3.forward, point + Vector3.back, c);
            Debug.DrawLine(point + Vector3.up, point + Vector3.down, c);
            Debug.DrawLine(point + Vector3.left, point + Vector3.right, c);
        }

        void DebugShow()
        {
            for(int i = 1; i < points.Count; i++)
            {
                Debug.DrawLine(points[i - 1].transform.position, points[i].transform.position);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            points = new List<GameObject>();
            points.Add(gameObject);
            points.Add(createNewNode(gameObject.transform.position));
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, gameObject.transform.position);


            //lineRenderer.sortingLayerID = sortingLayerID;
            //lineRenderer.sortingOrder = sortingOrder;
        }

        // Update is called once per frame
        void Update()
        {
            if (!end)
            {
                float moveDis = Time.deltaTime * speed;
                MoveOrCreateNode(moveDis);
            }
            DebugShow();
        }
    }
}
