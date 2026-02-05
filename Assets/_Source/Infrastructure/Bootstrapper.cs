using System.Collections.Generic;
using Contracts.Database;
using Contracts.Factory;
using Infrastructure.Database.Repositories;
using Infrastructure.Factory.Item;
using Infrastructure.Factory.Player;
using UnityEngine;
using Zenject;
using Unity.Plastic.Newtonsoft.Json;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [Inject] private readonly IPlayerConfig _playerConfig;
        [Inject] private readonly PlayerFactory _playerFactory;
        [Inject] private readonly ItemFactory _itemFactory;

        [SerializeField] private ItemsRepositories _repositories;
        [SerializeField] private string _jsonFileName;
        [SerializeField] private int _itemCount;

        private void Awake()
        {
            CreateRandomItem();
            _playerFactory.CreatePlayer(_playerConfig);
        }

        private void CreateRandomItem()
        {
            string json = LoadJsonFile();
            
            if(json == null)
                return;

            var data = JsonConvert.DeserializeObject<List<ItemConfig>>(json);

            for (int i = 0; i < _itemCount; i++)
            {
                int index = Random.Range(0, data.Count);
            
                _itemFactory.CreateItem(
                    _itemFactory.GetRandomPosition(),
                    data[index].Name, data[index].Id
                    , data[index].IsStackable, data[index].MaxStackCount,
                    new Color(data[index].UnityColor.Red / 255, data[index].UnityColor.Green / 255, data[index].UnityColor.Blue / 255)
                );
            }
        }
        
        private string LoadJsonFile()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(_jsonFileName);
        
            if (jsonFile == null)
            {
                return null;
            }
        
            return jsonFile.text;
        }
    }
}