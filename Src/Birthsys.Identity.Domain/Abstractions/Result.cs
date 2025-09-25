namespace Birthsys.Identity.Domain.Abstractions
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {

            if (isSuccess && error != Error.None)
                throw new InvalidOperationException("A successful result cannot have an error.");
            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException("A failure result must have an error.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);
        public static Result<T> Success<T>(T value) => new(value, true, Error.None);

        public static Result Failure(Error error) => new(false, error);
        public static Result<T> Failure<T>(Error error) => new(default!, false, error);

        public static Result<T> Create<T>(T value) => value is not null ? Success(value) : Failure<T>(Error.ReferenceNull);

    }
    public class Result<T> : Result
    {
        private readonly T _value;
        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("Cannot access the value of a failure result.");
                return _value;
            }
        }

        protected internal Result(T value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public static implicit operator Result<T>(T value) => Create(value);
    }

}