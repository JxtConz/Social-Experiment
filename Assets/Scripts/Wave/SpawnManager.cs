using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wave
{
    public class SpawnManager : MonoBehaviour
    {

        public GameObject end;
        public Finishable finishable;

        public EnemyData data;

        private Dictionary<char, HashSet<SpawnSource>> sources = new Dictionary<char, HashSet<SpawnSource>>();

        private WaveCondition[] waves;

        [SerializeField]
        private int currentWave = -1;

        // Start is called before the first frame update
        void Awake()
        {
            SpawnSource[] all = GameObject.FindObjectsOfType<SpawnSource>();
            foreach(SpawnSource s in all)
            {
                foreach(char g in s.goups)
                {
                    if (sources.ContainsKey(g))
                    {
                        sources[g].Add(s);
                    }else
                    {
                        HashSet<SpawnSource> l = new HashSet<SpawnSource>();
                        l.Add(s);
                        sources.Add(g, l);
                    }
                }
            }

            waves = getWaveConditionInOrder();
            foreach(WaveCondition w in waves)
            {
                w.Manager = this;
            }
            currentWave = -1;

            if (end != null)
                finishable = end.GetComponent<Finishable>();
        }

        private WaveCondition[] getWaveConditionInOrder()
        {

            WaveCondition[] back = GetComponentsInChildren<WaveCondition>();
            System.Array.Sort(back, 
                Comparer<WaveCondition>.Create(
                    (x, y) => x.transform.GetSiblingIndex() - y.transform.GetSiblingIndex()));

            string names = "";
            foreach(WaveCondition wc in back)
            {
                names += " " + wc.name;
            }
            Debug.Log(names);

            return back;
        }

        void Update()
        {
            if (currentWave == -2)
            {

            }
            else if (currentWave == -1)
            {
                currentWave++;
                waves[currentWave].StartWave();
                //Debug.Log("AA Start Wave " + currentWave + " " + waves[currentWave].name);
                waves[currentWave].UpdateWave();
            }
            else
            {
                //Debug.Log("WaveFinished" + waves[currentWave].WaveFinished() + " " + waves[currentWave].name);
                if (waves[currentWave].WaveFinished())
                {
                    //Debug.Log("finished Wave " + currentWave + " " + waves[currentWave].name);
                    currentWave++;
                    if(currentWave >= waves.Length)
                    {
                        currentWave = -2;
                        Finish();
                        return;
                    }
                    else
                    {
                        waves[currentWave].StartWave();
                        //Debug.Log("Start Wave " + currentWave + " " + waves[currentWave].name);
                    }
                }
                waves[currentWave].UpdateWave();
            }
        }

        public HashSet<SpawnSource> getGroup(params char[] groupNames)
        {
            HashSet<SpawnSource> all = new HashSet<SpawnSource>();
            foreach(char groupName in groupNames){
                HashSet<SpawnSource> current = sources[groupName];
                foreach (SpawnSource ss in current) {
                    if (!all.Contains(ss))
                    {
                        all.Add(ss);
                    }
                }
            }
            return all;
        }

        private void Finish()
        {
            if (finishable != null)
            {
                finishable.Finished();
            }
        }
    }
}