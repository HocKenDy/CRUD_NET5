namespace CRUD_NET5.ViewModels.Common
{
    public class APIResult<T>
    {
        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
    }
}
