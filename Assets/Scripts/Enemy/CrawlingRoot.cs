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

        public HitPlayer target;

        public float speed = 3;

        public float distaceNeededToPlayer = 0.5f;

        private float distaceNeededToPlayerSqr;

        private bool end = false;

        private LineRenderer lineRenderer;

        public int sortingLayerID;
        public int sortingOrder;

        private float currentCooldown;

        public float cooldown;

        public int hitStrength;

        private Vector3 GetTarget()
        {
            return target.transform.position;
        }

        private GameObject CreateNewNode(Vector3 pos)
        {
            GameObject g = new GameObject(gameObject.name + " " + points.Count);
            g.transform.position = pos;
            g.transform.parent = transform;
            g.AddComponent<BoxCollider2D>();
            g.layer = this.gameObject.layer;
            return g;
        }

        private bool CheckIfAtPlayer(Vector3 dir)
        {
            return dir.sqrMagnitude < distaceNeededToPlayer;
        }

        private void ExecuteHit()
        {
            if(currentCooldown < 0)
            {
                target.HitPlayer(gameObject, hitStrength, HitPlayer.HitType.RootTip);
                currentCooldown = cooldown;
            }
        }

        private bool NeedNewNode(Vector3 a, Vector3 b)
        {
            Vector3 dis = a - b;
            return dis.sqrMagnitude > MIN_NODE_DISTANCE;
        }

        private void MoveOrCreateNode(float delta)
        {
            GameObject a = points[points.Count - 1];
            GameObject b = points[points.Count - 2];

            Vector3 dir = (GetTarget() - a.transform.position);
            if(CheckIfAtPlayer(dir))
            {
                ExecuteHit();
            } 
            else
            {
                Vector3 newDir = dir.normalized * delta;
                Vector3 newTarget = a.transform.position + newDir;
                DebugCross(newTarget, Color.blue);
                if (NeedNewNode(b.transform.position, newTarget))
                {
                    b = a;
                    a = CreateNewNode(newTarget);
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
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                c.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                coll.size = new Vector2(length, 1);
                coll.offset = new Vector2(length / 2, 0); 
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
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player == null)
            {
                Debug.Log("cant find player end");
                enabled = false;
                return;
            }
                
            target = player.GetComponent<HitPlayer>();

            distaceNeededToPlayerSqr = distaceNeededToPlayer * distaceNeededToPlayerSqr;
            points = new List<GameObject>();
            points.Add(gameObject);
            points.Add(CreateNewNode(gameObject.transform.position));
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, gameObject.transform.position);

        }

        // Update is called once per frame
        void Update()
        {
            if (currentCooldown < 0)
            {
                float moveDis = Time.deltaTime * speed;
                MoveOrCreateNode(moveDis);
            } else
            {
                currentCooldown -= Time.deltaTime;
            }
            DebugShow();
        }
    }
}
