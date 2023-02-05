using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enemy
{

    [RequireComponent(typeof(LineRenderer))]
    public class CrawlingRoot : MonoBehaviour, HitAbleEnemy, KillAbleEnemy
    {
        private const float MIN_NODE_DISTANCE = 0.5f * 0.5f;

        private List<GameObject> points;

        public HitPlayer target;

        public float speed = 3;

        public float distaceNeededToPlayer = 0.5f;

        private float distaceNeededToPlayerSqr;

        private LineRenderer lineRenderer;

        public int sortingLayerID;
        public int sortingOrder;

        private float currentAttackCooldown;
        private float currentDeadTimeCooldown;
        private float currentStunnedTimeCooldown;

        public float cooldown;

        public int hitStrength;

        public int hitPoints;

        public float stunnedTime = 0.5f;
        public float fadeDeadTime = 2;

        public Color normalColor = Color.white;
        public Color hitColor = Color.red;

        public bool testHit;

        public float colliderThickness = 1f;

        public GameObject head;
        private SpriteRenderer headSpriteRenderer;

        public EnemySound sound;
        public event Dying OnDying;

        void OnValidate()
        {
            if (testHit)
            {
                testHit = false;
                HitEnemy(1);
            }
        }

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
            RootDelegate d = g.AddComponent<RootDelegate>();
            d.owner = this;
            return g;
        }

        private bool CheckIfAtPlayer(Vector3 dir)
        {
            return dir.sqrMagnitude < distaceNeededToPlayer;
        }

        private void MakeSound(EnemySound.SoundType s)
        {
            if(sound != null)
            {
                sound.MakeSound(s);
            }
        }

        private void ExecuteHit()
        {
            if(currentAttackCooldown < 0)
            {

                MakeSound(EnemySound.SoundType.RootAttack);
                target.HitPlayer(gameObject, hitStrength, HitPlayer.HitType.RootTip);
                currentAttackCooldown = cooldown;
            }
        }

        private bool NeedNewNode(Vector3 a, Vector3 b)
        {
            Vector3 dis = a - b;
            return dis.sqrMagnitude > MIN_NODE_DISTANCE;
        }

        private void RotateHead(Vector3 newPos)
        {
            if(head != null)
            {
                head.transform.position = newPos;
                Vector3 dir = (GetTarget() - newPos);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                head.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void SetupHead()
        {
            if (head != null)
            {
                headSpriteRenderer = head.GetComponent<SpriteRenderer>();
                sound = head.GetComponent<EnemySound>();
                SetHeadColor(normalColor);
            }
        }
        private void SetHeadColor(Color c)
        {
            if (headSpriteRenderer != null)
            {
                headSpriteRenderer.color = c;
            }
        }

        private void SetHeadAlpha(float f)
        {
            if (headSpriteRenderer != null)
            {
                Color c = headSpriteRenderer.color;
                c.a = f;
                headSpriteRenderer.color = c;
            }
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
                //DebugCross(newTarget, Color.blue);
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
                RotateHead(newTarget);
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

                coll.size = new Vector2(length, colliderThickness);
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
                Debug.LogWarning("cant find player end");
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

            SetupHead();

        }

        // Update is called once per frame
        void Update()
        {
            if (currentDeadTimeCooldown > 0)
            {
                float fade = currentDeadTimeCooldown / fadeDeadTime;
                ApplyAlpha(fade);
                currentDeadTimeCooldown -= Time.deltaTime;
                if (currentDeadTimeCooldown < 0)
                {
                    this.enabled = false;
                    GameObject.Destroy(gameObject);
                    if (OnDying != null)
                        OnDying.Invoke();
                    return;
                }
            }
            else if (currentStunnedTimeCooldown > 0)
            {
                float fade = currentStunnedTimeCooldown / stunnedTime;
                if (fade < 0.5)
                {
                    ApplyColor(Color.Lerp(normalColor, hitColor, fade * 2));
                } 
                else
                {
                    ApplyColor(Color.Lerp(hitColor, normalColor, (fade - 0.5f) * 2));
                }
                currentStunnedTimeCooldown -= Time.deltaTime;
            } 
            else if (currentAttackCooldown < 0)
            {
                ApplyColor(normalColor);
                float moveDis = Time.deltaTime * speed;
                MoveOrCreateNode(moveDis);
            }
            else
            {
                currentAttackCooldown -= Time.deltaTime;
            }
        }

        private void HitMe()
        {
            currentStunnedTimeCooldown = stunnedTime;
            MakeSound(EnemySound.SoundType.RootHurt);

        }

        private void KillMe()
        {
            currentDeadTimeCooldown = fadeDeadTime;
            for (int i = 1; i < points.Count; i++)
            {
                GameObject.Destroy(points[i]);
            }
            points.Clear();
            MakeSound(EnemySound.SoundType.RootDie);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void ApplyColor(Color c)
        {
            Gradient g = lineRenderer.colorGradient;
            GradientColorKey[] colorKeys = g.colorKeys;
            for (int j = 0; j < colorKeys.Length; j++)
            {
                colorKeys[j].color = c;
            }
            g.colorKeys = colorKeys;
            lineRenderer.colorGradient = g;

            for (int i = 0; i < lineRenderer.colorGradient.colorKeys.Length; i++)
            {
                lineRenderer.colorGradient.colorKeys[i].color = c;
            }
            SetHeadColor(c);
        }

        private void ApplyAlpha(float v)
        {
            Gradient c = lineRenderer.colorGradient;
            GradientAlphaKey[] alphaKeys = c.alphaKeys;
            for (int j = 0; j < alphaKeys.Length; j++)
            {
                alphaKeys[j].alpha = v;
            }
            c.alphaKeys = alphaKeys;
            lineRenderer.colorGradient = c;
            SetHeadAlpha(v);
        }

        public bool HitEnemy(int damage)
        {
            if (currentStunnedTimeCooldown <= 0)
            {
                hitPoints -= damage;
                if (hitPoints > 0)
                {
                    HitMe();
                    return false;
                }
                else
                {
                    KillMe();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
