using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private Transform axeContainer;
    [SerializeField] private GameObject axePrefab;
    [SerializeField] private int maxAxeCount = 3;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float axeRotateSpeed = 5f;
    private float spawnCoolTime = 1f;
    private bool isSpawning = false;

    private InputReaderSO input;

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

        //AxeRotate();
    }

    private void AxeRotate()
    {
        Vector3 rotation = axeContainer.rotation.eulerAngles;
        rotation.z += axeRotateSpeed;
        axeContainer.rotation = Quaternion.Euler(rotation);
    }

    private IEnumerator CreateAxe()
    {
        yield return new WaitForSeconds(spawnCoolTime);
        //yield return null;

        AxeController axe = Instantiate(axePrefab, transform.position, Quaternion.identity, axeContainer).GetComponent<AxeController>();

        axeList.Add(axe);
        SortAxe(true);

        isSpawning = false;
    }

    private void SortAxe(bool isSpawn)
    {
        if (axeList.Count == 0)
            return;

        float curAngle = 360 / axeList.Count;

        for (int i = 0; i < axeList.Count; i++)
        {
            float angle = i * curAngle;
            bool isLast = i == axeList.Count - 1;
            axeList[i].MoveTheCircle(angle, isSpawn && isLast);
        }
    }

    public void Initialize(Player player)
    {
        input = player.GetCompo<InputReaderSO>();
        input.FireEvent += Attack;
    }

    private void Attack()
    {
        if (axeList.Count == 0)
            return;

        AxeController axe = axeList[0];
        axeList.Remove(axe);
        SortAxe(false);

        axe.StartAttack();
    }
}
