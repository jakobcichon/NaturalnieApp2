namespace NaturalnieApp2.SharedControls.CustomControls.TextBox
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class MaskedTextBox : TextBox
    {
        private const string defaultMask = ".*";

        static MaskedTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaskedTextBox), new FrameworkPropertyMetadata(typeof(MaskedTextBox)));
        }

        public MaskedTextBox()
        {
            PreviewTextInput += this.MaskedTextBox_PreviewTextInput;
            
        }

        private void MaskedTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            if(!Regex.IsMatch(e.Text, MaskPattern))
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// Mask pattern which will be used for regex, to validate if given character can be provided
        /// </summary>
        public string MaskPattern
        {
            get { return (string)GetValue(MaskPatternProperty); }
            set 
            {
                if(value == null)
                {
                    SetValue(MaskPatternProperty, defaultMask);
                    return;
                }
                SetValue(MaskPatternProperty, value); 
            }
        }

        public static readonly DependencyProperty MaskPatternProperty =
            DependencyProperty.Register("MaskPattern", typeof(string), typeof(MaskedTextBox), new FrameworkPropertyMetadata(
                defaultValue: defaultMask),
            validateValueCallback: new ValidateValueCallback(IsValidMask));

        private static bool IsValidMask(object value)
        {
            if(value == null)
            {
                return false;
            }
            return true;
        }
    }
}
