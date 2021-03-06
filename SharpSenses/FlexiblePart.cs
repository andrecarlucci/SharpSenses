using System;

namespace SharpSenses {
    public class FlexiblePart : Item {
        private int _faults = 0;

        public static int DefaultTolerance = 0;

        public int Tolerance { get; set; }

        private bool _isOpen;
        public event EventHandler Opened;
        public event EventHandler Closed;

        public FlexiblePart() {
            Tolerance = DefaultTolerance;
        }

        public virtual string GetInfo() {
            return String.Format("{0} o:{1} x:{2} y:{3}", 
                IsVisible, IsOpen, Position.Image.X, Position.Image.Y);
        }

        public bool IsOpen {
            get { return _isOpen; }
            set {
                if (_isOpen == value) return;
                if (_faults < Tolerance) {
                    _faults++;
                    return;
                }
                _faults = 0;
                _isOpen = value;
                if (_isOpen) {
                    OnOpened();
                }
                else {
                    OnClosed();
                }
                RaisePropertyChanged(() => IsOpen);
            }
        }

        protected virtual void OnClosed() {
            var handler = Closed;
            if (handler != null) handler(this, new EventArgs());
        }

        protected virtual void OnOpened() {
            var handler = Opened;
            if (handler != null) handler(this, new EventArgs());
        }
    }
}