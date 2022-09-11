namespace NaturalnieApp2.SharedInterfaces.DialogBox
{
    public interface IDialogBox
    {
        public IDialogBox Show(string message);
        public IDialogBox ShowYesNo(string message);
        public IDialogBox ShowYesNoCancel(string message);
        public IDialogBox AddAction(DialogResultEnum resultType, Action action);
    }
}
