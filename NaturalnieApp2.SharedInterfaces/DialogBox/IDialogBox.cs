namespace NaturalnieApp2.SharedInterfaces.DialogBox
{
    using System.Runtime.CompilerServices;

    public interface IDialogBox
    {
        public IDialogBox Show(string message);
        public IDialogBox ShowYesNo(string message);
        public IDialogBox ShowYesNoCancel(string message);
        public IDialogBox AddAction(DialogBoxResults resultType, Action action);
    }
}
