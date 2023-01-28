namespace FurnitureShop.Domain.Models
{
    public class Result
    {
        public bool Succeeded { get; set; } = true;

        public string ErrorMessage { get; private set; } = string.Empty;

        protected Result(bool suceeded, string error)
        {
            if (suceeded && error != string.Empty)
                throw new InvalidOperationException();
            if (!suceeded && error == string.Empty)
                throw new InvalidOperationException();

            Succeeded = suceeded;
            ErrorMessage = error;
        }

        public static Result Failed(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Failed<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Ok<T>(T data)
        {
            return new Result<T>(data, true, string.Empty);
        }
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public Result(T? data, bool succeeded, string error) : base(succeeded, error)
        {
            Data = data;
        }
    }
}
