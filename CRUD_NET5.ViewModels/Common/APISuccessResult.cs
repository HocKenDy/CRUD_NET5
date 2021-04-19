namespace CRUD_NET5.ViewModels.Common
{
    public class APISuccessResult<T> : APIResult<T>
    {
        public APISuccessResult()
        {
            IsSuccessed = true;
        }
        public APISuccessResult(T result)
        {
            IsSuccessed = true;
            ResultObj = result;
        }
    }
}
