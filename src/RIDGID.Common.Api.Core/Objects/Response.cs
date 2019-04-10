namespace RIDGID.Common.Api.Core.Objects
{
    public class Response<T>
    {
        public Response(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
