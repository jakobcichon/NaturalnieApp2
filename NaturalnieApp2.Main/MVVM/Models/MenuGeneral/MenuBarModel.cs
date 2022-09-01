namespace NaturalnieApp2.Main.MVVM.Models.MenuGeneral
{
    using NaturalnieApp2.Main.Exceptions;
    using NaturalnieApp2.Main.Interfaces.Screens;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class MenuBarModel
    {
        public Dictionary<string, Dictionary<string, IMenuScreen>> MenuScreenList { get; init; }
        public IMenuScreen? DefaultScreen { get; private set; }

        public MenuBarModel()
        {
            MenuScreenList = new();
        }

        public void AddDefaultScreen(IMenuScreen? defaultScreen)
        {
            DefaultScreen = defaultScreen;
        }

        public void AddGroup(string groupName)
        {
            MenuScreenList.Add(groupName, new Dictionary<string, IMenuScreen>());
        }

        public void AddScreen(string groupName, string screenName, IMenuScreen screen)
        {
            if (!MenuScreenList.ContainsKey(groupName))
            {
                throw new NaturalnieExceptionBase($"Grupa menu o nazwie \"{groupName}\" nie istnieje!");
            }

            Dictionary<string, IMenuScreen>? menuScreenDict = new();
            MenuScreenList.TryGetValue(groupName, out menuScreenDict);

            if(menuScreenDict == null)
            {
                menuScreenDict = new();
            }

            menuScreenDict?.Add(screenName, screen);
        }

        public IMenuScreen? GetScreenByName(string screenName)
        {
            foreach(KeyValuePair<string, Dictionary<string, IMenuScreen>> screenDict in MenuScreenList)
            {
                screenDict.Value.TryGetValue(screenName, out var screen);
                if (screen != null)
                {
                    return screen;
                }
            }

            return null;
        }

        public void ClearAll()
        {
            MenuScreenList?.Clear();
        }

    }
}
