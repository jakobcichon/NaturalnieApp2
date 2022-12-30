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
        public Dictionary<string, Dictionary<string, IMenuScreen?>> MenuScreenList { get; init; }

        public MenuBarModel()
        {
            MenuScreenList = new();
        }

        #region Public methods
        public void AddGroup(string groupName)
        {
            MenuScreenList.Add(groupName, new Dictionary<string, IMenuScreen?>());
        }

        public Dictionary<string, IMenuScreen?> GetSubMenuDictForGivenGroup(string groupName)
        {
            CheckIfGroupExists(groupName);

            MenuScreenList.TryGetValue(groupName, out Dictionary<string, IMenuScreen?> menuScreenDict);

            return menuScreenDict!;
        }

        public IMenuScreen GetSceenForScreenName(string sceenName, Dictionary<string, IMenuScreen?> screens)
        {
            CheckIfScreenNameExists(sceenName, screens);

            screens.TryGetValue(sceenName, out IMenuScreen menuScreen);

            return menuScreen!;
        }

        public void AddScreen(string groupName, string screenName, IMenuScreen screen)
        {
            Dictionary<string, IMenuScreen?> screens = GetSubMenuDictForGivenGroup(groupName);

            screens?.Add(screenName, screen);
        }

        public bool SetScreenByName(string screenName, IMenuScreen? screenValue)
        {
            foreach (KeyValuePair<string, Dictionary<string, IMenuScreen?>> screenDict in MenuScreenList)
            {
                if(screenDict.Value.Any(d => d.Key.Equals(screenName)))
                {
                    screenDict.Value[screenName] = screenValue;
                    return true;
                }
            }

            return false;
        }

        public IMenuScreen? GetScreenByName(string screenName)
        {
            foreach (KeyValuePair<string, Dictionary<string, IMenuScreen?>> screenDict in MenuScreenList)
            {
                screenDict.Value.TryGetValue(screenName, out var screen);
                if (screen != null)
                {
                    return screen;
                }
            }

            return null;
        }

        public string? GetScreenNameByValue(IMenuScreen screen)
        {
            foreach (KeyValuePair<string, Dictionary<string, IMenuScreen?>> screenDict in MenuScreenList)
            {
                if(screenDict.Value.Any(s => s.Value == screen))
                {
                    return screenDict.Value.Where(s => s.Value == screen).FirstOrDefault().Key;
                }

            }

            return null;
        }

        public void ClearAll()
        {
            MenuScreenList?.Clear();
        }
        #endregion

        #region Private methods
        private void CheckIfGroupExists(string groupName)
        {
            if (!MenuScreenList.ContainsKey(groupName))
            {
                throw new NaturalnieExceptionBase($"Grupa menu o nazwie \"{groupName}\" nie istnieje!");
            }
        }

        private static void CheckIfScreenNameExists(string screenName, Dictionary<string, IMenuScreen?> screenDict)
        {
            if (!screenDict.ContainsKey(screenName))
            {
                ThrowSubMenuDoNotExist(screenName);
                return;
            }
        }

        private static void ThrowSubMenuDoNotExist(string screenName)
        {
            throw new NaturalnieExceptionBase($"Podmenu o nazwie \"{screenName}\" nie istnieje!");
        }
        #endregion

    }
}
