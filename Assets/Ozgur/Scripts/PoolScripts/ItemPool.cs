using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ozgur.Scripts.Pools
{
    public class ItemPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private Dictionary<T, bool> itemPool = new();
        [SerializeField] private T itemPrefab;
        [SerializeField] private int initialItemAmount;

        public static ItemPool<T> Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitPool();
            }

            else Destroy(this);
        }

        private void InitPool()
        {
            for (var i = 0; i < initialItemAmount; i++) CreateEmptyItem();
        }

        private void CreateEmptyItem()
        {
            var item = Instantiate(itemPrefab, transform);
            itemPool.Add(item, false);
            item.gameObject.SetActive(false);
        }

        public T GetItemFromPool()
        {
            foreach (var item in itemPool.Keys.ToList())
            {
                if (itemPool[item]) continue;

                itemPool[item] = true;
                item.gameObject.SetActive(true);

                return item;
            }

            CreateEmptyItem();

            var lastItem = itemPool.Last();
            itemPool[lastItem.Key] = true;
            lastItem.Key.gameObject.SetActive(true);

            return itemPool.Last().Key;
        }

        public void ReturnItemToPool(T item)
        {
            itemPool[item] = false;
            item.gameObject.SetActive(false);
            item.transform.SetParent(transform);
        }
    }
}