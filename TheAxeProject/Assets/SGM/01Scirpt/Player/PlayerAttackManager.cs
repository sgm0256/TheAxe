using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private GameObject axePrefab;
    [SerializeField] private Transform axeContainer;
    [SerializeField] private int maxAxeCount = 3;
    private float spawnCoolTime = 1f;
    private bool isSpawning = false;
    private bool isSorting = false;

    private InputReaderSO input;
    private Player player;

    private List<AxeController> axeList = new List<AxeController>();

    private void Update()
    {
        if (axeList.Count < maxAxeCount && !isSpawning)
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            {
                isSpawning = true;
                StartCoroutine(CreateAxe());
            }
        }

        RevolutionAxe();
    }

    private void RevolutionAxe()
    {
        Vector3 rotation = axeContainer.eulerAngles;
        rotation.z += 1;
        axeContainer.rotation = Quaternion.Euler(rotation);
    }

    private IEnumerator CreateAxe()
    {
        yield return new WaitForSeconds(spawnCoolTime);
        //yield return null;

        AxeController axe = Instantiate(axePrefab, transform.position, Quaternion.identity).GetComponent<AxeController>();
        axe.transform.parent = axeContainer;

        //axe.MoveTheCircle(0, true);
        axeList.Add(axe);
        SortAxe();

        isSpawning = false;
    }

    private void SortAxe()
    {
        if (axeList.Count == 0 || isSorting)
            return;

        isSorting = true;
        int angle = 360 / axeList.Count;

        for (int i = 0; i < axeList.Count; i++)
        {
            Vector3 dir = Quaternion.Euler(0, 0, i * angle) * transform.up;
            Vector3 pos = dir.normalized;

            axeList[i].transform.DOLocalMove(pos, 0.5f);
            //axeList[i].MoveTheCircle(i * angle, false);
        }
        isSorting = false;
    }

    public void Initialize(Player player)
    {
        this.player = player;

        input = player.GetCompo<InputReaderSO>();
        input.FireEvent += Attack;
    }

    private void Attack()
    {
        if (axeList.Count == 0 || isSorting)
            return;

        AxeController axe = axeList[0];
        axeList.Remove(axe);
        SortAxe();

        axe.StartAttack();
    }
}
