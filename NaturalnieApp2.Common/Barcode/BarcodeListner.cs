namespace NaturalnieApp2.Common.Barcode
{
    using NaturalnieApp2.SharedInterfaces.Logger;
    using System;
    using System.Diagnostics;
    using System.Timers;
    using System.Windows.Input;

    public class BarcodeListner
    {
        //Object fields
        System.Timers.Timer timer { get; init; }
        public string BarcodeToReturn { get; set; }
        string TemporaryBarcodeValue { get; set; }
        public bool Ready { get; set; }
        public bool Valid { get; set; }

        //Register an event
        public event BarcodeValidEventHandler BarcodeValid;

        public class BarcodeValidEventArgs : EventArgs
        {
            public bool Ready { get; set; }
            public bool Valid { get; set; }
            public string RecognizedBarcodeValue { get; set; } = string.Empty;
        }

        //Declare new event handler
        public delegate void BarcodeValidEventHandler(object sender, BarcodeValidEventArgs e);

        //Declaration of event handler
        protected virtual void OnBarcodeValid(BarcodeValidEventArgs e)
        {
            BarcodeValidEventHandler handler = BarcodeValid;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// CLass constructor
        /// </summary>
        /// <param name="barcodeReaderCharInterval"></param>
        public BarcodeListner(double barcodeReaderCharInterval, ILogger loggerService)
        {
            //Initialize timer
            this.timer = new Timer(barcodeReaderCharInterval);
            this.timer.Elapsed += OnTimedEvent;
            this.timer.Enabled = true;

            this.TemporaryBarcodeValue = "";
            this.Ready = true;
        }


        /// <summary>
        /// Method used to recognize if valid Barcode value.
        /// It should be placed in object KEyDown event.
        /// </summary>
        /// <param name="key"></param>
        public bool CheckIfBarcodeFromReader(Key key)
        {
            Debug.WriteLine($"Read key {key}");
            //Make initialization after first call
            if (this.Ready == true)
            {
                this.timer.Start();
                this.Ready = false;
                this.BarcodeToReturn = "";
                this.Valid = false;
            }

            //Recognize only digits
            if (key == Key.D0 || key == Key.D1 || key == Key.D2 || key == Key.D3 || key == Key.D4 ||
                key == Key.D5 || key == Key.D6 || key == Key.D7 || key == Key.D8 || key == Key.D9)
            {
                //Reset timer
                this.timer.Stop();
                this.timer.Start();

                switch (key)
                {
                    case Key.D0:
                        this.TemporaryBarcodeValue += "0";
                        break;
                    case Key.D1:
                        this.TemporaryBarcodeValue += "1";
                        break;
                    case Key.D2:
                        this.TemporaryBarcodeValue += "2";
                        break;
                    case Key.D3:
                        this.TemporaryBarcodeValue += "3";
                        break;
                    case Key.D4:
                        this.TemporaryBarcodeValue += "4";
                        break;
                    case Key.D5:
                        this.TemporaryBarcodeValue += "5";
                        break;
                    case Key.D6:
                        this.TemporaryBarcodeValue += "6";
                        break;
                    case Key.D7:
                        this.TemporaryBarcodeValue += "7";
                        break;
                    case Key.D8:
                        this.TemporaryBarcodeValue += "8";
                        break;
                    case Key.D9:
                        this.TemporaryBarcodeValue += "9";
                        break;
                }

            }
            else if (key == Key.Enter)
            {
                this.timer.Stop();
                this.Ready = true;
                this.BarcodeToReturn = this.TemporaryBarcodeValue;
                this.TemporaryBarcodeValue = "";
                if (this.BarcodeToReturn.Length == 8 || this.BarcodeToReturn.Length == 12 || this.BarcodeToReturn.Length == 13)
                {
                    this.Valid = true;
                    CallBarcodeValidEvent(this.Ready, this.Valid, this.BarcodeToReturn);
                    return true;
                }
                else this.Valid = false;
            }

            return false;
        }

        private void OnTimedEvent(object? source, ElapsedEventArgs e)
        {
            this.timer.Stop();
            this.Ready = true;
            this.Valid = false;
            this.TemporaryBarcodeValue = "";
        }


        private void CallBarcodeValidEvent(bool ready, bool valid, string barcode)
        {
            BarcodeValidEventArgs e = new()
            {
                Ready = ready,
                Valid = valid,
                RecognizedBarcodeValue = barcode
            };
            OnBarcodeValid(e);
        }
    }
}
