namespace NaturalnieApp2.SharedInterfaces.DialogBox
{
    public interface IDialogBox
    {
        public void Show(string message);
        public DialogResultEnum ShowYesNo(string message);
        public DialogResultEnum ShowYesNoCancel(string message);
    }
}
