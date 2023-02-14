namespace NetwordUtils
{
    public delegate void CommonResultHandler<T>(CommonResult<T> commonResult);

    public class CommonResult<T>
    {
        public int code;
        public string message;
        public T data;
    }
}