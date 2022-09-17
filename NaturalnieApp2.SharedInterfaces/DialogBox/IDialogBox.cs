﻿namespace NaturalnieApp2.SharedInterfaces.DialogBox
{
    using System.Runtime.CompilerServices;

    public interface IDialogBox
    {
        public IDialogBox Show(string message, string title = null);
        public IDialogBox ShowYesNo(string message, string title = null);
        public IDialogBox ShowYesNoCancel(string message, string title = null);
        public IDialogBox AddAction(DialogBoxResults resultType, Action action);
    }
}
