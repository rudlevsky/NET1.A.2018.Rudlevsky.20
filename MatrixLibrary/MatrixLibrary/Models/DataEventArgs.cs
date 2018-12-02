using System;

namespace MatrixLibrary.Models
{
    /// <summary>
    /// Class contains 
    /// </summary>
    public class DataEventArgs : EventArgs
    {
        /// <summary>
        /// Message for subscribers.
        /// </summary>
        public string Message { get; set; }

        const string STANDARD_MESSAGE = "Element was changed.";

        /// <summary>
        /// Constructor without arguments.
        /// </summary>
        public DataEventArgs() => Message = STANDARD_MESSAGE;

        /// <summary>
        /// Constructor with one argument.
        /// </summary>
        /// <param name="message">Message for subscribers.</param>
        public DataEventArgs(string message) => Message = message;
    }
}
