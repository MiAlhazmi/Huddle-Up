using System;

namespace Model
{
    [Serializable]
    public class Result<T>
    {
        public bool success;
        public T data;
        public string message;

        public bool Success
        {
            get => success;
            set => success = value;
        }

        public T Data
        {
            get => data;
            set => data = value;
        }

        public string Message
        {
            get => message;
            set => message = value;
        }


        public override string ToString()
        {
            return $"success: {success}, data: {data}, message: {message}";
        }

        public Result(bool success, T data, string message)
        {
            this.success = success;
            this.data = data;
            this.message = message;
        }
    } 
}