using System;

namespace MatrixLibrary.Models
{
    public class DataEventArgs : EventArgs
    {
        public string message;

        const string STANDARD_MESSAGE = "Element was changed.";

        public DataEventArgs() => this.message = STANDARD_MESSAGE;

        public DataEventArgs(string message) => this.message = message;
    }
}
