using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElinCoureseProjectClient.Models
{
  public static class NavMenuList
  {
    public static void AddToList(MenuItems navName)
    {
      _navList.Add(navName);
      _navList = _navList.Distinct().ToList();
      ChangeState.Invoke(null, new EventArgs());
    }
    public static void RemoveFromList(MenuItems navName)
    {
      _navList.Remove(navName);
    }
    public static List<MenuItems> GetList()
    {
      return _navList;
    }

    public static EventHandler ChangeState { get; set; }
    private static List<MenuItems> _navList = new List<MenuItems>();
  }
}
