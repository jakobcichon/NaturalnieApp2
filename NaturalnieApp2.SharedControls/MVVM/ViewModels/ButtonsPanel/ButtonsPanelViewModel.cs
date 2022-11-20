namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.ButtonsPanel
{
    using NaturalnieApp2.SharedControls.MVVM.Models.ButtonsPanel;
    using System;
    using System.CodeDom;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ButtonsPanelViewModel
    {
        public ObservableCollection<ButtonsModel> LeftButtons { get; init; }
        public ObservableCollection<ButtonsModel> RightButtons { get; init; }

        public ButtonsPanelViewModel()
        {
            LeftButtons = new();
            RightButtons = new();

        }

        public void AddLeftButton(string name, Action<object?> action, Predicate<object?> canExecute)
        {
            this.LeftButtons.Add(AddButton(name, action, canExecute));
            this.EnableButton(name);
        }

        public void AddRightButton(string name, Action<object?> action, Predicate<object?> canExecute)
        {
            this.RightButtons.Add(AddButton(name, action, canExecute));
            this.EnableButton(name);
        }

        public void DisableButton(string buttonName)
        {
            ButtonsModel? model = LeftButtons.Where(b => b.ButtonName == buttonName)?.FirstOrDefault();
            if(model != null)
            {
                model.IsEnabled = false;
                return;
            }

            model = RightButtons.Where(b => b.ButtonName == buttonName)?.FirstOrDefault();
            if (model != null)
            {
                model.IsEnabled = false;
                return;
            }
        }

        public void EnableButton(string buttonName)
        {
            ButtonsModel? model = LeftButtons.Where(b => b.ButtonName == buttonName)?.FirstOrDefault();
            if (model != null)
            {
                model.IsEnabled = true;
                return;
            }

            model = RightButtons.Where(b => b.ButtonName == buttonName)?.FirstOrDefault();
            if (model != null)
            {
                model.IsEnabled = true;
                return;
            }
        }

        private static ButtonsModel AddButton(string name, Action<object?> action, Predicate<object?> canExecute)
        {
            return new ButtonsModel()
            {
                ButtonName = name,
                ButtonCommand = new Commands.CommandBase(action, canExecute)
            };
        }
    }
}
