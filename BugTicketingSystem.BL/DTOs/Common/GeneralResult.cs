namespace BugTicketingSystem.BL
{
    public class GeneralResult
    {
        public bool Success { get; set; }
        public ResultError[] Errors { get; set; } = [];
    }

    public class GeneralResult<T> : GeneralResult
    {
        public T? Data { get; set; }
    }

    public class ResultError
    {
        public string Code { get; set; } = string.Empty;
        public string Msg { get; set; } = string.Empty;
    }
}
