using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator sharedInstance; //instancia compartida que permite tener un unico generador de niveles

    public List<LevelBlock> allTheLevelBlocks = new(); //lista que contiene todos los niveles creados
    public List<LevelBlock> currentLevelBlocks = new(); //lista de bloques actuales
    public Transform levelInitialPoint; //punto inicial donde se genera el primer nivel de todos

    private bool _isGeneratingInitialBlocks;

    private void Awake()
    {
        sharedInstance = this;
    }


    // Use this for initialization
    private void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void GenerateInitialBlocks()
    {
        _isGeneratingInitialBlocks = true;
        for (var i = 0; i < 3; i++)
        {
            AddNewBlock();
            _isGeneratingInitialBlocks = false;
        }

    }

    public void AddNewBlock()
    {
        //seleccionar un bloque aleatorio dentro de los disponibles 
        var randomIndex = Random.Range(0, allTheLevelBlocks.Count);

        if (_isGeneratingInitialBlocks) randomIndex = 0;

        var block = Instantiate(allTheLevelBlocks[randomIndex]);
        block.transform.SetParent(transform, false);

        //posicion del bloque
        var blockPosition = Vector3.zero;
        if (currentLevelBlocks.Count == 0) //vamos a colocar el primer elemento en pantalla
            blockPosition = levelInitialPoint.position;
        else
        {
            var previousBlock = currentLevelBlocks[^1];
            blockPosition = previousBlock.exitPoint.position;
            blockPosition.x += block.blockWidth / 2;
        }

        block.transform.position = blockPosition;
        currentLevelBlocks.Add(block);
    }

    public void RemoveOldBlock()
    {
        if (currentLevelBlocks.Count <= 4) return;
        var block = currentLevelBlocks[0];
        currentLevelBlocks.Remove(block);
        Destroy(block.gameObject);
    }

    public void RemoveAllTheBlocks()
    {
        while (currentLevelBlocks.Count > 0) RemoveOldBlock();
    }
}