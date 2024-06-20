using System.Collections.Generic;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.UI
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
            if (_menus.Count > 0)
                _menus.Pop().gameObject.SetActive(false);
            if(_menus.Count > 0)
                _menus.Peek().gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _menus.Pop().gameObject.SetActive(false);
            _menus.Clear();
        }
    }
}