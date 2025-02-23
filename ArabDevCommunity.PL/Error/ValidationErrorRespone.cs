namespace ArabDevCommunity.PL.Error
{
    public class ValidationErrorRespone : ApiExceptionResponse
    {
        public ValidationErrorRespone(): base(400)
        {
            Errors=new List<string>();
        }

        public IEnumerable<string> Errors { get; set; } 
    }
}
