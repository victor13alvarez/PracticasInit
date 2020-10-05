using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelsController : MonoBehaviour
{
    GameObject easyCourse;
    public GameObject[] prefabs;
    // Start is called before the first frame update
    void Start()
    {
        easyCourse = Instantiate(prefabs[Random.Range(0, 3)], this.transform);
        easyCourse.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) * this.transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void easyCompleted()
    {
        Destroy(easyCourse.GetComponentInChildren<stickController>());
        Destroy(easyCourse);
        StartCoroutine(createLevel(4, 7));
    }

    public void mediumCompleted()
    {
        Destroy(GameObject.FindGameObjectWithTag("Medium").GetComponentInChildren<stickController>());
        Destroy(GameObject.FindGameObjectWithTag("Medium"));
        StartCoroutine(createLevel(8, 11));
    }

    public void hardCompleted()
    {
        this.gameObject.GetComponent<AudioSource>().Stop();
    }

    IEnumerator createLevel(int from, int to)
    {
        yield return new WaitForSeconds(2f);
        GameObject court = Instantiate(prefabs[Random.Range(from, to)], this.transform);
        court.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) * this.transform.localScale.x;
    }
}
