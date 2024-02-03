﻿using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class MenuStack : MonoBehaviour
    {
        [SerializeField] private Menu _default;
        private readonly Stack<Menu> _menus = new Stack<Menu>();

        private void OnEnable()
        {
            _menus.Clear();
            _default.gameObject.SetActive(true);
            _menus.Push(_default);
        }

        public void Open(Menu menu)
        {
            _menus.Peek().gameObject.SetActive(false);
            
            menu.gameObject.SetActive(true);
            _menus.Push(menu);
        }

        public void Close()
        {
            _menus.Pop().gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _menus.Pop().gameObject.SetActive(false);
            _menus.Clear();
        }
    }
}