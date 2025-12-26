namespace RestaurantAPI.Exceptions
{
    public class FileSizeExceededException : Exception
    {
        public long FileSizeInBytes { get; }

        public FileSizeExceededException(long maxSizeInBytes)
            : base($"File size exceeds the allowed limit of {maxSizeInBytes} bytes.")
        {

        }
    }
}
